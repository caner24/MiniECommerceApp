using MediatR;
using Microsoft.EntityFrameworkCore;
using MiniECommerceApp.Application.MiniECommerce.Queries.Request;
using MiniECommerceApp.Application.MiniECommerce.Queries.Response;
using MiniECommerceApp.Data.Abstract;
using MiniECommerceApp.Entity;
using MiniECommerceApp.Entity.Helpers;
using MiniECommerceApp.Entity.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerceApp.Application.MiniECommerce.Handlers.QueryHandler
{
    public class GetAllProductQueryHandler : IRequestHandler<GetAllProductQueryRequest, PagedList<MiniECommerceApp.Entity.Models.Entity>>
    {
        private readonly IProductDal _productDal;
        private readonly ISortHelper<Product> _sortHelper;
        private readonly IDataShaper<Product> _dataShaper;
        public GetAllProductQueryHandler(IProductDal productDal, ISortHelper<Product> sortHelper, IDataShaper<Product> dataShaper)
        {
            _productDal = productDal;
            _sortHelper = sortHelper;
            _dataShaper = dataShaper;
        }
        public async Task<PagedList<MiniECommerceApp.Entity.Models.Entity>> Handle(GetAllProductQueryRequest request, CancellationToken cancellationToken)
        {

            var products = _productDal.GetAll();
            SearchByName(ref products, request.Name);
            SearchByCategories(ref products, request.Categories);
            var sortedOwners = _sortHelper.ApplySort(products, request.OrderBy);
            var shapedOwners = _dataShaper.ShapeData(sortedOwners, request.Fields);

            return PagedList<MiniECommerceApp.Entity.Models.Entity>.ToPagedList(shapedOwners,
            request.PageNumber,
            request.PageSize);

        }
        private void SearchByName(ref IQueryable<Product> owners, string productName)
        {
            if (!owners.Any() || string.IsNullOrWhiteSpace(productName))
                return;

            if (string.IsNullOrEmpty(productName))
                return;

            owners = owners.Where(o => o.ProductName == productName);
        }
        private void SearchByCategories(ref IQueryable<Product> owners, string categoryName)
        {
            if (!owners.Any() || string.IsNullOrWhiteSpace(categoryName))
                return;

            if (string.IsNullOrEmpty(categoryName))
                return;

            owners = owners.Where(o => o.Categories.Any(x => x.CategoryName == categoryName));

        }
    }
}
