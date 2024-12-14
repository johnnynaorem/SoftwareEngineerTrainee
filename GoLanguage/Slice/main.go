package main

import (
	"fmt"
	"sort"
)

func main() {
	fmt.Println("Slice")

	slice := make([]int, 4)

	slice[0] = 123
	slice[1] = 213
	slice[2] = 112
	slice[3] = 241

	slice = append(slice, 411, 522)

	fmt.Println(slice)
	sort.Ints(slice)
	fmt.Println(slice)
	fmt.Println(sort.IntsAreSorted(slice))
	fmt.Printf("The Lenght of slice: %d \n", len(slice))

	//remove a value from slices based on index

	skills := []string{"Go", "JS", "reactjs", "sql", "c#"}

	var index = 2

	skills = append(skills[:index], skills[index+1:]...)

	fmt.Println(skills)
}
