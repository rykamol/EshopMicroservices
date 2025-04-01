namespace Catelog.API.Products.GetProductByCategory
{
	// this record is useless, added just to understand the standard steps to CQRS pattern.
	//public record GetProductByCategoryRequest(string category);  
	public record GetProductByCategoryResponse(IEnumerable<Product> Products);

	public class GetProductByCategoryEndPoint : ICarterModule
	{
		public void AddRoutes(IEndpointRouteBuilder app)
		{
			app.MapGet("products/category/{category}",
			   async (string category, ISender sender) =>
			   {
				   var result = await sender.Send(new GetProductByCategoryQuery(category));
				   var response = result.Adapt<GetProductByCategoryResponse>();
				   return Results.Ok(response);
			   })
		   .WithName("GetProductByCategory")
		   .Produces<GetProductByCategoryResponse>(StatusCodes.Status200OK)
		   .ProducesProblem(StatusCodes.Status400BadRequest)
		   .WithSummary("Get Products by Category")
		   .WithDescription("Get Products by Category");

		}
	}
}
