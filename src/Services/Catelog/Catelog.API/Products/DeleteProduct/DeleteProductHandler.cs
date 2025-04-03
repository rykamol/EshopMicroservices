﻿using Catelog.API.Products.UpdateProduct;

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

	public class DeleteProductCommandHandler(IDocumentSession session)
		: ICommandHandler<DeleteProductCommand, DeleteProductResult>
	{
		public async Task<DeleteProductResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
		{
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
