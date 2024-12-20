package main

import (
	"auth-micro/model"
	"net/http"
	"strconv"

	"github.com/gin-gonic/gin"
	"go.uber.org/zap"
)

func AddProduct(ctx *gin.Context) {
	var product model.Product
	ctx.ShouldBindJSON(&product)

	// * 1. Get User Email from Claim
	userEmail := ctx.GetString("usermail")
	if userEmail == "" {
		logger.Error("failed to get the token from the header")
		ctx.JSON(http.StatusInternalServerError, gin.H{"message": "failed to get the token from the header"})
		return
	}

	// * 2. validate the product
	if product.Name == "" {
		ctx.JSON(http.StatusBadRequest, gin.H{"message": "Name field is missing, please add the name field"})
		return
	}

	// * 3. Get the user from the database
	var user model.User
	dbConnector.Where("email = ?", userEmail).First(&user)

	// * 4. addind the user id inside the product
	product.UserId = strconv.FormatUint(uint64(user.ID), 10)

	// * 5. check for the existing product
	var existingProduct model.Product
	productErrorNotFound := dbConnector.Where("name = ? AND user_id = ?", product.Name, product.UserId).First(&existingProduct).Error

	if productErrorNotFound == nil {
		ctx.JSON(http.StatusConflict, gin.H{"message": "Same Product Already Exist."})
		return
	}

	// * 6. Write the logic to save the product in the database
	primaryKey := dbConnector.Create(&product)

	if primaryKey.Error != nil {
		logger.Error("Failed to create the product", zap.Error(primaryKey.Error))
		ctx.JSON(http.StatusInternalServerError, gin.H{"message": "Failed to create the product"})
		return
	}
	logger.Info("Product created successfully", zap.String("product name", product.Name))
	ctx.JSON(http.StatusCreated, gin.H{"message": "Product created successfully"})
}
