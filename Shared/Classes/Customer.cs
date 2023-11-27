namespace Shared.Classes
{
    public class Customer
    {
        public int Id { get; init; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public CustomerType Level { get; set; } = CustomerType.Standard;

        public Customer(int id, string name, string password, string email)
        {
            Id = id;
            Name = name;
            Password = password;
            Email = email;
        }

        public Customer()
        {
	        
        }

        public enum CustomerType
        {
	        Standard,
	        Premium,
	        VIP
        }
	}
}