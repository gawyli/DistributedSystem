using DistributedSystem.Client.Core.ProductAggregate.Commands;
using DistributedSystem.Shared.Web.Pages;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DistributedSystem.Client.Web.Pages.Product;

public class CreateModel : BasePageModel
{
    public CreateProduct.Command Command { get; set; } = null!;

    public CreateModel(IConfiguration configuration, IMediator mediator) : base(configuration, mediator)
    {
        
    }


    public void OnGet()
    {
        this.Command = new CreateProduct.Command();
    }

    public async Task<IActionResult> OnPostAsync(CreateProduct.Command command, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var productId = await _mediator.Send(command, cancellationToken);

        return RedirectToPage("Details", new { id = productId });
    }
}

