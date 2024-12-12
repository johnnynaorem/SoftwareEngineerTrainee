package structs

import "fmt"

type Vehicle struct {
	typeOfVehicle string
	NameOfVehicle string
	Model         string
}

func NewVehicle(typeOfVehicle string, name string, model string) Vehicle {
	return Vehicle{typeOfVehicle: typeOfVehicle, NameOfVehicle: name, Model: model}
}

// ? Receiver Funtions
func (vehicle Vehicle) GetVehicleName () string {
	return vehicle.NameOfVehicle;
}

func (vehicle *Vehicle) UpdateVehicleName (updateName string){
	vehicle.NameOfVehicle = updateName;
}

func Structs() {
	vehicle := Vehicle{typeOfVehicle: "Car", NameOfVehicle: "Galardo", Model: "2024-01-02 15:04:0"}
	fmt.Println(vehicle)
}