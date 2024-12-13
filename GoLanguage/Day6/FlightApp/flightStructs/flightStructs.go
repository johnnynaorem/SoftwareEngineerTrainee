package flightstructs

import (
	"fmt"
	"sort"
)

type FlightStruct struct {
	flightNumber  string
	departureFrom string
	arrivalTo     string
	flightDate    string
	fair          float64
}

type Flights struct {
	flights []FlightStruct
}

func (flight *Flights) getAllFlight() {
	fmt.Println("All Flights:")
	fmt.Println("Flight Number\t", "Departure\t", "Arrival\t", "Fair\t", "Date")
	for _, txn := range flight.flights {
		fmt.Println(txn.flightNumber, "\t\t", txn.departureFrom, " \t", txn.arrivalTo, "         ", txn.fair, "  ", txn.flightDate)
	}
}

func searchFlight(flights *Flights, departureFrom string, arrivalTo string) Flights {
	var sortFlight Flights
	for _, txn := range flights.flights {
		if txn.departureFrom == departureFrom && txn.arrivalTo == arrivalTo {
			sortFlight.flights = append(sortFlight.flights, txn)
		}
	}
	return sortFlight
}

func createFlightStruct(flights *Flights) {
	flights.flights = append(flights.flights,
		FlightStruct{
			flightNumber:  "AE123",
			departureFrom: "Imphal",
			arrivalTo:     "Delhi",
			flightDate:    "15-12-2024/10:30",
			fair:          2799,
		},
		FlightStruct{
			flightNumber:  "AE124",
			departureFrom: "Delhi",
			arrivalTo:     "Kolkata",
			flightDate:    "16-12-2024/09:00",
			fair:          2999,
		},
		FlightStruct{
			flightNumber:  "AE125",
			departureFrom: "Kolkata",
			arrivalTo:     "Mumbai",
			flightDate:    "17-12-2024/10:00",
			fair:          3590,
		},
		FlightStruct{
			flightNumber:  "AE126",
			departureFrom: "Imphal",
			arrivalTo:     "Delhi",
			flightDate:    "15-12-2024/20:30",
			fair:          2499,
		},
	)
}

func SortByFair(flights *Flights) {
	sort.Slice(flights.flights, func(i, j int) bool {
		return flights.flights[i].fair > flights.flights[j].fair
	})
	flights.getAllFlight()
}

func DeleteFlight(flights *Flights, flightNumber string, flightDate string) {
	fmt.Println(flightDate, flightNumber)
	for index, txn := range flights.flights {
		if txn.flightNumber == flightNumber && txn.flightDate == flightDate {
			// slice1 := flights.flights[0:index]
			// slice2 := flights.flights[index+1:]
			// mergedSlice := append(slice1, slice2...)
			// fmt.Println(mergedSlice)
			// flights.flights = mergedSlice
			flights.flights = append(flights.flights[:index], flights.flights[index+1:]...)
		}
	}
}

func adminMenu(flights *Flights) {
	var choice int
	for {
		fmt.Println("\nAdmin Menu:")
		fmt.Println("1. See All Flights")
		fmt.Println("2. Add Flight")
		fmt.Println("3. Delete Flight")
		fmt.Println("0. Exit")
		fmt.Print("Choose an option: ")
		fmt.Scanln(&choice)

		switch choice {
		case 1:
			flights.getAllFlight()

		case 2:
			var flightNumber, departureFrom, arrivalTo, flightDate string
			var fair float64
			fmt.Print("Enter Flight Number: ")
			fmt.Scanln(&flightNumber)
			fmt.Print("Enter Departure City: ")
			fmt.Scanln(&departureFrom)
			fmt.Print("Enter Arrival City: ")
			fmt.Scanln(&arrivalTo)
			fmt.Print("Enter Flight Date: ")
			fmt.Scanln(&flightDate)
			fmt.Print("Enter Flight Fare: ")
			fmt.Scanln(&fair)

			newFlight := FlightStruct{
				flightNumber:  flightNumber,
				departureFrom: departureFrom,
				arrivalTo:     arrivalTo,
				flightDate:    flightDate,
				fair:          fair,
			}
			flights.flights = append(flights.flights, newFlight)
			fmt.Println("Flight added successfully.")

		case 3:
			var fligntNumber, flightDate string
			fmt.Print("Enter the Flight Number to be delete: ")
			fmt.Scanln(&fligntNumber)
			fmt.Print("Enter the Flight Date to be delete: ")
			fmt.Scanln(&flightDate)
			DeleteFlight(flights, fligntNumber, flightDate)
		case 0:
			fmt.Println("Admin Logged Out.")
			return

		default:
			fmt.Println("Invalid option. Please choose a valid option.")
		}
	}
}

func customerMenu(flights *Flights) {
	var choice int
	for {
		fmt.Println("\nCustomer Menu:")
		fmt.Println("1. See All Flights")
		fmt.Println("2. Search Flight")
		fmt.Println("3. Sort By Flight Fair")

		fmt.Println("0. Exit")
		fmt.Print("Choose an option: ")
		fmt.Scanln(&choice)

		switch choice {
		case 1:
			flights.getAllFlight()

		case 2:
			var departureFrom, arrivalTo string
			fmt.Print("From: ")
			fmt.Scanln(&departureFrom)
			fmt.Print("To: ")
			fmt.Scanln(&arrivalTo)
			if departureFrom != "" && arrivalTo != "" {
				result := searchFlight(flights, departureFrom, arrivalTo)
				if len(result.flights) > 0 {
					fmt.Println("\nMatching Flights:")
					for _, txn := range result.flights {
						fmt.Println(txn.flightNumber, txn.departureFrom, txn.arrivalTo, txn.fair, txn.flightDate)
					}

					var sortChoice int
					fmt.Println("\n1. Sort by Price")
					fmt.Println("2. Sort by Flight Date")
					fmt.Println("0. Exit")
					fmt.Print("Choose an option: ")
					fmt.Scanln(&sortChoice)

					switch sortChoice {
					case 1:
						sort.Slice(result.flights, func(i, j int) bool {
							return result.flights[i].fair > result.flights[j].fair
						})
						fmt.Println("\nSorted by Price:")
						for _, txn := range result.flights {
							fmt.Println(txn.flightNumber, txn.departureFrom, txn.arrivalTo, txn.fair, txn.flightDate)
						}
					case 2:
						sort.Slice(result.flights, func(i, j int) bool {
							return result.flights[i].flightDate > result.flights[j].flightDate
						})
						fmt.Println("\nSorted by Flight Date:")
						for _, txn := range result.flights {
							fmt.Println(txn.flightNumber, txn.departureFrom, txn.arrivalTo, txn.fair, txn.flightDate)
						}
					}
				} else {
					fmt.Println("\nNo matching flights found.")
				}
			}

		case 3:
			SortByFair(flights)

		case 0:
			fmt.Println("Goodbye!")
			return

		default:
			fmt.Println("Invalid option. Please choose a valid option.")
		}
	}
}

func FlightMainFunction() {
	flights := Flights{}
	fmt.Println("\nFlight Booking App")
	createFlightStruct(&flights)

	var userType int
	fmt.Println("\n1. Admin")
	fmt.Println("2. Customer")
	fmt.Print("Choose user type: ")
	fmt.Scanln(&userType)

	switch userType {
	case 1:
		adminMenu(&flights)
	case 2:
		customerMenu(&flights)
	default:
		fmt.Println("Invalid option. Please choose a valid user type.")
	}
}
