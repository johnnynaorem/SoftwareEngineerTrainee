package main

import (
	"fmt"
	"myModule/structs"
)

func main() { 

	structs.Structs()
	newVehicle := structs.NewVehicle("Car", "Galardo", "2024-01-02 15:04:0")
	fmt.Println(newVehicle)
	fmt.Println(newVehicle.GetVehicleName())

	newVehicle.UpdateVehicleName("Aventador")
	fmt.Println(newVehicle)
	fmt.Println(newVehicle.GetVehicleName())
	
}