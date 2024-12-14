package main

import "fmt"

func main() {
	fmt.Println("About Map: Store in key value format")

	vehicles := make(map[string]string)

	vehicles["car"] = "BMW"
	vehicles["motobike"] = "Classic 350"
	vehicles["truck"] = "Shaktiman"

	fmt.Println("List of all the vehicles: ", vehicles)
	fmt.Println("List of car vehicles: ", vehicles["car"])

	delete(vehicles, "car")
	fmt.Println("List of all the vehicles: ", vehicles)

	for key, vehicle := range vehicles {
		fmt.Printf("key: %s, Value : %s", key, vehicle)
	}
}
