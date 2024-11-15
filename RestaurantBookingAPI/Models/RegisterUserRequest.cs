namespace RestaurantBookingAPI.Models
{
    public class RegisterUserRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public int PhoneNumber { get; set; }
        public string Name { get; set; }
        public string UserType { get; set; } = "customer";
    }
}
