namespace PizzaStore.Models
{
    public class Cart : IEquatable<Cart>
    {
        public int CartNumber {  get; set; }
        public int CustomerId { get; set; }
        public List<int> Pizzas { get; set; }

        public Cart() { 
            Pizzas = new List<int>();
        }

        public bool Equals(Cart? other)
        {
            return (this ?? new Cart()).CartNumber == (other ?? new Cart()).CartNumber;
        }
    }
}
