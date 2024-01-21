using AspNetMinimalBoilerplate.Entities;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace AspNetMinimalBoilerplate.Features.Items.UpdateItem;

public class UpdateItemParametersValidator : AbstractValidator<UpdateItemParameters>
{
    public UpdateItemParametersValidator(OrderContext db, int exceptId)
    {
        RuleFor(x => x.Name)
            .MustAsync(async (name, cancellationToken) =>
                !await db.Items.AnyAsync(item => item.Name == name && item.Id != exceptId, cancellationToken))
            .WithMessage("Item with this name already exists")
            .NotEmpty();
        RuleFor(x => x.Quantity).GreaterThan(0);
        RuleFor(x => x.Price).GreaterThan(0);
    }
}