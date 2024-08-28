using MediatR;

namespace DistributedSystem.Shared.Core.Abstractions;
public interface IQuery : IRequest, IAction
{
}

public interface IQuery<TResponse> : IRequest<TResponse>, IAction
{
}
