﻿
using Catelog.API.Models;
using Catelog.API.Products.CreateProduct;

namespace Catelog.API.Products.UpdateProduct
{
	public record UpdateProductRequest(
		Guid id,
		string name,
		List<string> category,
		string description,
		string imageFile,
		decimal price);

	public record UpdateProductResponse(Boolean isSuccess);
	public class UpdateProductEndpoint : ICarterModule
	{
		public void AddRoutes(IEndpointRouteBuilder app)
		{
			app.MapPut("/products",
				async (UpdateProductRequest request, ISender sender) =>
				{
					var command = request.Adapt<UpdateProductCommand>();
					var result = await sender.Send(command);
					var response = result.Adapt<UpdateProductResponse>();

					return Results.Ok(response);

				})
			.WithName("UpdateProduct")
			.Produces<UpdateProductResponse>(StatusCodes.Status200OK)
			.ProducesProblem(StatusCodes.Status400BadRequest)
			.ProducesProblem(StatusCodes.Status404NotFound)
			.WithSummary("Update Product")
			.WithDescription("Product update successfully!");
		}
	}
}
