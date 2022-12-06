using FluentValidation;
using NLayer.Core.DTO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Service.Validations
{
    public class ProductDtoValidator:AbstractValidator<ProductDto>
    {
        public ProductDtoValidator()
        {
            RuleFor(x => x.Name).NotNull().WithMessage("{PropertyName} is required").NotEmpty().WithMessage("{PropertyName} is required");
            
            //Stock ve Price'ın default'u 0'dır. Value typeların default değeri olduğu için not null ile kontroll edilemezler. String reference type olduğu için null olabilir. Price, double, datetime value type olduğu için not null not empty kullanamayız. 
            
            RuleFor(x=>x.Price). InclusiveBetween(1, int.MaxValue).WithMessage("{PropertyName} must be greater 0"); 
            RuleFor(x=>x.Stock).InclusiveBetween(1, int.MaxValue).WithMessage("{PropertyName} must be greater 0");
           
            //Category id int olduğundan defaultu 0.
            RuleFor(x=>x.CategoryId).InclusiveBetween(1, int.MaxValue).WithMessage("{PropertyName} must be greater 0");
        }


    }
}
