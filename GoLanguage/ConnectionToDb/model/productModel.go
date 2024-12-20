package model

import "gorm.io/gorm"

type Product struct {
	gorm.Model
	Name        string  `gorm:"type:varchar(255);uniqueIndex:user_product_unique"`
	UserId      string  `gorm:"type:varchar(255);uniqueIndex:user_product_unique"`
	Description string  `gorm:"type:text"`
	Price       float64 `gorm:"type:decimal(10,2)"`
	Quantity    uint    `gorm:"default:5"`
	Rating      float32 `gorm:"default:4"`
}
