using AutoMapper;
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
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommandRequest, UpdateCategoryCommandResponse>
    {
        private readonly IMapper _mapper;
        private readonly ICategoryDal _categoryDal;
        public UpdateCategoryCommandHandler(IMapper mapper, ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
            _mapper = mapper;
        }
        public async Task<UpdateCategoryCommandResponse> Handle(UpdateCategoryCommandRequest request, CancellationToken cancellationToken)
        {
            var category = await _categoryDal.Get(x => x.Id == request.Id).FirstOrDefaultAsync();
            if (category is not null)
            {
                var updatedCategory = await _categoryDal.UpdateAsync(category);
                return new UpdateCategoryCommandResponse { IsUpdated = true, Category = updatedCategory };
            }
            throw new CategoryNotFoundException();
        }
    }
}
