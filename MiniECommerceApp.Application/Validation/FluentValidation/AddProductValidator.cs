using FluentValidation;
using MiniECommerceApp.Entity.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerceApp.Application.Validation.FluentValidation
{
    public class AddProductValidator : AbstractValidator<AddProductDto>
    {
        public AddProductValidator()
        {
            RuleFor(x => x.Amount).NotEmpty().NotNull().WithMessage($"{nameof(AddProductDto.Amount)} alani boş bırakilamaz !.");
            RuleFor(x => x.ProductPrice).NotEmpty().NotNull().WithMessage($"{nameof(AddProductDto.ProductPrice)} alani boş bırakilamaz !.");
            RuleFor(x => x.ProductName).NotEmpty().NotNull().WithMessage($"{nameof(AddProductDto.ProductName)} alani boş bırakilamaz !.");
            RuleFor(x => x.Size).NotEmpty().NotNull().WithMessage($"{nameof(AddProductDto.Size)} alani boş bırakilamaz !.");
            RuleFor(x => x.CategoriesId).NotEmpty().NotNull().WithMessage($"{nameof(AddProductDto.CategoriesId)} alani boş bırakilamaz !.");
        }
    }
}
