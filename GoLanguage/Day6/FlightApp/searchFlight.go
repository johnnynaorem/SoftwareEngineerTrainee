package main

import (
	"myModule/model"
	"net/http"

	"github.com/gin-gonic/gin"
)

func SearchFlight(ctx *gin.Context) {
	var flights []model.Flights

	// Get query parameters
	origin := ctx.DefaultQuery("origin", "") // ! Default to empty string if not provided
	destination := ctx.DefaultQuery("destination", "")

	// * Build the query dynamically based on the provided parameters
	query := flightDbConnector.Model(&model.Flights{})

	// * Add conditions to the query if the parameters are provided
	if origin != "" {
		query = query.Where("departure_airport = ?", origin)
	}
	if destination != "" {
		query = query.Where("arrival_airport = ?", destination)
	}

	// * Execute the query to fetch the filtered results
	if err := query.Find(&flights).Error; err != nil {
		// ? If there's an error in fetching data, return an error message
		ctx.JSON(http.StatusInternalServerError, gin.H{"error": "Unable to retrieve flights"})
		return
	}

	// * Respond with the filtered flights and total count
	totalFlights := len(flights)
	if totalFlights == 0 {
		ctx.JSON(http.StatusOK, gin.H{
			"message": "No Match Flight",
		})
		return
	}
	ctx.JSON(http.StatusOK, gin.H{
		"totalFlight": totalFlights,
		"flights":     flights,
	})
}