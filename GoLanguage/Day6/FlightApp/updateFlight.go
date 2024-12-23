package main

import (
	"myModule/model"
	"net/http"
	"strconv"

	"github.com/gin-gonic/gin"
	"go.uber.org/zap"
)

func UpdateFlight(ctx *gin.Context) {
	var flight model.Flights
	ctx.ShouldBindJSON(&flight)

	var flightStatus = flight.FlightStatus
	// * Get the flight number from the URL parameter
	flightId := ctx.Param("flightId")

	// * Check if the flight exists
	var updatedFlight model.Flights
	if err := flightDbConnector.Where("id = ?", flightId).First(&updatedFlight).Error; err != nil {
		ctx.JSON(http.StatusNotFound, gin.H{"error": "Flight not found"})
		return
	}

	updatedFlight.FlightStatus = flightStatus

	var userEmail = ctx.GetString("userEmail")
	if userEmail == "" {
		logger.Error("failed to get the token from the header")
		ctx.JSON(http.StatusInternalServerError, gin.H{"message": "failed to get the token from the header"})
		return
	}

	// * Check if the user is authorized to update the flight
	var user model.User
	flightDbConnector.Where("email = ?", userEmail).First(&user)
	logger.Info("User", zap.String("Role", user.Role), zap.String("ID", strconv.FormatUint(uint64(user.ID), 10)))

	if user.Role != "admin" || strconv.FormatUint(uint64(user.ID), 10) != updatedFlight.UserId {
		ctx.JSON(http.StatusUnauthorized, gin.H{"error": "User is not authorized to update this flight"})
		return
	}

	// * Update the flight
	if err := flightDbConnector.Model(&model.Flights{}).Where("id = ?", flightId).Updates(&updatedFlight).Error; err != nil {
		ctx.JSON(http.StatusInternalServerError, gin.H{"error": "Failed to update the flight"})
		return
	}

	ctx.JSON(http.StatusOK, gin.H{"message": "Flight updated successfully", "flight": updatedFlight})

}
