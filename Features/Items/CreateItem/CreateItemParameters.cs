namespace AspNetMinimalBoilerplate.Features.Items.CreateItem;

public class CreateItemParameters
{
    public string Name { get; set; } = null!;
    
    public int Quantity { get; set; }
    
    public decimal Price { get; set; }
}