using FluentValidation;
using MiniECommerceApp.Entity.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerceApp.Application.Validation.FluentValidation
{
    public class UpdateProductValidator : AbstractValidator<UpdateProductDto>
    {
        public UpdateProductValidator()
        {
            RuleFor(x => x.Id).NotEmpty().NotNull().WithMessage($"{nameof(UpdateProductDto.Id)} alani boş bırakilamaz !.");
            RuleFor(x => x.Amount).NotEmpty().NotNull().WithMessage($"{nameof(UpdateProductDto.Amount)} alani boş bırakilamaz !.");
            RuleFor(x => x.ProductPrice).NotEmpty().NotNull().WithMessage($"{nameof(UpdateProductDto.ProductPrice)} alani boş bırakilamaz !.");
            RuleFor(x => x.ProductName).NotEmpty().NotNull().WithMessage($"{nameof(UpdateProductDto.ProductName)} alani boş bırakilamaz !.");
            RuleFor(x => x.Size).NotEmpty().NotNull().WithMessage($"{nameof(UpdateProductDto.Size)} alani boş bırakilamaz !.");
            RuleFor(x => x.CategoriesId).NotEmpty().NotNull().WithMessage($"{nameof(UpdateProductDto.CategoriesId)} alani boş bırakilamaz !.");
        }

    }
}
