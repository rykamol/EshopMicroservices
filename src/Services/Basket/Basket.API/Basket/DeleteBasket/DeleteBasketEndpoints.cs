﻿
using Basket.API.Basket.StoreBasket;

namespace Basket.API.Basket.DeleteBasket
{
	//public record DeleteBasketRequest(string UserName);
	public record DeleteBasketResponse(bool IsSuccess);
	public class DeleteBasketEndpoints : ICarterModule
	{
		public void AddRoutes(IEndpointRouteBuilder app)
		{
			app.MapDelete("/basket/{userName}", async (string userName, ISender sender) =>
			{
				var result = await sender.Send(new DeleteBasketCommand(userName));
				var response = result.Adapt<DeleteBasketResponse>();
				return Results.Ok(response);
			})
			.WithName("DeleteBasket")
			.Produces<DeleteBasketResponse>(StatusCodes.Status201Created)
			.ProducesProblem(StatusCodes.Status400BadRequest)
			.WithSummary("Delee basket")
			.WithDescription("Delete basket");
		}
	}
}
