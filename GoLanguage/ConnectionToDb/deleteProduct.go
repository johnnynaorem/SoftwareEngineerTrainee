package main

import (
	"auth-micro/model"
	"net/http"

	"github.com/gin-gonic/gin"
	"go.uber.org/zap"
)

func DeleteProduct(ctx *gin.Context) {
	// *1. Get the product id from the request
	var product model.Product
	productID := ctx.Param("id")

	// *2. Delete the product from the database
	err := dbConnector.Where("id = ?", productID).Delete(&product).Error
	if err != nil {
		logger.Error("Failed to delete the product", zap.Error(err))
		ctx.JSON(http.StatusInternalServerError, gin.H{"message": "Failed to delete the product"})
		return
	}

	logger.Info("Product deleted successfully")
	ctx.JSON(http.StatusOK, gin.H{"message": "Product deleted successfully"})
}
