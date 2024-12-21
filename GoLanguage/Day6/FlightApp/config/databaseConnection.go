package config

import (
	"fmt"
	"myModule/model"

	// "gorm.io/driver/sqlserver"
	"gorm.io/driver/mysql"
	"gorm.io/gorm"
)

func DatabaseDsn() string {
	// return fmt.Sprintf("sqlserver://PYRAMIDINDIA.johnny%%20naorem:Pyramid%%40321@127.0.0.1:1433?database=test")
	return fmt.Sprintf("root:Johnny02@tcp(127.0.0.1:3306)/flightDB?charset=utf8&parseTime=True&loc=Local")
}

func ConnectDB() *gorm.DB {
	userdb, err := gorm.Open(mysql.Open(DatabaseDsn()), &gorm.Config{})

	if err != nil {
		panic("Failed to connect DB")
	}
	userdb.AutoMigrate(&model.User{}, &model.Flights{})
	return userdb
}
