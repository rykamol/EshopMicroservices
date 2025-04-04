

namespace Basket.API.Basket.GetBasket
{
	public record GetBasketQuery(string UserName) : IQuery<GetBasketResult>;
	public record GetBasketResult(ShoppingCart Cart) ;
	public class GetBasketQueryHandler : IQueryHandler<GetBasketQuery, GetBasketResult>
	{
		public async Task<GetBasketResult> Handle(GetBasketQuery query, CancellationToken cancellationToken)
		{
			// Todo: get basket form database
			//var basket = await _reposity.GetBasket(query.UserName);
			
			return new GetBasketResult(new ShoppingCart("mock"));
		}
	}


}
