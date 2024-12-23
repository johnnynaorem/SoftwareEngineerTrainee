package main

import (
	"myModule/config"
	"myModule/jwt"
	"net/http"
	"time"

	"github.com/gin-gonic/gin"
	"github.com/joho/godotenv"
	"go.uber.org/zap"
	"gorm.io/gorm"
)

// func main() {
// 	flightstructs.FlightMainFunction()
// }

var logger *zap.Logger
var flightDbConnector *gorm.DB
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
	if err := godotenv.Load(".env"); err != nil {
		panic("No .env file found")
	}
	flightDbConnector = config.ConnectDB()

	// * Create a new jwt manager
	jwtManager = jwt.NewJWTManager("SECRET_KEY", 5*time.Hour)

	// configuration of the http server.
	httpServer := gin.Default()

	// ? Unprotected Routes
	httpServer.POST("/save-user", AddUser)
	httpServer.POST("/login-user", Login)
	httpServer.GET("/hi", func(ctx *gin.Context) {
		ctx.JSON(http.StatusCreated, gin.H{"message": "User created successfully"})
	})

	// ? Protected Routes
	httpServer.Use(jwt.AuthorizeJwtToken())
	httpServer.POST("/add-flight", AddFlight)
	httpServer.GET("/getall-flight", GetAllFlight)
	httpServer.GET("/get-all-flight-by-user", GetAllByUser)
	httpServer.GET("/search-flight", SearchFlight)
	httpServer.DELETE("/delete-flight/:flightId", DeleteFlight)
	httpServer.PATCH("/update-flight/:flightId", UpdateFlight)

	// running the server
	httpServer.Run(":8081")
}
