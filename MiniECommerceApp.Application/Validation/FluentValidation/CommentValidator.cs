using FluentValidation;
using MiniECommerceApp.Entity;
using MiniECommerceApp.Entity.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerceApp.Application.Validation.FluentValidation
{
    public class CommentValidator : AbstractValidator<AddCommentsToProductDto>
    {
        public CommentValidator()
        {
            RuleFor(x => x.CommentText).NotEmpty().NotNull().WithMessage("Mesaj içeriği boş bırakılamaz !.");
            RuleFor(x => x.ProductId).NotEmpty().NotNull().WithMessage("Ürün ıd boş bırakılamaz !.");
        }
    }
}
