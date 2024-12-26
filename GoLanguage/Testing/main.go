package main

import "github.com/Rhymond/go-money"

type Product struct {
	ID        string
	name      string
	Quantity  uint
	UnitPrice *money.Money // ? It contains two fields, amount and currency
}

type Products struct {
	ID       string // ? It is the Category ID
	Products []Product
	Currency string
}

func (productCategory Products) ComputeProductCategoryAmount() (*money.Money, error) {
	totalAmount := money.New(0, productCategory.Currency)
	for _, product := range productCategory.Products {
		var err error
		totalAmount, err = totalAmount.Add(product.UnitPrice.Multiply(int64(product.Quantity)))
		if err != nil {
			return nil, err
		}
	}
	return totalAmount, nil
}
