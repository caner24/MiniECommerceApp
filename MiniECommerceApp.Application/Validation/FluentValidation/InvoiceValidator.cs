using FluentValidation;
using MiniECommerceApp.Entity.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerceApp.Application.Validation.FluentValidation
{
    public class InvoiceValidator : AbstractValidator<CreateInvoiceDto>
    {
        public InvoiceValidator()
        {
            RuleFor(x => x.UserId).NotEmpty().NotNull().WithMessage($"{nameof(CreateInvoiceDto.UserId)} alani boş bırakilamaz !.");
            RuleFor(x => x.ProductId).NotEmpty().NotNull().WithMessage($"{nameof(CreateInvoiceDto.ProductId)} alani boş bırakilamaz !.");
            RuleFor(x => x.Amount).NotEmpty().NotNull().WithMessage($"{nameof(CreateInvoiceDto.Amount)} alani boş bırakilamaz !.");
        }
    }
}
