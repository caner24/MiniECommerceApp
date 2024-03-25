using AutoMapper;
using MediatR;
using MiniECommerceApp.Application.MiniECommerce.Commands.Request;
using MiniECommerceApp.Application.MiniECommerce.Commands.Response;
using MiniECommerceApp.Data.Abstract;
using MiniECommerceApp.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerceApp.Application.MiniECommerce.Handlers.CommandHandler
{
    public class AddCategoryCommandHandler : IRequestHandler<AddCategoryCommandRequest, AddCategoryCommandResponse>
    {
        private readonly IMapper _mapper;
        private readonly ICategoryDal _categoryDal;
        public AddCategoryCommandHandler(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
        }

        public async Task<AddCategoryCommandResponse> Handle(AddCategoryCommandRequest request, CancellationToken cancellationToken)
        {
            var category = _mapper.Map<Category>(request);
            var addedCategory = await _categoryDal.AddAsync(category);
            if (addedCategory is not null)
            {
                return new AddCategoryCommandResponse { IsAdded = true, Category = addedCategory };
            }
            return new AddCategoryCommandResponse { IsAdded = false};
        }
    }
}
