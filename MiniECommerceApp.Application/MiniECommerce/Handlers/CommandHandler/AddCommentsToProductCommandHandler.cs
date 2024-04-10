using MediatR;
using Microsoft.EntityFrameworkCore;
using MiniECommerceApp.Application.MiniECommerce.Commands.Request;
using MiniECommerceApp.Application.MiniECommerce.Commands.Response;
using MiniECommerceApp.Data.Abstract;
using MiniECommerceApp.Data.Concrete;
using MiniECommerceApp.Entity;
using MiniECommerceApp.Entity.Exceptions;
using MiniECommerceApp_Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerceApp.Application.MiniECommerce.Handlers.CommandHandler
{
    public class AddCommentsToProductCommandHandler : IRequestHandler<AddComentsToProductCommandRequest, AddCommentsToProductResponse>
    {
        private readonly IProductDal _productDal;
        private readonly MiniECommerceContext _miniECommerceContext;
        public AddCommentsToProductCommandHandler(IProductDal productDal, MiniECommerceContext context)
        {
            _productDal = productDal;
            _miniECommerceContext = context;
        }
        public async Task<AddCommentsToProductResponse> Handle(AddComentsToProductCommandRequest request, CancellationToken cancellationToken)
        {
            var product = await _productDal.Get(x => x.Id == request.ProductId).Include(x => x.Comments).Include(x => x.ProductDetail).FirstOrDefaultAsync();
            if (product == null)
                throw new ProductNotFoundException();

            var user = await _miniECommerceContext.Set<User>().Where(x => x.UserName == request.UserId).FirstOrDefaultAsync();
            if (user == null)
                throw new UserNotFoundExcepiton();

            var sampleData = new OffensiveMLModel.ModelInput()
            {
                Text = request.CommentText,
            };
            var result = OffensiveMLModel.Predict(sampleData);
            bool isValid = result.PredictedLabel == "0" ? true : false;


            product.Comments.Add(new Entity.Comment { UserId = user.Id, CommentText = request.CommentText, IsValidComment = isValid });
            await _productDal.UpdateAsync(product);
            return new AddCommentsToProductResponse { IsCommentConfirmed = isValid };
        }
    }
}
