using BuildingBlocks.CQRS;
using Catelog.API.Models;
using MediatR;
using System.Windows.Input;

namespace Catelog.API.Products.CreateProduct
{
	public record CreateProductCommand(
		string name,
		List<string> category,
		string description,
		string imageFile,
		decimal price) : ICommand<CreateProductResult>;

	public record CreateProductResult(Guid Guid);
	internal class CreateProducCommandtHandler : ICommandHandler<CreateProductCommand, CreateProductResult>
	{
		public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
		{
			//Business logic  
			//Create a product entity from command object
			var product = new Product
			{
				Name = command.name,
				Category = command.category,
				Description = command.description,
				ImageFile = command.imageFile,
				Price = command.price
			};
			//save to database

			//return the create product result 

			return new CreateProductResult(Guid.NewGuid());
		}
	}
}
