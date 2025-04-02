using BuildingBlocks.Exceptions;

namespace Catelog.API.Exceptions
{
	public class ProductNotFoundException:NotFoundException
	{
        public ProductNotFoundException(Guid Id):base("Product",Id)
        {
            
        }
    }
}
