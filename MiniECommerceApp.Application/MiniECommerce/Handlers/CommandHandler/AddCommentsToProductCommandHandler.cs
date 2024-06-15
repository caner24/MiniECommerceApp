using MediatR;
using Microsoft.EntityFrameworkCore;
using MiniECommerceApp.Application.MiniECommerce.Commands.Request;
using MiniECommerceApp.Application.MiniECommerce.Commands.Response;
using MiniECommerceApp.Data.Abstract;
using MiniECommerceApp.Data.Concrete;
using MiniECommerceApp.Entity;
using MiniECommerceApp.Entity.Exceptions;
using MiniECommerceApp_Application;
using Stripe;
using Stripe.Climate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerceApp.Application.MiniECommerce.Handlers.CommandHandler
{
    public class AddCommentsToProductCommandHandler : IRequestHandler<AddComentsToProductCommandRequest, AddCommentsToProductResponse>
    {
        private readonly IProductDal _productDal;
        private readonly IInvoicesDal _invoiceDal;
        private readonly MiniECommerceContext _context;
        private readonly ClaimsPrincipal _claimsPrincipal;
        public AddCommentsToProductCommandHandler(IProductDal productDal, IInvoicesDal invoiceDal, ClaimsPrincipal claimsPrincipal, MiniECommerceContext context)
        {
            _claimsPrincipal = claimsPrincipal;
            _invoiceDal = invoiceDal;
            _productDal = productDal;
            _context = context;
        }
        public async Task<AddCommentsToProductResponse> Handle(AddComentsToProductCommandRequest request, CancellationToken cancellationToken)
        {
            var user = _claimsPrincipal.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var stripeUser = await _context.Users.FirstOrDefaultAsync(x => x.Id == user);
            var product = await _productDal.Get(x => x.Id == request.ProductId).Include(x => x.Comments).Include(x => x.ProductDetail).FirstOrDefaultAsync();
            if (product == null)
                throw new ProductNotFoundException();

            if (user == null)
                throw new UserNotFoundExcepiton();

            var options = new ChargeListOptions
            {
                Limit = 100 // İhtiyacınıza göre limit ayarlayın
            };

            var service = new ChargeService();
            var charges = await service.ListAsync(options);
            var filteredPayments = charges.Data.Where(charge =>charge.BillingDetails.Email==stripeUser.Email).ToList();

            if (filteredPayments.Count==0)
                throw new Exception("Ürüne yorum yapmak için satın almanız gerekmektedir");

            var sampleData = new OffensiveMLModel.ModelInput()
            {
                Text = request.CommentText,
            };
            var result = OffensiveMLModel.Predict(sampleData);
            bool isValid = result.PredictedLabel == "0" ? true : false;


            product.Comments.Add(new Entity.Comment { UserId = user, CommentText = request.CommentText, IsValidComment = isValid });
            await _productDal.UpdateAsync(product);
            return new AddCommentsToProductResponse { IsCommentConfirmed = isValid };
        }
    }
}
