using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DistributedSystem.Shared.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class BaseController : ControllerBase
{
    private readonly IMediator _mediator;

    public BaseController(IMediator mediator)
    {
        _mediator = mediator;
    }
}
