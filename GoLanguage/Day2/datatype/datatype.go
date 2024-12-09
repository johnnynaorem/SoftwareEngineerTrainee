package datatype

import "fmt"

//string
var (
	lastName = "Naorem";
	firstName = "Johnny"
)

func Declaration(){
	// Number
	//Declaration Style

	//First Way (var identifier dataType = value)
	var number1 int = 10

	//Second Way (var identifier = value)
	var number2 = 20.0
	number2 = 20.1

	//Third Way and Short Way ((identifier) := value)
	number3 := 10.10

	fmt.Println(firstName + " " + lastName)
	fmt.Println(number1, number2, number3)
	fmt.Printf("First: %T\nSecond: %T\nThird: %T\n", number1, number2, number3)

	result := sum(20,30)
	fmt.Println("The result of sum is: ",result)


	//boolean
	var isTrue = false
	fmt.Println(isTrue)
	fmt.Println(!isTrue)

}

func sum (x int, y int) int {
	return x + y
}
