using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
namespace Ordering.Domain.Abstractions
{
	public interface IDomainEvent : INotification //Inotification will ditchpatch with mediatr handler
	{
		Guid EventId => Guid.NewGuid();
		public DateTime OccurredOn => DateTime.UtcNow;
		public string EventType => GetType().AssemblyQualifiedName;
	}
}
