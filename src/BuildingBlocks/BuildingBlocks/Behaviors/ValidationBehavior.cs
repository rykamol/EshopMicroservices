using BuildingBlocks.CQRS;
using FluentValidation;
using MediatR;

namespace BuildingBlocks.Behaviors
{
	public class ValidationBehavior<TRequest, TReponse>(IEnumerable<IValidator<TRequest>> validators)
		: IPipelineBehavior<TRequest, TReponse> where TRequest : ICommand<TReponse>
	{
		public async Task<TReponse> Handle(TRequest request, RequestHandlerDelegate<TReponse> next, CancellationToken cancellationToken)
		{
			var context = new ValidationContext<TRequest>(request);

			var validationResults = await Task.WhenAll(
				validators.Select(v => v.ValidateAsync(context, cancellationToken)));

			var failures = validationResults
				.Where(r => r.Errors.Any())
				.SelectMany(r => r.Errors)
				.ToList();

			if (failures.Any()) { throw new ValidationException(failures); }

			return await next();
		}
	}
}
