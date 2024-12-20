package main

import (
	"fmt"
	flightstructs "myModule/flightStructs"
	"net/http"

	"github.com/gin-gonic/gin"
	"go.uber.org/zap"
	"gorm.io/gorm"
)

func AddFlight(ctx *gin.Context) {
	var flight flightstructs.FlightStruct
	ctx.ShouldBindJSON(&flight)

	// ?1. Check for Existing Flight.
	var existingFlight flightstructs.FlightStruct

	flightNotFoundError := flightDbConnector.Where("flight_number = ?", flight.FlightNumber).First(&existingFlight).Error

	if flightNotFoundError == gorm.ErrRecordNotFound {
		newFlight := &flightstructs.FlightStruct{FlightNumber: flight.FlightNumber, Fair: flight.Fair, ArrivalTo: flight.ArrivalTo, DepartureFrom: flight.DepartureFrom, FlightDate: flight.FlightDate}

		primaryKey := flightDbConnector.Create(newFlight)

		if primaryKey.Error != nil {
			logger.Error("Failed to Add Flight", zap.String("flight number ", flight.FlightNumber), zap.Error(primaryKey.Error))
			ctx.JSON(http.StatusConflict, gin.H{"message": "The Flight is already added"})
			return
		}
		logger.Info(fmt.Sprintf("User %s created successfully", flight.FlightNumber))
		ctx.JSON(http.StatusCreated, gin.H{"message": "Flight added successfully"})

	} else {
		logger.Warn("User Flight Already Exist", zap.String("flight number", flight.FlightNumber))
		ctx.JSON(http.StatusConflict, gin.H{"message": "Flight Already Exist"})
	}

}
