namespace AspNetMinimalBoilerplate.Features.Items.UpdateItem;

public class UpdateItemResponse
{
    public int Id { get; set; }
    
    public string Name { get; set; } = null!;
    
    public int Quantity { get; set; }
    
    public decimal Price { get; set; }
}