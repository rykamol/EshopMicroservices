
using Catelog.API.Products.GetProductById;

namespace Catelog.API.Products.DeleteProduct
{
	//public record DeleteProductRequest(Guid Id);
	public record DeleteProductResponse(bool IsSuccess);

	public class DeleteProductEndpoint : ICarterModule
	{
		public void AddRoutes(IEndpointRouteBuilder app)
		{
			app.MapDelete("products/{id}",
			   async (Guid id, ISender sender) =>
			   {
				   var result = await sender.Send(new DeleteProductCommand(id));
				   var response = result.Adapt<DeleteProductResponse>();

				   return Results.Ok(response);
			   })
		   .WithName("DeleteProductById")
		   .Produces<GetProductsByIdResponse>(StatusCodes.Status200OK)
		   .ProducesProblem(StatusCodes.Status400BadRequest)
		   .WithSummary("Delete Products by Id")
		   .WithDescription("Delete Products by Id");
		}
	}
}
