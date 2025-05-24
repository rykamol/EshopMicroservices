
namespace Ordering.Domain.ValueObjects
{
	public record OrderName
	{
		public string Value { get; }
		public const int DefaultLength = 20;

		public OrderName(string value) => Value = value;
		public static OrderName Of(string value)
		{
			ArgumentNullException.ThrowIfNullOrWhiteSpace(value);
			ArgumentOutOfRangeException.ThrowIfGreaterThan(value.Length,DefaultLength);

			return new OrderName(value);
		}
	}
}
