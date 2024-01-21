using AspNetMinimalBoilerplate.Entities;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace AspNetMinimalBoilerplate.Features.Items.CreateItem;

public class CreateItemParametersValidator : AbstractValidator<CreateItemParameters>
{
    public CreateItemParametersValidator(OrderContext db)
    {
        RuleFor(x => x.Name)
            .MustAsync(async (name, cancellationToken) =>
                !await db.Items.AnyAsync(item => item.Name == name, cancellationToken))
            .WithMessage("Item with this name already exists")
            .NotEmpty();
        RuleFor(x => x.Quantity).GreaterThan(0);
        RuleFor(x => x.Price).GreaterThan(0);
    }
}