using TravelWebApp.Models;

namespace TravelWebApp.Services;

public interface ICartService
{
    List<CartItem> Items { get; }
    event Action OnChange;
    void AddItem(CartItem item);
    void RemoveItem(string id);
    void ClearCart();
    decimal GetTotal();
}

public class CartService : ICartService
{
    public List<CartItem> Items { get; private set; } = [];
    public event Action? OnChange;

    public void AddItem(CartItem item)
    {
        Items.Add(item);
        NotifyStateChanged();
    }

    public void RemoveItem(string id)
    {
        var item = Items.FirstOrDefault(i => i.Id == id);
        if (item != null)
        {
            Items.Remove(item);
            NotifyStateChanged();
        }
    }

    public void ClearCart()
    {
        Items.Clear();
        NotifyStateChanged();
    }

    public decimal GetTotal() => Items.Sum(i => i.Price * i.Quantity);

    private void NotifyStateChanged() => OnChange?.Invoke();
}

public class CartItem
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public int ProductId { get; set; }
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
    public string Image { get; set; } = "";
    public decimal Price { get; set; }
    public int Quantity { get; set; } = 1;
    public string Type { get; set; } = ""; // Accommodation, Activity, Permit, Package
    public DateTime? Date { get; set; }
    public DateTime? EndDate { get; set; }
}
