package config

import (
	"auth-micro/model"
	"fmt"

	// "gorm.io/driver/sqlserver"
	"gorm.io/driver/mysql"
	"gorm.io/gorm"
)

func DatabaseDsn() string {
	// return fmt.Sprintf("sqlserver://PYRAMIDINDIA.johnny%%20naorem:Pyramid%%40321@127.0.0.1:1433?database=test")
	return fmt.Sprintf("root:Johnny02@tcp(127.0.0.1:3306)/johnny?charset=utf8&parseTime=True&loc=Local")
}

func ConnectDB() *gorm.DB {
	db, err := gorm.Open(mysql.Open(DatabaseDsn()), &gorm.Config{})

	if err != nil {
		panic("Failed to connect DB")
	}
	if err := db.AutoMigrate(&model.User{}, &model.Product{}); err != nil {
		panic(fmt.Sprintf("Failed to auto-migrate: %v", err.Error()))
	}
	return db
}
