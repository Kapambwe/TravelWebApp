namespace TravelWebApp.Models;

public class Destination
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string Country { get; set; } = "";
    public string Image { get; set; } = "";
    public string Description { get; set; } = "";
    public decimal StartingPrice { get; set; }
    public string Currency { get; set; } = "R";
    public string[] Highlights { get; set; } = [];
    public string BestSeason { get; set; } = "";
    public string[] Wildlife { get; set; } = [];
}

public class Property
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string Type { get; set; } = "";
    public int DestinationId { get; set; }
    public string DestinationName { get; set; } = "";
    public string Location { get; set; } = "";
    public string Image { get; set; } = "";
    public string[] Gallery { get; set; } = [];
    public decimal PricePerNight { get; set; }
    public decimal? OriginalPrice { get; set; }
    public string Currency { get; set; } = "R";
    public double Rating { get; set; }
    public int ReviewCount { get; set; }
    public int Stars { get; set; }
    public string[] Amenities { get; set; } = [];
    public bool FreeCancellation { get; set; }
    public bool BreakfastIncluded { get; set; }
    public string Description { get; set; } = "";
    public double Latitude { get; set; }
    public double Longitude { get; set; }
}

public class TravelDeal
{
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public string Description { get; set; } = "";
    public int DiscountPercent { get; set; }
    public DateTime ValidUntil { get; set; }
    public string Badge { get; set; } = "";
    public string Image { get; set; } = "";
    public string Link { get; set; } = "";
}

public class SearchFilters
{
    public string Query { get; set; } = "";
    public DateTime? CheckIn { get; set; }
    public DateTime? CheckOut { get; set; }
    public int Adults { get; set; } = 2;
    public int Children { get; set; }
    public int Rooms { get; set; } = 1;
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
    public string PropertyType { get; set; } = "";
    public int? MinStars { get; set; }
    public double? MinRating { get; set; }
    public bool? FreeCancellation { get; set; }
    public bool? BreakfastIncluded { get; set; }
    public string SortBy { get; set; } = "price_asc";
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}

public class SearchResult
{
    public int TotalCount { get; set; }
    public List<Property> Results { get; set; } = [];
    public int Page { get; set; }
    public int TotalPages { get; set; }
}

public class Booking
{
    public string Reference { get; set; } = "";
    public int PropertyId { get; set; }
    public string PropertyName { get; set; } = "";
    public string PropertyImage { get; set; } = "";
    public DateTime CheckIn { get; set; }
    public DateTime CheckOut { get; set; }
    public int Guests { get; set; }
    public int Rooms { get; set; }
    public decimal TotalAmount { get; set; }
    public decimal AmountPaid { get; set; }
    public string Currency { get; set; } = "R";
    public string Status { get; set; } = "";
    public DateTime CreatedAt { get; set; }
    public string GuestName { get; set; } = "";
    public string GuestEmail { get; set; } = "";
}

public class BookingConfirmation
{
    public string Reference { get; set; } = "";
    public string PropertyName { get; set; } = "";
    public DateTime CheckIn { get; set; }
    public DateTime CheckOut { get; set; }
    public int Nights { get; set; }
    public decimal TotalAmount { get; set; }
    public string Currency { get; set; } = "R";
    public string Status { get; set; } = "Confirmed";
    public string GuestName { get; set; } = "";
}

public class UserProfile
{
    public int Id { get; set; }
    public string FullName { get; set; } = "";
    public string Email { get; set; } = "";
    public string Phone { get; set; } = "";
    public string Avatar { get; set; } = "";
    public int LoyaltyPoints { get; set; }
    public string MembershipTier { get; set; } = "Bronze";
    public List<Booking> BookingHistory { get; set; } = [];
}

public class ChatMessage
{
    public string Role { get; set; } = "";
    public string Content { get; set; } = "";
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}

public class GuideSection
{
    public string Title { get; set; } = "";
    public string Icon { get; set; } = "";
    public string Route { get; set; } = "";
    public string Description { get; set; } = "";
}

public class VisaInfo
{
    public string Nationality { get; set; } = "";
    public string DestinationCountry { get; set; } = "";
    public string Status { get; set; } = "";
    public int MaxStayDays { get; set; }
    public string Notes { get; set; } = "";
}
