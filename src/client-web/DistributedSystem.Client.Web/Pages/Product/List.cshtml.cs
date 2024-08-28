using DistributedSystem.Client.Core.ProductAggregate.Queries;
using DistributedSystem.Shared.Web.Pages;
using MediatR;

namespace DistributedSystem.Client.Web.Pages.Product;
public class ListModel : BasePageModel
{
    public List<DistributedSystem.Shared.Common.Aggregates.ProductAggragate.Product> Products { get; set; } = new();
    public ListModel(IConfiguration configuration, IMediator mediator) : base(configuration, mediator)
    {

    }

    public async Task OnGetAsync(CancellationToken cancellationToken)
    {
        var products = await _mediator.Send(new ListProducts.Query(), cancellationToken);

        this.Products = products;
    }
}
