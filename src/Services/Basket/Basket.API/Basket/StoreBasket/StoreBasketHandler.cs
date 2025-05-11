
using Discount.Grpc;
using static Discount.Grpc.DiscountProtoService;

namespace Basket.API.Basket.StoreBasket
{
	public record StoreBasketCommand(ShoppingCart Cart) : ICommand<StoreBasketResult>;
	public record StoreBasketResult(string UserName);
	public class StoreBasketCommandValidator : AbstractValidator<StoreBasketCommand>
	{
		public StoreBasketCommandValidator()
		{
			RuleFor(x => x.Cart).NotNull().WithMessage("Cart can not be null!");
			RuleFor(x => x.Cart.UserName).NotNull().NotEmpty().WithMessage("Username is required!");
		}
	}

	public class StoreBasketCommandHandler(
		IBasketRepository repository, DiscountProtoServiceClient discountProto)
		: ICommandHandler<StoreBasketCommand, StoreBasketResult>
	{
		public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
		{
			await DeductDiscount(discountProto, command, cancellationToken);


			//Todo: Store basket in databse (use marten upsert -if exist = update , if not =add )
			await repository.StoreBasket(command.Cart, cancellationToken);

			return new StoreBasketResult(command.Cart.UserName);
		}

		private static async Task DeductDiscount(DiscountProtoServiceClient discountProto, StoreBasketCommand command, CancellationToken cancellationToken)
		{
			//TODO: communicate with Discount.Grpc and canculate lastest prices or product
			foreach (var item in command.Cart.Items)
			{
				var coupon = await discountProto.GetDiscountAsync(
					new GetDiscountRequest { ProductName = item.ProductName }, cancellationToken: cancellationToken); ;

				item.Price -= coupon.Amount;
			}
		}	 
	}
}
