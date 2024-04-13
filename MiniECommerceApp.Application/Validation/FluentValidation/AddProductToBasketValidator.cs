using FluentValidation;
using MiniECommerceApp.Entity.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerceApp.Application.Validation.FluentValidation
{
    public class AddProductToBasketValidator : AbstractValidator<AddProductToBasketDto>
    {
        public AddProductToBasketValidator()
        {
            RuleFor(x => x.UserId).NotEmpty().NotNull().WithMessage($"{nameof(AddProductToBasketDto.UserId)} alani boş bırakilamaz !.");
            RuleFor(x => x.ProdId).NotEmpty().NotNull().WithMessage($"{nameof(AddProductToBasketDto.ProdId)} alani boş bırakilamaz !.");
        }
    }
}
