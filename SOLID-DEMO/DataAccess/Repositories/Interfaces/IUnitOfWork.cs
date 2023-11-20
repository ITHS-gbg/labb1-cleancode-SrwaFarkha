namespace Server.DataAccess.Repositories.Interfaces
{
	public interface IUnitOfWork
	{
		ICustomerRepository CustomerRepository { get; }
		IOrderRepository OrderRepository { get; }
		IProductRepository ProductRepository { get; }
	}
}
