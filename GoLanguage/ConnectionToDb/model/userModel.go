package model

import "gorm.io/gorm"

type User struct {
	gorm.Model
	Name     string
	Password string
	Email    string
	Phone    string
	Address  string
	City     string
}
