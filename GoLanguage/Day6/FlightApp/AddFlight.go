package main

import (
	"fmt"
	flightstructs "myModule/flightStructs"
	"net/http"
	"regexp"

	"github.com/gin-gonic/gin"
	"go.uber.org/zap"
	"gorm.io/gorm"
)

// func validateUser(user model.User) error {
// 	// Email format validation
// 	if !isValidEmail(user.Email) {
// 		return fmt.Errorf("Invalid email format")
// 	}

// 	// Password length validation
// 	if len(user.Password) < 8 {
// 		return fmt.Errorf("Password must be at least 8 characters long")
// 	}

// 	// Phone number format validation (assuming it should be numeric)
// 	if !isValidPhone(user.Phone) {
// 		return fmt.Errorf("Invalid phone number format")
// 	}

// 	// Address and City check
// 	// if user.Address == "" || user.City == "" {
// 	// 	return fmt.Errorf("Address and City are required")
// 	// }

// 	return nil
// }

// Helper function to validate email format using regex
func isValidEmail(email string) bool {
	re := regexp.MustCompile(`^[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,}$`)
	return re.MatchString(email)
}

// Helper function to validate phone number format
func isValidPhone(phone string) bool {
	re := regexp.MustCompile(`^\d{10}$`)
	return re.MatchString(phone)
}

func AddFlight(ctx *gin.Context) {
	var flight flightstructs.FlightStruct
	ctx.ShouldBindJSON(&flight)

	// logger.Info("Recieved User Request", zap.String("useremail", user.Email), zap.String("username", user.Name))

	// ?1. Write the validation Logic

	// if err := validateUser(user); err != nil {
	// 	ctx.JSON(http.StatusBadRequest, gin.H{"message": err.Error()})
	// 	return
	// }

	// ?2. Check for Existing Flight.
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
