package main

import (
	"auth-micro/model"
	"net/http"

	"github.com/gin-gonic/gin"
	"go.uber.org/zap"
)

func GetAllProduct(ctx *gin.Context) {
	// *1. Write the logic to fetch all products from the database
	var products []model.Product
	err := dbConnector.Find(&products).Error

	if err != nil {
		logger.Error("Failed to get the products", zap.Error(err))
		ctx.JSON(http.StatusInternalServerError, gin.H{"message": "Failed to get the products"})
		return
	}

	totalProducts := len(products)
	if totalProducts == 0 {
		logger.Info("No products found")
		ctx.JSON(http.StatusOK, gin.H{"message": "No products found"})
		return
	}

	logger.Info("Products fetched successfully")
	ctx.JSON(http.StatusOK, gin.H{"products": products, "total_products": totalProducts})
}

func GetAllProductByUserId(ctx *gin.Context) {
	var products []model.Product

	// *1. Get User Email from Claim
	userEmail := ctx.GetString("usermail")
	if userEmail == "" {
		logger.Error("failed to get the token from the header")
		ctx.JSON(http.StatusInternalServerError, gin.H{"message": "failed to get the token from the header"})
		return
	}

	// *2. Get the user from the database and extract the user id
	var user model.User
	dbConnector.Where("email = ?", userEmail).First(&user)

	// *3. Write the logic to fetch all products by user id
	err := dbConnector.Where("user_id = ?", user.ID).Find(&products).Error
	if err != nil {
		logger.Error("Failed to get the products", zap.Error(err))
		ctx.JSON(http.StatusInternalServerError, gin.H{"message": "Failed to get the products"})
		return
	}

	totalProducts := len(products)
	if totalProducts == 0 {
		logger.Info("No products found")
		ctx.JSON(http.StatusOK, gin.H{"message": "No products found"})
		return
	}

	logger.Info("Products fetched successfully")
	ctx.JSON(http.StatusOK, gin.H{"products": products, "total_products": totalProducts})

}
