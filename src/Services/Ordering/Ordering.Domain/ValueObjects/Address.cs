namespace Ordering.Domain.ValueObjects
{
	public record Address
	{
		public string FirstName { get; } = default;
		public string LastName { get; } = default;
		public string? Email { get; } = default;
		public string AddressLine { get; } = default;
		public string Country { get; } = default;
		public string State { get; } = default;
		public string ZipCode { get; } = default;

		protected Address( )
		{
		}
		private Address(string firstName,string lastName,string email,
			string addressLine,string country,string state,string zipcode)
		{
			FirstName = firstName;
			LastName = lastName;
			Email = email;
			AddressLine = addressLine;			
			Country = country;				
			State = state;
			ZipCode = zipcode;			
		}
		public static Address Of(string firstName, string lastName, string email,
			string addressLine, string country, string state, string zipcode)
		{
			ArgumentNullException.ThrowIfNullOrWhiteSpace(firstName);
			ArgumentNullException.ThrowIfNullOrWhiteSpace(email);
			ArgumentNullException.ThrowIfNullOrWhiteSpace(addressLine);

			return new Address(firstName,lastName,email,addressLine,country,state,zipcode);
		}
	}
}

