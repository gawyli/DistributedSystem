using DistributedSystem.Shared.Common.Aggregates.ProductAggregate.Commands;
using DistributedSystem.Shared.Common.Aggregates.ProductAggregate.Queries;
using DistributedSystem.Shared.Web.Pages;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DistributedSystem.Client.Web.Pages.Product;

public class UpdateModel : BasePageModel
{
    public UpdateProduct.Command Command { get; set; } = null!;
    public DistributedSystem.Shared.Common.Aggregates.ProductAggragate.Product Product { get; set; } = null!;
    public UpdateModel(IConfiguration configuration, IMediator mediator) : base(configuration, mediator)
    {
    }

    //public async Task OnGetAsync(GetProductById.Query query, CancellationToken cancellationToken)
    //{
    //    var product = await _mediator.Send(query, cancellationToken);

    //    this.Product = product;

    //}

    //public async Task<IActionResult> OnPostAsync(UpdateProduct.Command command, CancellationToken cancellationToken)
    //{
    //    var product = await _mediator.Send(command, cancellationToken);

    //    return LocalRedirect($"/Product/Details/{product.Id}");
    //}
}

