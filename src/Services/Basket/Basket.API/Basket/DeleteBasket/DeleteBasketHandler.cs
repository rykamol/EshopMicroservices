﻿
namespace Basket.API.Basket.DeleteBasket
{
	public record DeleteBasketCommand(string UserName) : ICommand<DeleteBasketResult>;
	public record DeleteBasketResult(bool IsSuccess);
	public class DeleteBasketCommandValidator : AbstractValidator<DeleteBasketCommand>
	{
		public DeleteBasketCommandValidator()
		{
			RuleFor(x => x.UserName).NotEmpty().WithMessage("Username is required!");
		}
	}

	public class DeleteBasketCommandHandler(IBasketRepository repository) : ICommandHandler<DeleteBasketCommand, DeleteBasketResult>
	{
		public async Task<DeleteBasketResult> Handle(DeleteBasketCommand command, CancellationToken cancellationToken)
		{
			//Todo: Delete basket from database and cache
			await repository.DeleteBasket(command.UserName);
			return new DeleteBasketResult(true);
		}
	}
}
