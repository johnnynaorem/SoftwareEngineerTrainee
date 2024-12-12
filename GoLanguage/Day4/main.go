package main

import "fmt"

func main(){
	// var result = count(2);

	
	// var result = strStr("sadbutsad", "sad")
	// var result = twoSum([]int{2,5,5,11}, 10)
	var result = InsertionSort([]int{2,6,5,11})
	fmt.Println(result)
}

func count(n int) []int {
	arr := make([]int, n+ 1)
	for i := 0; i <= n; i++ {
		arr[i] = getCount(i)
	}
	return arr
 }
 func getCount(n int) int{
	count := 0
	for n != 0 {
		if (n % 2 == 1) {
			count++
		}
		n = n >> 1
	}
	return count;
 }

 func strStr(haystack string, needle string) int {
    for i := 0; i <= len(haystack)-len(needle); i++ {
		if haystack[i:i+len(needle)] == needle {
			return i
		}
	}
	return -1
 }

 func twoSum(nums []int, target int) []int {
    for i:=0; i< len(nums); i++{
		for j := i+1; j< len(nums); j++ {
			if nums[i]+nums[j] == target{
				return []int{i,j}
			}
		}
	}
	return []int{-1,-1}
 }

 // ? Sorting

 // ?Bubble Sort
// ? Time Complexity: O(n^2)
 func BubbleSort(arr []int) []int {
	for i := 0; i < len(arr)-1; i++ {
		isSwap := false
		for j := 0; j < len(arr)-i-1; j++ {
			if arr[j] > arr[j+1]{
				var temp = arr[j]
				arr[j] = arr[j+1]
				arr[j+1] = temp
				isSwap = true
			}
		}
		if !isSwap{
			return arr
		}
	}
	return arr;
 }

 //Selection Sort

 //Time Complexity: O(n^2)
 func SelectionSort(arr []int) []int {

	for i := 0; i < len(arr)-1; i++ {
		smallestValueIndex := i

		for j := i+1; j < len(arr); j++ {
			if arr[j] < arr[smallestValueIndex]{
				smallestValueIndex = j
			}
		}

		var temp = arr[i]
		arr[i] = arr[smallestValueIndex]
		arr[smallestValueIndex] = temp
	}

	return arr
 }


 // Insertion Sort
//  ? Time Complexity = O(n^2)
 func InsertionSort(arr []int) []int {
	for i := 1; i < len(arr)-1; i++ {
		current := arr[i]
		prev := 0

		for prev>=0 && current < arr[prev] {
			arr[prev+1] = arr[prev]
			prev --
		}
		arr[prev+1] = current
	}
	return arr;
 }

