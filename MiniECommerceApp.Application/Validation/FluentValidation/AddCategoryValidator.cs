using FluentValidation;
using MiniECommerceApp.Entity.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerceApp.Application.Validation.FluentValidation
{
    public class AddCategoryValidator:AbstractValidator<AddCategoryDto>
    {
        public AddCategoryValidator()
        {
            RuleFor(x => x.CategoryName).NotEmpty().NotNull().WithMessage($"{nameof(AddCategoryDto.CategoryName)} alani boş bırakilamaz !.");
        }
    }
}
