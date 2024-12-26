package main

import (
	"testing"

	"github.com/Rhymond/go-money"
	"github.com/stretchr/testify/assert"
)

func TestComputeProductCategoryAmount(t *testing.T) {

	t.Run("Positive Case", func(t *testing.T) {
		productCategory := Products{
			ID: "1",
			Products: []Product{
				{
					ID:        "1",
					name:      "Product 1",
					Quantity:  2,
					UnitPrice: money.New(100, "USD"),
				},
				{
					ID:        "2",
					name:      "Product 2",
					Quantity:  3,
					UnitPrice: money.New(200, "USD"),
				},
			},
			Currency: "USD",
		}

		amount, err := productCategory.ComputeProductCategoryAmount()
		assert.NoError(t, err)

		assert.Equal(t, int64(800), amount.Amount())
		assert.Equal(t, "USD", amount.Currency().Code)
	})

	t.Run("Currency Issues", func(t *testing.T) {
		productCategory := Products{
			ID:       "1",
			Currency: "EUR",
			Products: []Product{
				{
					ID:        "1",
					name:      "Product 1",
					Quantity:  2,
					UnitPrice: money.New(100, "USD"),
				},
			},
		}

		_, err := productCategory.ComputeProductCategoryAmount()

		assert.Error(t, err)
	})
}
