namespace Catelog.API.Products.GetProductById
{
	public record GetProductByIdQuery(Guid Id) : IQuery<GetProductByIdResult>;
	public record GetProductByIdResult(Product Product);

	public class GetProductByQueryIdHandler(IDocumentSession session, ILogger<GetProductByQueryIdHandler> logger)
		: IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
	{
		public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
		{
			logger.LogInformation("GetProductByQueryIdHandler.Handle called with {Query}", query);

			var product = await session.LoadAsync<Product>(query.Id, cancellationToken);
			if (product is null)
			{
				throw new ProductNotFoundException(query.Id);
			}
			return new GetProductByIdResult(product);
		}
	}
}
