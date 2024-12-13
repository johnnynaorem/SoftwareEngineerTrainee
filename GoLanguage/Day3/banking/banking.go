package banking

import (
	"fmt"
	"strconv"
	"time"
)

	type Transaction struct {
		Date   time.Time
		Amount float64
		Type   string
	}

type BankAccount struct {
	Balance          float64
	TransactionHistory []Transaction
}

func (account *BankAccount) Deposit(amount float64) {
	if amount <= 0 {
		fmt.Println("Invalid deposit amount")
		return
	}

	account.Balance += amount
	account.TransactionHistory = append(account.TransactionHistory, Transaction{
		Date:   time.Now(),
		Amount: amount,
		Type:   "Deposit",
	})

	fmt.Printf("Deposited $%.2f. New Balance: $%.2f\n", amount, account.Balance)
}

func (account *BankAccount) Withdraw(amount float64) {
	if amount <= 0 {
		fmt.Println("Invalid withdrawal amount")
		return
	}
	if account.Balance < amount {
		fmt.Println("Insufficient balance")
		return
	}

	// if account.Balance == amount {
	// 	fmt.Println("Minimum Balance should be 500")
	// 	return
	// }

	account.Balance -= amount
	account.TransactionHistory = append(account.TransactionHistory, Transaction{
		Date:   time.Now(),
		Amount: amount,
		Type:   "Withdraw",
	})

	fmt.Printf("Withdrew $%.2f. New Balance: $%.2f\n", amount, account.Balance)
}

func (account *BankAccount) ViewHistory() {
	fmt.Println("\nTransaction History:")
	for _, txn := range account.TransactionHistory {
		fmt.Printf("%s - %s: $%.2f\n", txn.Date.Format("2006-01-02 15:04:05"), txn.Type, txn.Amount)
	}
}

func readFloat(prompt string) (float64, error) {
	var input string
	fmt.Print(prompt)
	fmt.Scanln(&input)

	amount, err := strconv.ParseFloat(input, 64)
	if err != nil {
		return 0, fmt.Errorf("Invalid input, please enter a valid number.")
	}
	return amount, nil
}

func Banking() {
	account := &BankAccount{}

	for {
		fmt.Println("\nBanking App")
		fmt.Println("1. Deposit")
		fmt.Println("2. Withdraw")
		fmt.Println("3. View History")
		fmt.Println("4. Exit")
		fmt.Print("Choose an option: ")

		var choice int
		_, err := fmt.Scanln(&choice)
		if err != nil {
			fmt.Println("Invalid input, please enter a number.")
			continue
		}

		switch choice {
		case 1:
			amount, err := readFloat("Enter deposit amount: $")
			if err != nil {
				fmt.Println(err)
				continue
			}
			account.Deposit(amount)

		case 2:
			amount, err := readFloat("Enter withdrawal amount: $")
			if err != nil {
				fmt.Println(err)
				continue
			}
			account.Withdraw(amount)

		case 3:
			account.ViewHistory()

		case 4:
			fmt.Println("Goodbye!")
			return

		default:
			fmt.Println("Invalid option. Please choose a valid option.")
		}
	}
}
