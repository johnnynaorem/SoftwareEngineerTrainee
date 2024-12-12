package main

import "fmt"

func Cleaning(channel chan string) {
	fmt.Println("Start Cleaning")
	fmt.Println("End Cleaning")
	channel <- "I am Cleaning"

}

func main() {
	fmt.Println("Start Main Function")
	channel := make(chan string)
	go Cleaning(channel)

	fmt.Println(<- channel)
	fmt.Println("Complete Main Function")
}