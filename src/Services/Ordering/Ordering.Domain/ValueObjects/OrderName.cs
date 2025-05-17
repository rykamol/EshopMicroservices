
namespace Ordering.Domain.ValueObjects
{
	public record OrderName
	{
		public string Value { get; }
		public const int DefaultLength = 5;

		public OrderName(string value) => Value = value;
		public static OrderName Of(string value)
		{
			ArgumentNullException.ThrowIfNullOrWhiteSpace(value);
			ArgumentOutOfRangeException.ThrowIfNotEqual(value.Length,DefaultLength);

			return new OrderName(value);
		}
	}
}
