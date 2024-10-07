namespace PizzaStore.Models.DTOs
{
    public class PizzaCartDTO : IEquatable<PizzaCartDTO>
    {
        public int SLNo { get; set; }
        public int PizzaId { get; set; }
        public int Quantity { get; set; }
        public float Price { get; set; }
        public float Discount { get; set; }

        public bool Equals(PizzaCartDTO? other)
        {
            return (this ?? new PizzaCartDTO()).SLNo == (other ?? new PizzaCartDTO()).SLNo;
        }
    }

}
