package main

import (
	"myModule/config"
	flightstructs "myModule/flightStructs"
	"net/http"

	"github.com/gin-gonic/gin"
	"go.uber.org/zap"
	"gorm.io/gorm"
)

// func main() {
// 	flightstructs.FlightMainFunction()
// }

var logger *zap.Logger
var flightDbConnector *gorm.DB

func init() {
	var err error
	logger, err = zap.NewDevelopment()

	if err != nil {
		panic(err)
	}
	defer logger.Sync()
}

func main() {
	flightDbConnector = config.ConnectDB()

	// configuration of the http server.
	httpServer := gin.Default()
	//? Method : @POST
	// ? Endpoint Route : /save-user
	httpServer.POST("/save-flight", AddFlight)
	httpServer.GET("/hi", func(ctx *gin.Context) {
		ctx.JSON(http.StatusCreated, gin.H{"message": "User created successfully"})
	})

	httpServer.GET("/getall-flight", func(ctx *gin.Context) {
		var flights []flightstructs.FlightStruct

		if err := flightDbConnector.Find(&flights).Error; err != nil {
			ctx.JSON(http.StatusInternalServerError, gin.H{"error": "Unable to retrieve flights"})
			return
		}
		totalFlights := len(flights)
		ctx.JSON(http.StatusOK, gin.H{"totalFlight": totalFlights, "flights": flights})
	})

	httpServer.GET("/search-flight", func(ctx *gin.Context) {
		var flights []flightstructs.FlightStruct

		// Get query parameters
		origin := ctx.DefaultQuery("origin", "") // ! Default to empty string if not provided
		destination := ctx.DefaultQuery("destination", "")

		// * Build the query dynamically based on the provided parameters
		query := flightDbConnector.Model(&flightstructs.FlightStruct{})

		// * Add conditions to the query if the parameters are provided
		if origin != "" {
			query = query.Where("departure_from = ?", origin)
		}
		if destination != "" {
			query = query.Where("arrival_to = ?", destination)
		}

		// * Execute the query to fetch the filtered results
		if err := query.Find(&flights).Error; err != nil {
			// If there's an error in fetching data, return an error message
			ctx.JSON(http.StatusInternalServerError, gin.H{"error": "Unable to retrieve flights"})
			return
		}

		// * Respond with the filtered flights and total count
		totalFlights := len(flights)
		if totalFlights <= 0 {
			ctx.JSON(http.StatusOK, gin.H{
				"message": "No Match Flight",
			})
			return
		}
		ctx.JSON(http.StatusOK, gin.H{
			"totalFlight": totalFlights,
			"flights":     flights,
		})
	})

	// running the server
	httpServer.Run(":8081")
}
