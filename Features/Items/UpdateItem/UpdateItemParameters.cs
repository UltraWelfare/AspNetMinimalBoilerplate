namespace AspNetMinimalBoilerplate.Features.Items.UpdateItem;

public class UpdateItemParameters
{
    public string Name { get; set; } = null!;
    
    public int Quantity { get; set; }
    
    public decimal Price { get; set; }
}