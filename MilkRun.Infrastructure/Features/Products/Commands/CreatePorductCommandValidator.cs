using FluentValidation;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkRun.Infrastructure.Features.Products.Commands
{   
    public class CreatePorductCommandValidator : AbstractValidator<CreateProductCommand>
    {      
        public CreatePorductCommandValidator()
        {

            RuleFor(p => p.Price)
                .LessThan(0).WithMessage("{PropertyName} can't be less than 0.");
            RuleFor(p => p.Title)
               .NotEmpty().WithMessage("{PropertyName} is required.");
            RuleFor(p => p.Description)
                .MaximumLength(100).WithMessage("{PropertyName} can't be more than  100.");
        }       
    }
}
