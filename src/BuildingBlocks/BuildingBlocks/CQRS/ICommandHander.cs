using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.CQRS
{
	public interface ICommandHandler<in TCommand> : ICommandHandler<TCommand, Unit>
		where TCommand : ICommand<Unit>
	{

	}

	public interface ICommandHandler<in TCommand, TResposne> : IRequestHandler<TCommand, TResposne>
		where TCommand : ICommand<TResposne>
		where TResposne : notnull
	{

	}

}
