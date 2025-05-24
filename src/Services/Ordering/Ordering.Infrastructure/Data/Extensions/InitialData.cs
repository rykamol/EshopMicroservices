namespace Ordering.Infrastructure.Data.Extensions
{
    internal class InitialData
    {
        public static IEnumerable<Customer> Customers = new List<Customer>()
            {
                Customer.Create(CustomerId.Of(new Guid("34ce2bf5-bd3e-4441-ad28-7ace7c85ae28")),"Kamol","kamol@sample.com"),
                Customer.Create(CustomerId.Of(new Guid("827a933a-6eed-4914-9bd7-2d165efbadf2")),"Roy","roy@sample.com")
            };
    }
}
