using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributedSystem.Shared.Core.Abstractions;
public interface ICommand : IRequest, IAction
{
}

public interface ICommand<TResponse> : IRequest<TResponse>, IAction
{
}
