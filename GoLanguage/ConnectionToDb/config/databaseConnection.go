package config

import (
	"auth-micro/model"
	"fmt"

	"gorm.io/driver/sqlserver"
	"gorm.io/gorm"
)

func DatabaseDsn() string {
	return fmt.Sprintf("sqlserver://PYRAMIDINDIA.johnny%%20naorem:Pyramid%%40321@127.0.0.1:1433?database=test")
}

func ConnectDB() *gorm.DB {
	userdb, err := gorm.Open(sqlserver.Open(DatabaseDsn()), &gorm.Config{})

	if err != nil {
		panic("Failed to connect DB")
	}
	userdb.AutoMigrate(&model.User{})
	return userdb
}
