namespace Catelog.API.Products.CreateProduct
{
	public record CreateProductCommand(
		string Name,
		List<string> Category,
		string Description,
		string ImageFile,
		decimal Price) : ICommand<CreateProductResult>;

	public record CreateProductResult(Guid Guid);

	internal class CreateProducCommandtHandler(IDocumentSession session, ILogger<CreateProducCommandtHandler> logger)
		: ICommandHandler<CreateProductCommand, CreateProductResult>
	{
		public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
		{
			//Write log
			logger.LogInformation("CreateProducCommandtHandler.Handle called with {command}", command);

 
			//Create a product entity from command object
			var product = new Product
			{
				Name = command.Name,
				Category = command.Category,
				Description = command.Description,
				ImageFile = command.ImageFile,
				Price = command.Price
			};

			//save to database
			session.Store(product);
			await session.SaveChangesAsync(cancellationToken);

			//return the create product result 
			return new CreateProductResult(product.Id);
		 
		}
	}
}
