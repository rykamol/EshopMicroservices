namespace Ordering.Infrastructure.Data.Extensions
{
    internal class InitialData
    {
        public static IEnumerable<Customer> Customers => new List<Customer>()
            {
                Customer.Create(CustomerId.Of(new Guid("34ce2bf5-bd3e-4441-ad28-7ace7c85ae28")),"Kamol","kamol@sample.com"),
                Customer.Create(CustomerId.Of(new Guid("827a933a-6eed-4914-9bd7-2d165efbadf2")),"John","john@sample.com")
            };
        public static IEnumerable<Product> Products => new List<Product>()
        {
            Product.Create(ProductId.Of(new Guid("796966c0-0c5a-4a16-97c2-834f3925d78c")),"Laptop",999),
            Product.Create(ProductId.Of(new Guid("d64fb1d9-cdb1-46f3-b2fb-40ea952a86c1")),"Smartphone",599),
            Product.Create(ProductId.Of(new Guid("394d16a5-3abf-4604-9329-000f7ba0e1b8")),"Monitor",399),
            Product.Create(ProductId.Of(new Guid("c0a88614-f42d-4e11-953c-39f81be58012")),"Headphones",199)
        };

        public static IEnumerable<Order> OrderWithItems
        {
            get
            {
                var address1 = Address.Of("Kamol", "Roy", "kamol@sample.com", "Dhaka,Bangladesh", "Bangladesh", "101", "1230");
                var address2 = Address.Of("John", "Doe", "jokh@sample.com", "Uganda,Holululu", "Uganda", "201", "9562");

                var payment1 = Payment.Of("Kamol", "371360744899146", "12/27", "568", "Visa");
                var payment2 = Payment.Of("John", "373538663518361", "03/28", "334", "American Express");

                var order1 = Order.Create(
                    id: OrderId.Of(Guid.NewGuid()),
                    customerId: CustomerId.Of(new Guid("34ce2bf5-bd3e-4441-ad28-7ace7c85ae28")),
                    orderName: OrderName.Of("Order-kamol"),
                    shippingAddress: address1,
                    billingAddress: address1,
                    payment1);


                order1.AddItem(ProductId.Of(new Guid("796966c0-0c5a-4a16-97c2-834f3925d78c")), 2, 999);
                order1.AddItem(ProductId.Of(new Guid("d64fb1d9-cdb1-46f3-b2fb-40ea952a86c1")), 5, 599);

                var order2 = Order.Create(
                    id: OrderId.Of(Guid.NewGuid()),
                    customerId: CustomerId.Of(new Guid("827a933a-6eed-4914-9bd7-2d165efbadf2")),
                    orderName: OrderName.Of("Order-john"),
                    shippingAddress: address2,
                    billingAddress: address2,
                    payment2);


                order2.AddItem(ProductId.Of(new Guid("394d16a5-3abf-4604-9329-000f7ba0e1b8")), 2, 399);
                order2.AddItem(ProductId.Of(new Guid("c0a88614-f42d-4e11-953c-39f81be58012")), 5, 199);
                
                return new List<Order> { order1,order2 };
            }
        }
    }
}
