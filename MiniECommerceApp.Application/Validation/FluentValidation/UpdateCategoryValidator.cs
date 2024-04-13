using FluentValidation;
using MiniECommerceApp.Entity.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerceApp.Application.Validation.FluentValidation
{
    public class UpdateCategoryValidator:AbstractValidator<UpdateCategoryDto>
    {
        public UpdateCategoryValidator()
        {
            RuleFor(x => x.Id).NotEmpty().NotNull().WithMessage($"{nameof(UpdateCategoryDto.Id)} alani boş bırakilamaz !.");
            RuleFor(x => x.CategoryName).NotEmpty().NotNull().WithMessage($"{nameof(UpdateCategoryDto.CategoryName)} alani boş bırakilamaz !.");
        }
    }
}
