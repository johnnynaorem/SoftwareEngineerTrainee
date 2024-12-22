package main

import (
	"myModule/model"
	"net/http"
	"strconv"

	"github.com/gin-gonic/gin"
)

func DeleteFlight(ctx *gin.Context) {
	var flight model.Flights
	// * Get the flight number from the URL parameter
	flightId := ctx.Param("flightId")

	// * Check if the flight exists
	if err := flightDbConnector.Where("id = ?", flightId).First(&flight).Error; err != nil {
		ctx.JSON(http.StatusNotFound, gin.H{"error": "Flight not found"})
		return
	}

	var userEmail = ctx.GetString("userEmail")

	if userEmail == "" {
		logger.Error("failed to get the token from the header")
		ctx.JSON(http.StatusInternalServerError, gin.H{"message": "failed to get the token from the header"})
		return
	}

	var user model.User
	flightDbConnector.Where("email = ?", userEmail).First(&user)
	// * Check if the user is authorized to delete the flight
	if user.Role != "admin" || strconv.FormatUint(uint64(user.ID), 10) != flight.UserId {
		ctx.JSON(http.StatusUnauthorized, gin.H{"error": "User is not authorized to delete this flight"})
		return
	}

	// * Delete the flight
	if err := flightDbConnector.Delete(&flight).Error; err != nil {
		ctx.JSON(http.StatusInternalServerError, gin.H{"error": "Failed to delete the flight"})
		return
	}

	ctx.JSON(http.StatusOK, gin.H{"message": "Flight deleted successfully", "flight": flight})
}
