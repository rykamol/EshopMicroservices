namespace Ordering.Domain.ValueObjects
{
	public record CustomerId
	{
		private Guid Value;

        public CustomerId(Guid value) => Value = value; 
        public static CustomerId Of(Guid value)
        {
            ArgumentNullException.ThrowIfNull(value);

            if(value ==Guid.Empty)
            {
                throw new DomainException("Customer Id cannot be empty.");
            }

            return new CustomerId(value);
        }
    }
}
