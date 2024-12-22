package main

import (
	"myModule/model"
	"net/http"

	"github.com/gin-gonic/gin"
	"go.uber.org/zap"
)

func GetAllByUser(ctx *gin.Context) {
	var flights []model.Flights
	var userEmail = ctx.GetString("userEmail")

	if userEmail == "" {
		logger.Error("failed to get the token from the header")
		ctx.JSON(http.StatusInternalServerError, gin.H{"message": "failed to get the token from the header"})
		return
	}

	// *2. Get the user from the database and extract the user id
	var user model.User
	flightDbConnector.Where("email = ?", userEmail).First(&user)

	// *3. Write the logic to fetch all flight by user id
	err := flightDbConnector.Where("user_id = ?", user.ID).Find(&flights).Error
	if err != nil {
		logger.Error("Failed to get the flight", zap.Error(err))
		ctx.JSON(http.StatusInternalServerError, gin.H{"message": "Failed to get the flghts"})
		return
	}

	total_flight := len(flights)
	if total_flight == 0 {
		logger.Info("No flight found")
		ctx.JSON(http.StatusOK, gin.H{"message": "No flight found"})
		return
	}

	logger.Info("Products fetched successfully")
	ctx.JSON(http.StatusOK, gin.H{"flight": flights, "total_flight": total_flight})
}
