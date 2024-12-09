package datatype

import "fmt"

func FunctionDeclaration(){

	//Single value Return Function Calling
	result := Multiply(2,10);
	fmt.Println(result)

	//Multiple Value Return Function Calling
	//Handling error also
	err, retun := BinaryShifting(2,3);
	if(err != ""){
		println(err)
	}else{
		println(retun)
	}

	//Return function from another Funtion
	returnFunction := firstFunction(1,2);
	println(returnFunction())

	// IIFE Immediate Invoke Funtion(Self Calling After Declataion)
	func(){
		fmt.Println("This is from IIFE")
	}();
}

//Single Value Return Funtion
func Multiply(x int, y int) int {
	return x * y
}

//Multiple Value Return Function
func BinaryShifting (x int, y int) (string, int){
	if(x < 0){
		return "x should be more than or equal to 0", -1
	}
	return "", x >> 1
}

//Function inside Funtion 
func firstFunction(x int, y int) func() int {
	return func() int {
		return x * y
	}
}

