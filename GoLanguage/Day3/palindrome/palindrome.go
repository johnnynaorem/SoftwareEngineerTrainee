package palindrome

import "fmt"

func checkPalindrome(){
	var n string;
	fmt.Println("Enter number: ");
	fmt.Scanf("%s", &n);

	result := reverse(n)
	if(result == n){
		fmt.Println("Number is palindrome");
	}else{
		fmt.Println("Number is not palindrome");
	}
}

func reverse(s string) string {
    var reversed string
    for _, v := range s {
        reversed = string(v) + reversed
    }
    return reversed
}