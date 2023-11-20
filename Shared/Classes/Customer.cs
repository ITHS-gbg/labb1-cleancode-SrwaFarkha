namespace Shared.Classes
{
    public class Customer
    {
        public int Id { get; init; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public Customer(string name, string password, string email)
        {
            Name = name;
            Password = password;
            Email = email;
        }

        public Customer()
        {
	        
        }
    }
}