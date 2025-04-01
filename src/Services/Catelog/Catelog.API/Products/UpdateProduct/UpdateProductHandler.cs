
using Catelog.API.Products.CreateProduct;

namespace Catelog.API.Products.UpdateProduct
{
	public record UpdateProductCommand(
		Guid id,
		string name,
		List<string> category,
		string description,
		string imageFile,
		decimal price) : ICommand<UpdateProductResult>;

	public record UpdateProductResult(Boolean isSuccess);
	public class UpdateProductCommandHandler(IDocumentSession session, ILogger<UpdateProductCommandHandler> logger) : ICommandHandler<UpdateProductCommand, UpdateProductResult>
	{
		public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
		{
			logger.LogInformation("UpdateProductCommandHandler.Handle called with {Query}", command);

			//Business logic  
			//Create a product entity from command object

			var product = await session.LoadAsync<Product>(command.id, cancellationToken);
			
			if (product is null)
			{
				throw new ProductNotFoundException();
			}

			product.Name = command.name;
			product.Category = command.category;
			product.Description = command.description;
			product.ImageFile = command.imageFile;
			product.Price = command.price;

			//save to database
			session.Update(product);
			await session.SaveChangesAsync(cancellationToken);
			//return the create product result 

			return new UpdateProductResult(true);
		}
	}
}
