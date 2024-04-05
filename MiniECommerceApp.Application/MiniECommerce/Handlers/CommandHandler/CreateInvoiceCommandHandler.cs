using MediatR;
using Microsoft.EntityFrameworkCore;
using MiniECommerceApp.Application.MiniECommerce.Commands.Request;
using MiniECommerceApp.Application.MiniECommerce.Commands.Response;
using MiniECommerceApp.Data.Abstract;
using MiniECommerceApp.Data.Concrete;
using MiniECommerceApp.Entity;
using MiniECommerceApp.Entity.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerceApp.Application.MiniECommerce.Handlers.CommandHandler
{
    public class CreateInvoiceCommandHandler : IRequestHandler<CreateInvoiceCommandRequest, CreateInvoiceCommandResponse>
    {
        private readonly IInvoicesDal _invoceDal;
        private readonly IProductDal _productDal;
        private readonly MiniECommerceContext _context;
        public CreateInvoiceCommandHandler(IInvoicesDal invoicesDal, IProductDal productDal, MiniECommerceContext context)
        {
            _context = context;
            _invoceDal = invoicesDal;
            _productDal = productDal;
        }
        public async Task<CreateInvoiceCommandResponse> Handle(CreateInvoiceCommandRequest request, CancellationToken cancellationToken)
        {
            var lineTotal = 0;
            var user = _context.Set<User>().Where(x => x.UserName == request.UserId).AsNoTracking().FirstOrDefaultAsync();
            if (user is null)
                throw new UserNotFoundExcepiton();
            var invoices = new Invoice();
            var transactionScope = await _context.Database.BeginTransactionAsync();
            for (int i = 0; i < request.ProductId.Count(); i++)
            {
                var product = await _productDal.Get(x => x.Id == request.ProductId[i]).FirstOrDefaultAsync();
                if (product is not null)
                {
                    invoices.Product.Add(product);
                    var newAmount = product.ProductDetail.Amount - request.Amount[i];
                    if (newAmount < 0)
                        throw new NotEnoughtAmountException();
                    product.ProductDetail.Amount = newAmount;
                    lineTotal = lineTotal + ((int)product.ProductPrice * request.Amount[i]);
                    await _productDal.UpdateAsync(product);
                }
                invoices.UserId = request.UserId;
            }
            await _invoceDal.AddAsync(invoices);
            await transactionScope.CommitAsync();
            return new CreateInvoiceCommandResponse { IsOk = true, LineTotal = lineTotal };
        }
    }
}
