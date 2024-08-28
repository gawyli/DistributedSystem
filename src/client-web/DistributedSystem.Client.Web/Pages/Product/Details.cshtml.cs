
using DistributedSystem.Shared.Web.Pages;
using MediatR;

namespace DistributedSystem.Client.Web.Pages.Product
{
    public class DetailsModel : BasePageModel
    {
        public DistributedSystem.Shared.Common.Aggregates.ProductAggragate.Product? Product { get; set; }
        public string Message { get; set; } = string.Empty;
        public DetailsModel(IConfiguration configuration, IMediator mediator) : base(configuration, mediator)
        {
        }



        //public async Task OnGetAsync(GetProductDetails.Query query, CancellationToken cancellationToken)
        //{
        //    var product = await _mediator.Send(query, cancellationToken);
        //    if (product == null)
        //    {
        //        this.Message = "Product not found";
        //        return;
        //    }

        //    this.Product = product!;
        //}
    }
}
