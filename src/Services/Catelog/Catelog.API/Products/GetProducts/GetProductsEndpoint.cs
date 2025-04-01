
using Catelog.API.Models;
using Catelog.API.Products.CreateProduct;

namespace Catelog.API.Products.GetProducts
{
	public class GetProductsEndpoint : ICarterModule
	{
		//public record GetProductsRequest
		public record GetProductsResponse(IEnumerable<Product> Products);
		public void AddRoutes(IEndpointRouteBuilder app)
		{
			app.MapGet("products/",
				async (ISender sender) =>
				{
					var result = await sender.Send(new GetProductsQuery());
					var response = result.Adapt<GetProductsResponse>();

					return Results.Ok(response);
				})
			.WithName("GetProducts")
			.Produces<CreateProductResponse>(StatusCodes.Status200OK)
			.ProducesProblem(StatusCodes.Status400BadRequest)
			.WithSummary("Get Products")
			.WithDescription("Get Products");
		}
	}
}
