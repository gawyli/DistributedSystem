using MediatR;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;

namespace DistributedSystem.Shared.Web.Pages;
public abstract class BasePageModel : PageModel
{
    protected readonly IConfiguration _configuration;
    protected readonly IMediator _mediator;

    protected BasePageModel(IConfiguration configuration, IMediator mediator)
    {
        _configuration = configuration;
        _mediator = mediator;
    }

}
