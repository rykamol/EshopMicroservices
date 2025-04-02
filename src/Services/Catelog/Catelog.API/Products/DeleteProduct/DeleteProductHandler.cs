using Catelog.API.Products.UpdateProduct;

namespace Catelog.API.Products.DeleteProduct
{
	public record DeleteProductCommand(Guid Id) : ICommand<DeleteProductResult>;
	public record DeleteProductResult(bool IsSuccess);
	public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
	{
		public DeleteProductCommandValidator()
		{
			RuleFor(x => x.Id).NotEmpty().WithMessage("Product Id is required!");
		}
	}

	public class DeleteProductCommandHandler(IDocumentSession session, ILogger<DeleteProductCommandHandler> logger)
		: ICommandHandler<DeleteProductCommand, DeleteProductResult>
	{
		public async Task<DeleteProductResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
		{
			logger.LogInformation("DeleteProductCommandHandler.Handle called with {command}", command);

			var product = await session.LoadAsync<Product>(command.Id, cancellationToken);
			if (product is null)
			{
				throw new ProductNotFoundException(command.Id);
			}
			session.Delete(product);
			//session.Delete<Product>(command.Id);     //This methos also works
			await session.SaveChangesAsync(cancellationToken);

			return new DeleteProductResult(true);
		}
	}
}
