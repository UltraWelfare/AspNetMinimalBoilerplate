using AspNetMinimalBoilerplate.Entities;
using AspNetMinimalBoilerplate.Features.Items.CreateItem;
using AspNetMinimalBoilerplate.Features.Items.GetItems;
using AspNetMinimalBoilerplate.Features.Items.UpdateItem;
using AspNetMinimalBoilerplate.Utilities;
using Microsoft.EntityFrameworkCore;
using OneOf.Types;

namespace AspNetMinimalBoilerplate.Features.Items;

public class ItemService(OrderContext db)
{
    public async Task<List<GetItemsResponse>> GetItems()
    {
        return await db.Items
            .Select(item => new GetItemsResponse
            {
                Id = item.Id,
                Name = item.Name,
                Quantity = item.Quantity,
                Price = item.Price
            })
            .ToListAsync();
    }
    
    public async Task<ValueOrError<CreateItemResponse>> CreateItem(CreateItemParameters parameters)
    {
        var validator = new CreateItemParametersValidator(db);
        var validationResult = await validator.ValidateAsync(parameters);
        if (!validationResult.IsValid)
        {
            return validationResult.Errors;
        }
        
        var item = new Item
        {
            Name = parameters.Name,
            Quantity = parameters.Quantity,
            Price = parameters.Price
        };
        
        db.Items.Add(item);
        await db.SaveChangesAsync();
        
        return new CreateItemResponse
        {
            Id = item.Id,
            Name = item.Name,
            Quantity = item.Quantity,
            Price = item.Price
        };
    }

    public async Task<ValueOrError<UpdateItemResponse>> UpdateItem(int id, UpdateItemParameters parameters)
    {
        var item = await db.Items.SingleOrDefaultAsync(item => item.Id == id);
        if (item is null) return new NotFound();
        
        var validator = new UpdateItemParametersValidator(db, id);
        var validationResult = await validator.ValidateAsync(parameters);
        if (!validationResult.IsValid)
        {
            return validationResult.Errors;
        }
        
        item.Name = parameters.Name;
        item.Quantity = parameters.Quantity;
        item.Price = parameters.Price;
        await db.SaveChangesAsync();

        return new UpdateItemResponse()
        {
            Id = item.Id,
            Name = item.Name,
            Quantity = item.Quantity,
            Price = item.Price
        };
    }
    
    public async Task<ValueOrError<None>> DeleteItem(int id)
    {
        var item = await db.Items.SingleOrDefaultAsync(item => item.Id == id);
        if (item is null) return new NotFound();
        
        db.Items.Remove(item);
        await db.SaveChangesAsync();
        return new None();
    }
    
}