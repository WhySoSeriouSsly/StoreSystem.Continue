using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using StoreSystem.Entities.Concrete;

namespace StoreSystem.Business.ValidationRules.FluentValidation
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        { 
            RuleFor(x => x.ProductName).NotNull().WithMessage("Product Name Bos Gecilemez");
            RuleFor(x => x.ProductName).Length(4, 20).WithMessage("4 ve 20 karakter uzunluğunda olmalı.");
            RuleFor(p => p.UnitPrice).GreaterThanOrEqualTo(1).WithMessage("Unit Price 1 den büyük olmalı");
        }
    }
    
}
