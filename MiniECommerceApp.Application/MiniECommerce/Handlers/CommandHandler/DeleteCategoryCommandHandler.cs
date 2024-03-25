using MediatR;
using Microsoft.EntityFrameworkCore;
using MiniECommerceApp.Application.MiniECommerce.Commands.Request;
using MiniECommerceApp.Application.MiniECommerce.Commands.Response;
using MiniECommerceApp.Data.Abstract;
using MiniECommerceApp.Entity.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerceApp.Application.MiniECommerce.Handlers.CommandHandler
{
    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommandRequest, DeleteCategoryCommandResponse>
    {
        private readonly ICategoryDal _categoryDal;
        public DeleteCategoryCommandHandler(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
        }
        public async Task<DeleteCategoryCommandResponse> Handle(DeleteCategoryCommandRequest request, CancellationToken cancellationToken)
        {
            var category = await _categoryDal.Get(x => x.Id == request.Id).FirstOrDefaultAsync();
            if (category is not null)
            {
                await _categoryDal.DeleteAsync(category);
                return new DeleteCategoryCommandResponse { IsDeleted = true };
            }
            throw new CategoryNotFoundException();
        }
    }
}
