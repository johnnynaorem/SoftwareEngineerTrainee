package config

import (
	"fmt"
	"myModule/model"
	"os"

	// "gorm.io/driver/sqlserver"
	"gorm.io/driver/mysql"
	"gorm.io/gorm"
)

func DatabaseDsn() string {
	// return fmt.Sprintf("sqlserver://PYRAMIDINDIA.johnny%%20naorem:Pyramid%%40321@127.0.0.1:1433?database=test")
	// return fmt.Sprintf("username:password@tcp(127.0.0.1:port)/db_name?charset=utf8&parseTime=True&loc=Local")
	return fmt.Sprintf("%s:%s@tcp(%s:%s)/%s?charset=utf8&parseTime=True&loc=Local",
		os.Getenv("MYSQL_USER"),
		os.Getenv("MYSQL_PASSWORD"),
		os.Getenv("MYSQL_HOST"),
		os.Getenv("MYSQL_PORT"),
		os.Getenv("MYSQL_DATABASE"),
	)
}

func ConnectDB() *gorm.DB {
	userdb, err := gorm.Open(mysql.Open(DatabaseDsn()), &gorm.Config{})

	if err != nil {
		panic("Failed to connect DB")
	}
	userdb.AutoMigrate(&model.User{}, &model.Flights{})
	return userdb
}
