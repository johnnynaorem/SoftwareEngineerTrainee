package featureCalculator

import (
	"fmt"
	// "math"
)

func Calculator(){
	var n int
	var pv, interestRate, periodicDeposit float64;
	var endBalance float64;
	var totalInterest float64
	var totalPeriodicDeposit float64
	var featureValue float64

	fmt.Printf("Enter Number of Period: ")
	fmt.Scanln(&n)

	fmt.Printf("Enter Starting Amount: ")
	fmt.Scanln(&pv) 

	fmt.Printf("Interest Rate: ")
	fmt.Scanln(&interestRate) 

	fmt.Printf("Periodic Deposit: ")
	fmt.Scanln(&periodicDeposit) 

	println("Period\t\t", "Starting_Balance \t", "Deposit \t", "Interest \t\t", "End Balance")
	for i := 1; i <= n; i++ {
		startBalanceStr := fmt.Sprintf("%.2f", pv)
		interest := (pv * interestRate)/100
		totalInterest += interest;
		interestStr := fmt.Sprintf("%.3f", interest)
		periodicDepositStr := fmt.Sprintf("%.2f", periodicDeposit)
		totalPeriodicDeposit = totalPeriodicDeposit + 500 
		endBalance = pv + interest + periodicDeposit
		pv = endBalance;
		endBalanceStr := fmt.Sprintf("%.2f", endBalance)
		featureValue = endBalance
		println( i,"\t\t", startBalanceStr, "\t\t",  periodicDepositStr, "\t", interestStr, "\t\t", endBalanceStr)
	}
	totalPeriodicDepositStr := fmt.Sprintf("%.2f", totalPeriodicDeposit)
	totalInterestStr := fmt.Sprintf("%.2f", totalInterest)
	
	featureValueStr := fmt.Sprintf("%.2f", featureValue)
	println("")

	println("Feature Value: ", featureValueStr)
	println("Total Periodic Deposits: ", totalPeriodicDepositStr)
	println("Total Interest: ", totalInterestStr)
}