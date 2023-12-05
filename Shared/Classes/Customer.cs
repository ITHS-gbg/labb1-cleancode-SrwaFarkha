namespace Shared.Classes

{
    public class Customer
    {
        public int Id { get; init; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Enums.Enums.CustomerLevel Level { get; set; }
        public double DiscountRate { get; set; }


		public Customer(int id, string name, string password, string email, Enums.Enums.CustomerLevel level)
        {
            Id = id;
            Name = name;
            Password = password;
            Email = email;
            Level = level;
        }

		public Customer(string name, string password, string email, Enums.Enums.CustomerLevel level)
		{
			Name = name;
			Password = password;
			Email = email;
			Level = level;
		}

		public Customer(string email, string password)
		{
			Email = email;
			Password = password;
		}

		public Customer()
        {

        }


    }

   
}