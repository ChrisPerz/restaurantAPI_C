namespace RestaurantBookingAPI.Models
{
    public class OrderRequest
    {
        public int TableId { get; set; }
        public int CustomerId { get; set; }
        public int[] DishIds { get; set; }
        public string Dates { get; set; }
        public int Num_people { get; set; }
    }
}
    