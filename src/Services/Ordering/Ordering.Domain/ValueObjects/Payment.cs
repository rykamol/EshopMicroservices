using System.Diagnostics.Metrics;
using System.Reflection.Emit;

namespace Ordering.Domain.ValueObjects
{
	public record Payment
	{
		public string CardName { get; } = default;
		public string CardNumber { get; } = default;
		public string Expiration { get; } = default;
		public string CVV { get; } = default;
		public string PaymentMethod { get; } = default;

		protected Payment()
		{
		}
		private Payment(string cardName, string cardNumber, string expiration,
			string cvv, string paymentMethod)
		{
			CardName = cardName;
			CardNumber = cardNumber;
			Expiration = expiration;
			CVV = cvv;
			PaymentMethod = paymentMethod;
		}
		public static Payment Of(string cardName, string cardNumber, string expiration,
			string cvv, string paymentMethod)
		{
			ArgumentNullException.ThrowIfNullOrWhiteSpace(cardNumber);
			ArgumentNullException.ThrowIfNullOrWhiteSpace(cardNumber);
			ArgumentNullException.ThrowIfNullOrWhiteSpace(expiration);
			ArgumentNullException.ThrowIfNullOrWhiteSpace(cvv);
			ArgumentOutOfRangeException.ThrowIfGreaterThan(cvv.Length,3);
			ArgumentNullException.ThrowIfNullOrWhiteSpace(paymentMethod);

			return new Payment(cardNumber, cardNumber, expiration, cvv, paymentMethod);
		}
	}
}
