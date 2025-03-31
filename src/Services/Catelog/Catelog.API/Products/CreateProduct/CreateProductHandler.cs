using MediatR;

namespace Catelog.API.Products.CreateProduct
{
	public record CreateProductCommand (
		string name,
		List<string> category,
		string description,
		string imageFile,
		decimal price) : IRequest<CreateProductResult>;

	public record CreateProductResult(Guid Guid);
	internal class CreateProducCommandtHandler : IRequestHandler<CreateProductCommand, CreateProductResult>
	{
		public Task<CreateProductResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
		{
			//Business logic  
			throw new NotImplementedException();
		}
	}
}
