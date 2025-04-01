namespace Catelog.API.Products.GetProductById
{
	//public record GetProductByIdRequest(Guid Id); 
	public record GetProductsByIdResponse(Product Product);

	public class GetProductByIdEndpoint : ICarterModule
	{
		public void AddRoutes(IEndpointRouteBuilder app)
		{
			app.MapGet("products/{id}",
			   async (Guid id, ISender sender) =>
			   {
				   var result = await sender.Send(new GetProductByIdQuery(id));
				   var response = result.Adapt<GetProductsByIdResponse>();

				   return Results.Ok(response);
			   })
		   .WithName("GetProductById")
		   .Produces<GetProductsByIdResponse>(StatusCodes.Status200OK)
		   .ProducesProblem(StatusCodes.Status400BadRequest)
		   .WithSummary("Get Products by Id")
		   .WithDescription("Get Products by Id");
		}
	}
}
