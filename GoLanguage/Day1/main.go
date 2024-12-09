package main

import (
	"fmt"
	johnny "myModule/Johnny"
)

func main(){
	var name string
	fmt.Print("Enter Your Last Name: ")
	fmt.Scanf("%s", &name)
	
	fmt.Println("Hello");
	johnny.Display();
	fmt.Print(" " + name);
}