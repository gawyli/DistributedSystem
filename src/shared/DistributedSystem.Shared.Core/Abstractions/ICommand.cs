using MediatR;

namespace DistributedSystem.Shared.Core.Abstractions;
public interface ICommand : IRequest, IAction
{
}

public interface ICommand<TResponse> : IRequest<TResponse>, IAction
{
}
