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
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

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

            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                var user = await _context.Set<User>().Where(x => x.UserName == request.UserId).FirstOrDefaultAsync();
                if (user is null)
                    throw new UserNotFoundExcepiton();

                var invoices = new Invoice { UserId = user.Id, InvoiceNo = Guid.NewGuid() };

                for (int i = 0; i < request.ProductId.Count(); i++)
                {
                    var product = await _productDal.Get(x => x.Id == request.ProductId[i]).Include(x => x.ProductDetail).FirstOrDefaultAsync();
                    if (product is not null)
                    {
                        invoices.Product.Add(product);
                        if (product.Amount < request.Amount[i])
                            throw new NotEnoughtAmountException();


                        var newAmount = product.Amount - request.Amount[i];

                        product.Amount = newAmount;
                        lineTotal += (int)(product.ProductPrice * request.Amount[i]);
                        await _productDal.UpdateAsync(product);
                    }
                }
                await _invoceDal.AddAsync(invoices);

                await transaction.CommitAsync();
                return new CreateInvoiceCommandResponse { IsOk = true, LineTotal = lineTotal };
            }
        }
    }
}
