using DistributedSystem.Shared.Api.Controllers;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

using Swashbuckle.AspNetCore.Annotations;
using DistributedSystem.FinanceApproval.Api.FinanceApplicationAggregate.Commands;
using MediatR;

namespace DistributedSystem.FinanceApproval.Api.Controllers.FinanceApplication;

[ApiVersion("1.0")]
public class CreateFinanceApplicationController : BaseController
{
    private readonly IMediator _mediator;

    public CreateFinanceApplicationController(IMediator mediator) : base()
    {
        _mediator = mediator;
    }

    [HttpPost]
    [SwaggerOperation(
        Summary = "Create a Financial Application",
        Description = "Creates a Financial Application resource",
        OperationId = "FinancialApplications.Create",
        Tags = new[] { "FinancialApplicationControllers" })]
    public async Task<IActionResult> Create([FromBody] CreateFinanceApplication.Command command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return this.Ok(result);
    }
}
