package main

import (
	"auth-micro/config"
	"auth-micro/jwt"
	"fmt"
	"net/http"
	"time"

	"github.com/gin-gonic/gin"
	"go.uber.org/zap"
	"gorm.io/gorm"
)

var logger *zap.Logger
var dbConnector *gorm.DB
var jwtManager *jwt.JWTManager

func init() {
	var err error
	logger, err = zap.NewDevelopment()

	if err != nil {
		panic(err)
	}
	defer logger.Sync()
}

func main() {
	dbConnector = config.ConnectDB()

	// * Create a new jwt manager
	jwtManager = jwt.NewJWTManager("SECRET_KEY", 5*time.Hour)

	// * configuration of the http server.
	httpServer := gin.Default()

	//? Method : @POST
	// ? Endpoint Route : /save-user
	httpServer.POST("/save-user", AddUser)
	httpServer.POST("/login-user", AuthenticateUser)
	httpServer.GET("/hi", func(ctx *gin.Context) {
		ctx.JSON(http.StatusCreated, gin.H{"message": "User created successfully"})
	})

	// * Protected Route
	httpServer.Use(jwt.AuthorizeJwtToken())
	httpServer.GET("/testAuthorization", func(ctx *gin.Context) {
		fmt.Println(ctx.GetString("usermail"))
		ctx.JSON(http.StatusOK, gin.H{"message": "Authorization"})
	})

	httpServer.POST("/add-product", AddProduct)
	httpServer.GET("/get-all-products", GetAllProduct)
	httpServer.GET("/get-all-products-by-user", GetAllProductByUserId)
	httpServer.DELETE("/delete-product/:id", DeleteProduct)

	// ! running the server
	httpServer.Run(":8080")
}
