using FluentValidation;
using MiniECommerceApp.Entity.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerceApp.Application.Validation.FluentValidation
{
    public class DeleteProductValidator:AbstractValidator<DeleteProductDto>
    {
        public DeleteProductValidator()
        {
            RuleFor(x => x.Id).NotEmpty().NotNull().WithMessage($"{nameof(DeleteProductDto.Id)} alani boş bırakilamaz !.");
        }
    }
}
