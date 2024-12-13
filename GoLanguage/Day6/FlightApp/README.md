# Flight Booking App

## Overview
The **Flight Booking App** is a simple flight management system that allows users to interact with flight data. The app is designed with two user roles: **Admin** and **Customer**. Admins can add, delete, and view flights, while customers can search, view, and sort flights by fare or flight date. This app provides a straightforward way to manage and book flights.

## Features

### Admin Features:
- **View All Flights**: Admins can see the list of all flights available.
- **Add New Flights**: Admins can add new flights by providing flight details such as flight number, departure city, arrival city, flight date, and fare.
- **Delete Flights**: Admins can delete specific flights by providing the flight number and date.

### Customer Features:
- **View All Flights**: Customers can view the entire list of available flights.
- **Search Flights**: Customers can search for flights based on departure and arrival cities.
- **Sort Search Results**: Customers can sort the search results either by fare or flight date to help in making better decisions.

## Data Structure

### FlightStruct
`FlightStruct` represents a single flight with the following fields:
- `flightNumber`: The flight number (string).
- `departureFrom`: The city of departure (string).
- `arrivalTo`: The city of arrival (string).
- `flightDate`: The date and time of the flight (string).
- `fair`: The fare of the flight (float64).

### Flights
The `Flights` type is a collection of flights stored in a slice of `FlightStruct` objects.

## Functions

### Admin Functions
- **getAllFlight()**: Displays all available flights with their details.
- **createFlightStruct()**: Initializes the system with default flights when the application starts.
- **DeleteFlight()**: Deletes a flight based on the flight number and flight date.
- **adminMenu()**: Displays an interactive menu for the admin to perform actions like viewing, adding, or deleting flights.

### Customer Functions
- **getAllFlight()**: Displays all available flights.
- **searchFlight()**: Allows customers to search for flights based on departure and arrival cities.
- **SortByFair()**: Sorts flights by fare in descending order.
- **customerMenu()**: Provides a menu for customers to view, search, or sort flights.

### Main Function
**FlightMainFunction()**: This is the entry point of the application where users can choose whether they are an admin or a customer and proceed accordingly.

---

## Output

### Admin Menu:
```plaintext
1. See All Flights
2. Add Flight
3. Delete Flight
0. Exit
Choose an option: 1
Flight Number    Departure    Arrival    Fair    Date
AE123            Imphal       Delhi      2799    15-12-2024/10:30
AE124            Delhi        Kolkata    2999    16-12-2024/09:00
AE125            Kolkata      Mumbai     3590    17-12-2024/10:00
AE126            Imphal       Delhi      2499    15-12-2024/20:30


Admin Menu:
1. See All Flights
2. Add Flight
3. Delete Flight
0. Exit
Choose an option: 2

Enter Flight Number: AE127
Enter Departure City: Chennai
Enter Arrival City: Bangalore
Enter Flight Date: 18-12-2024/15:00
Enter Flight Fare: 2599

Flight added successfully.
The list of flights is updated:

All Flights:
Flight Number    Departure    Arrival    Fair    Date
AE123            Imphal       Delhi      2799    15-12-2024/10:30
AE124            Delhi        Kolkata    2999    16-12-2024/09:00
AE125            Kolkata      Mumbai     3590    17-12-2024/10:00
AE126            Imphal       Delhi      2499    15-12-2024/20:30
AE127            Chennai      Bangalore  2599    18-12-2024/15:00

Admin Menu:
1. See All Flights
2. Add Flight
3. Delete Flight
0. Exit
Choose an option: 3

Enter the Flight Number to be deleted: AE123
Enter the Flight Date to be deleted: 15-12-2024/10:30

Flight deleted successfully.
The flight list is updated:

All Flights:
Flight Number    Departure    Arrival    Fair    Date
AE124            Delhi        Kolkata    2999    16-12-2024/09:00
AE125            Kolkata      Mumbai     3590    17-12-2024/10:00
AE126            Imphal       Delhi      2499    15-12-2024/20:30
AE127            Chennai      Bangalore  2599    18-12-2024/15:00
```

### Customer Menu:
```plaintext
1. See All Flights
2. Search Flight
3. Sort By Flight Fair
0. Exit
Choose an option: 2

From: Imphal
To: Delhi

Matching Flights:
AE126  Imphal    Delhi      2499   15-12-2024/20:30
AE123  Imphal    Delhi      2799   15-12-2024/10:30

1. Sort by Price
2. Sort by Flight Date
0. Exit
Choose an option: 1
Sorted by Price:
AE123  Imphal    Delhi      2799   15-12-2024/10:30
AE126  Imphal    Delhi      2499   15-12-2024/20:30

Customer Menu:
1. See All Flights
2. Search Flight
3. Sort By Flight Fair
0. Exit
Choose an option: 3

Sorted by Price:
AE125  Kolkata   Mumbai     3590   17-12-2024/10:00
AE124  Delhi     Kolkata    2999   16-12-2024/09:00
AE123  Imphal    Delhi      2799   15-12-2024/10:30
AE127  Chennai   Bangalore  2599   18-12-2024/15:00
AE126  Imphal    Delhi      2499   15-12-2024/20:30
```


## Example Code
```plaintext

### main.go (or flight_booking.go)

go
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

