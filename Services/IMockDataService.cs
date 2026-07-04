using TravelWebApp.Models;

namespace TravelWebApp.Services;

public interface ITravelService
{
    Task<List<Destination>> GetDestinationsAsync();
    Task<Destination?> GetDestinationAsync(int id);
    Task<SearchResult> SearchPropertiesAsync(SearchFilters filters);
    Task<Property?> GetPropertyAsync(int id);
    Task<List<TravelDeal>> GetTravelDealsAsync();
    Task<BookingConfirmation> CreateBookingAsync(Booking booking);
    Task<List<Booking>> GetMyBookingsAsync(string email);
    Task<Booking?> GetBookingAsync(string reference);
    Task<UserProfile?> GetUserProfileAsync(string email);
    Task<List<VisaInfo>> GetVisaInfoAsync(string nationality);
    Task<List<GuideSection>> GetGuideSectionsAsync();
}

public class MockTravelService : ITravelService
{
    private List<Destination> _destinations = [];
    private List<Property> _properties = [];
    private List<TravelDeal> _deals = [];
    private List<Booking> _bookings = [];
    private List<VisaInfo> _visaInfo = [];
    private List<GuideSection> _guideSections = [];
    private int _bookingCounter = 1000;

    public MockTravelService()
    {
        SeedData();
    }

    public Task<List<Destination>> GetDestinationsAsync() =>
        Task.FromResult(_destinations);

    public Task<Destination?> GetDestinationAsync(int id) =>
        Task.FromResult(_destinations.FirstOrDefault(d => d.Id == id));

    public Task<SearchResult> SearchPropertiesAsync(SearchFilters filters)
    {
        var query = filters.Query?.ToLower() ?? "";
        var results = _properties.AsEnumerable();

        if (!string.IsNullOrWhiteSpace(query))
            results = results.Where(p =>
                p.Name.ToLower().Contains(query) ||
                p.DestinationName.ToLower().Contains(query) ||
                p.Location.ToLower().Contains(query));

        if (filters.MinPrice.HasValue)
            results = results.Where(p => p.PricePerNight >= filters.MinPrice.Value);
        if (filters.MaxPrice.HasValue)
            results = results.Where(p => p.PricePerNight <= filters.MaxPrice.Value);
        if (!string.IsNullOrWhiteSpace(filters.PropertyType))
            results = results.Where(p =>
                p.Type.ToLower() == filters.PropertyType.ToLower());
        if (filters.MinStars.HasValue)
            results = results.Where(p => p.Stars >= filters.MinStars.Value);
        if (filters.MinRating.HasValue)
            results = results.Where(p => p.Rating >= filters.MinRating.Value);
        if (filters.FreeCancellation == true)
            results = results.Where(p => p.FreeCancellation);

        results = filters.SortBy switch
        {
            "price_asc" => results.OrderBy(p => p.PricePerNight),
            "price_desc" => results.OrderByDescending(p => p.PricePerNight),
            "rating" => results.OrderByDescending(p => p.Rating),
            "stars" => results.OrderByDescending(p => p.Stars),
            _ => results.OrderBy(p => p.PricePerNight)
        };

        var list = results.ToList();
        var total = list.Count;
        var paged = list.Skip((filters.Page - 1) * filters.PageSize).Take(filters.PageSize).ToList();

        return Task.FromResult(new SearchResult
        {
            TotalCount = total,
            Results = paged,
            Page = filters.Page,
            TotalPages = (int)Math.Ceiling(total / (double)filters.PageSize)
        });
    }

    public Task<Property?> GetPropertyAsync(int id) =>
        Task.FromResult(_properties.FirstOrDefault(p => p.Id == id));

    public Task<List<TravelDeal>> GetTravelDealsAsync() =>
        Task.FromResult(_deals);

    public Task<BookingConfirmation> CreateBookingAsync(Booking booking)
    {
        booking.Reference = $"TRAV{++_bookingCounter}";
        booking.Status = "Confirmed";
        booking.CreatedAt = DateTime.UtcNow;
        _bookings.Add(booking);

        return Task.FromResult(new BookingConfirmation
        {
            Reference = booking.Reference,
            PropertyName = booking.PropertyName,
            CheckIn = booking.CheckIn,
            CheckOut = booking.CheckOut,
            Nights = (int)(booking.CheckOut - booking.CheckIn).TotalDays,
            TotalAmount = booking.TotalAmount,
            Currency = booking.Currency,
            Status = "Confirmed",
            GuestName = booking.GuestName
        });
    }

    public Task<List<Booking>> GetMyBookingsAsync(string email) =>
        Task.FromResult(_bookings.Where(b => b.GuestEmail == email).OrderByDescending(b => b.CreatedAt).ToList());

    public Task<Booking?> GetBookingAsync(string reference) =>
        Task.FromResult(_bookings.FirstOrDefault(b => b.Reference == reference));

    public Task<UserProfile?> GetUserProfileAsync(string email)
    {
        var profile = new UserProfile
        {
            Id = 1,
            FullName = "Sarah Mwale",
            Email = email,
            Phone = "+27 82 555 1234",
            Avatar = "",
            LoyaltyPoints = 2450,
            MembershipTier = "Gold",
            BookingHistory = _bookings.Where(b => b.GuestEmail == email).ToList()
        };
        return Task.FromResult<UserProfile?>(profile);
    }

    public Task<List<VisaInfo>> GetVisaInfoAsync(string nationality)
    {
        if (string.IsNullOrWhiteSpace(nationality))
            return Task.FromResult(_visaInfo);
        return Task.FromResult(_visaInfo.Where(v =>
            v.Nationality.Contains(nationality, StringComparison.OrdinalIgnoreCase) ||
            v.DestinationCountry.Contains(nationality, StringComparison.OrdinalIgnoreCase)).ToList());
    }

    public Task<List<GuideSection>> GetGuideSectionsAsync() =>
        Task.FromResult(_guideSections);

    private void SeedData()
    {
        _destinations =
        [
            new() { Id = 1, Name = "Cape Town", Country = "South Africa", Image = "capetown", Description = "Where mountains meet the ocean", StartingPrice = 850, Highlights = ["Table Mountain", "Boulders Beach", "V&A Waterfront"], BestSeason = "Oct-Apr", Wildlife = ["Penguins", "Whales"] },
            new() { Id = 2, Name = "Victoria Falls", Country = "Zimbabwe", Image = "vicfalls", Description = "The smoke that thunders", StartingPrice = 1200, Highlights = ["Victoria Falls", "Zambezi River", "Bungee Jumping"], BestSeason = "Apr-Oct", Wildlife = ["Elephants", "Hippos", "Crocodiles"] },
            new() { Id = 3, Name = "Kruger National Park", Country = "South Africa", Image = "kruger", Description = "Africa's wildest safari destination", StartingPrice = 2400, Highlights = ["Big Five", "Safari Drives", "Bush Walks"], BestSeason = "May-Sep", Wildlife = ["Lions", "Leopards", "Rhinos", "Elephants", "Buffalo"] },
            new() { Id = 4, Name = "Johannesburg", Country = "South Africa", Image = "joburg", Description = "The city of gold", StartingPrice = 650, Highlights = ["Maboneng", "Apartheid Museum", "Soweto"], BestSeason = "Year-round", Wildlife = ["Lion Park"] },
            new() { Id = 5, Name = "Zanzibar", Country = "Tanzania", Image = "zanzibar", Description = "Spice Island paradise", StartingPrice = 1800, Highlights = ["Stone Town", "Pristine Beaches", "Spice Tours"], BestSeason = "Jun-Oct", Wildlife = ["Sea Turtles", "Dolphins"] },
            new() { Id = 6, Name = "Serengeti", Country = "Tanzania", Image = "serengeti", Description = "Endless plains of the Great Migration", StartingPrice = 3200, Highlights = ["Great Migration", "Balloon Safaris", "Big Cats"], BestSeason = "Jun-Sep", Wildlife = ["Wildebeest", "Zebras", "Cheetahs", "Lions"] },
            new() { Id = 7, Name = "Okavango Delta", Country = "Botswana", Image = "okavango", Description = "Africa's last wetland wilderness", StartingPrice = 4500, Highlights = ["Mokoro Canoes", "Wildlife Viewing", "Bird Watching"], BestSeason = "May-Sep", Wildlife = ["Elephants", "Hippos", "Wild Dogs", "Buffalo"] },
            new() { Id = 8, Name = "Namib Desert", Country = "Namibia", Image = "namib", Description = "The oldest desert on earth", StartingPrice = 2100, Highlights = ["Sossusvlei", "Skeleton Coast", "Sand Dunes"], BestSeason = "Mar-Oct", Wildlife = ["Oryx", "Springbok", "Desert Elephants"] },
            new() { Id = 9, Name = "South Luangwa", Country = "Zambia", Image = "southluangwa", Description = "Valley of the leopards & walking safaris", StartingPrice = 2800, Highlights = ["Walking Safari", "Valley Game Drives", "Bird Watching"], BestSeason = "Jun-Oct", Wildlife = ["Leopards", "Lions", "Elephants", "Thornicroft Giraffes"] },
            new() { Id = 10, Name = "Lower Zambezi", Country = "Zambia", Image = "lowerzambezi", Description = "Canoe safaris along the majestic river", StartingPrice = 3000, Highlights = ["Canoe Safari", "Tiger Fishing", "Sunset Cruise"], BestSeason = "Jun-Nov", Wildlife = ["Elephants", "Hippos", "Crocodiles", "Buffaloes"] },
            new() { Id = 11, Name = "Kafue National Park", Country = "Zambia", Image = "kafue", Description = "Zambia's oldest & largest wilderness", StartingPrice = 2500, Highlights = ["Busanga Plains", "Tree-climbing Lions", "Kafue River boat ride"], BestSeason = "May-Nov", Wildlife = ["Lions", "Cheetahs", "Wild Dogs", "Sitatunga"] },
            new() { Id = 12, Name = "Kasanka National Park", Country = "Zambia", Image = "kasanka", Description = "Home of the giant bat migration", StartingPrice = 1800, Highlights = ["Bat Migration Canopy", "Wetland Birding", "Sitatunga Swamp Walk"], BestSeason = "Oct-Dec", Wildlife = ["Fruit Bats", "Sitatunga", "Shoebills"] },
            new() { Id = 13, Name = "Lake Bangweulu & Samfya", Country = "Zambia", Image = "samfya", Description = "White sand beaches & wetlands", StartingPrice = 1500, Highlights = ["Samfya Beach", "Bangweulu Swamps", "Black Lechwe Search"], BestSeason = "Year-round", Wildlife = ["Black Lechwes", "Shoebill Storks"] },
            new() { Id = 14, Name = "Lake Tanganyika", Country = "Zambia", Image = "tanganyika", Description = "Endless freshwater diving & angling", StartingPrice = 2200, Highlights = ["Scuba Diving", "Goliath Fishing", "Kalambo Falls Trek"], BestSeason = "May-Oct", Wildlife = ["Cichlids", "Hippos", "Fish Eagles"] },
            new() { Id = 15, Name = "Livingstone / Victoria Falls", Country = "Zambia", Image = "livingstone", Description = "Zambia's adventure & waterfall capital", StartingPrice = 2000, Highlights = ["Victoria Falls", "Devil's Pool swim", "Zambezi Boat Cruise"], BestSeason = "Feb-Jul", Wildlife = ["Zebra", "Elephants", "Giraffes"] },
            new() { Id = 16, Name = "Copperbelt", Country = "Zambia", Image = "copperbelt", Description = "Mining heritage & private game ranches", StartingPrice = 1500, Highlights = ["Ndola Golf Course", "Chimfunshi Chimp Sanctuary", "Ranch Game Drives"], BestSeason = "Year-round", Wildlife = ["Chimpanzees", "Impalas", "Wildebeests"] },
        ];

        _properties =
        [
            new() { Id = 1, Name = "The Table Bay Hotel", Type = "Hotel", DestinationId = 1, DestinationName = "Cape Town", Location = "V&A Waterfront, Cape Town", Image = "tablebay", PricePerNight = 1800, OriginalPrice = 2400, Rating = 4.8, ReviewCount = 234, Stars = 5, FreeCancellation = true, BreakfastIncluded = true,
                Amenities = ["Pool", "Parking", "WiFi", "Gym", "Spa", "Restaurant", "Airport Shuttle"],
                Gallery = ["tablebay1", "tablebay2", "tablebay3", "tablebay4"],
                Latitude = -33.9036, Longitude = 18.4231,
                Description = "Luxury 5-star hotel overlooking the V&A Waterfront with stunning views of Table Mountain and Robben Island." },
            new() { Id = 2, Name = "Gorgeous George", Type = "Hotel", DestinationId = 1, DestinationName = "Cape Town", Location = "City Centre, Cape Town", Image = "gorgeous", PricePerNight = 1350, Rating = 4.6, ReviewCount = 187, Stars = 4, FreeCancellation = true, BreakfastIncluded = true,
                Amenities = ["Rooftop Pool", "Restaurant", "Bar", "WiFi", "Air conditioning"],
                Gallery = ["gorgeous1", "gorgeous2"],
                Latitude = -33.9221, Longitude = 18.4211,
                Description = "Boutique hotel in the heart of Cape Town with a stunning rooftop pool and bar." },
            new() { Id = 3, Name = "Victoria Falls Safari Lodge", Type = "Lodge", DestinationId = 2, DestinationName = "Victoria Falls", Location = "Victoria Falls, Zimbabwe", Image = "viclodge", PricePerNight = 2800, OriginalPrice = 3500, Rating = 4.9, ReviewCount = 312, Stars = 5, FreeCancellation = true, BreakfastIncluded = true,
                Amenities = ["Pool", "Restaurant", "Bar", "Safari Tours", "WiFi", "Airport Shuttle"],
                Gallery = ["viclodge1", "viclodge2"],
                Latitude = -17.9244, Longitude = 25.8572,
                Description = "Award-winning lodge with breathtaking views of the Victoria Falls mist." },
            new() { Id = 4, Name = "Kruger Shalati", Type = "Lodge", DestinationId = 3, DestinationName = "Kruger National Park", Location = "Skukuza, Kruger NP", Image = "shalati", PricePerNight = 4500, OriginalPrice = 5800, Rating = 4.9, ReviewCount = 156, Stars = 5, FreeCancellation = false, BreakfastIncluded = true,
                Amenities = ["Pool", "Restaurant", "Safari Drives", "Spa", "WiFi", "Train Carriage Rooms"],
                Gallery = ["shalati1", "shalati2"],
                Latitude = -24.9948, Longitude = 31.5969,
                Description = "The legendary train-on-bridge hotel overlooking the Sabie River in Kruger." },
            new() { Id = 5, Name = "City Lodge Sandton", Type = "Hotel", DestinationId = 4, DestinationName = "Johannesburg", Location = "Sandton, Johannesburg", Image = "citylodge", PricePerNight = 950, Rating = 4.2, ReviewCount = 423, Stars = 3, FreeCancellation = true, BreakfastIncluded = false,
                Amenities = ["Pool", "Parking", "WiFi", "Gym", "Restaurant"],
                Latitude = -26.1076, Longitude = 28.0567,
                Description = "Convenient business hotel in Sandton's financial district." },
            new() { Id = 6, Name = "Zanzibar White Sands", Type = "Resort", DestinationId = 5, DestinationName = "Zanzibar", Location = "Nungwi, Zanzibar", Image = "whitesands", PricePerNight = 2200, OriginalPrice = 2900, Rating = 4.7, ReviewCount = 278, Stars = 4, FreeCancellation = true, BreakfastIncluded = true,
                Amenities = ["Pool", "Beach Access", "Spa", "Restaurant", "Bar", "WiFi", "Water Sports"],
                Latitude = -5.7289, Longitude = 39.3005,
                Description = "Luxury beach resort on Zanzibar's pristine northern coastline." },
            new() { Id = 7, Name = "Four Seasons Safari Lodge", Type = "Lodge", DestinationId = 6, DestinationName = "Serengeti", Location = "Serengeti NP, Tanzania", Image = "fsserengeti", PricePerNight = 6500, Rating = 4.9, ReviewCount = 89, Stars = 5, FreeCancellation = false, BreakfastIncluded = true,
                Amenities = ["Pool", "Spa", "Safari Drives", "Restaurant", "Bar", "WiFi", "Helipad"],
                Latitude = -2.3333, Longitude = 34.8333,
                Description = "Ultra-luxury safari lodge in the heart of the Serengeti." },
            new() { Id = 8, Name = "Sossusvlei Desert Lodge", Type = "Lodge", DestinationId = 8, DestinationName = "Namib Desert", Location = "Sossusvlei, Namibia", Image = "sossus", PricePerNight = 3800, Rating = 4.8, ReviewCount = 124, Stars = 5, FreeCancellation = false, BreakfastIncluded = true,
                Amenities = ["Pool", "Restaurant", "Star Gazing", "Safari Drives", "WiFi"],
                Latitude = -24.8877, Longitude = 15.8450,
                Description = "Exclusive desert lodge surrounded by the iconic red dunes of Sossusvlei." },
            new() { Id = 9, Name = "Cape Grace Hotel", Type = "Hotel", DestinationId = 1, DestinationName = "Cape Town", Location = "V&A Waterfront, Cape Town", Image = "capegrace", PricePerNight = 3200, Rating = 4.8, ReviewCount = 198, Stars = 5, FreeCancellation = true, BreakfastIncluded = true,
                Amenities = ["Pool", "Spa", "Restaurant", "Bar", "WiFi", "Gym", "Butler Service"],
                Latitude = -33.9030, Longitude = 18.4215,
                Description = "World-class luxury hotel on its own private quay at the V&A Waterfront." },
            new() { Id = 10, Name = "Sun City Palace", Type = "Resort", DestinationId = 4, DestinationName = "Johannesburg", Location = "Sun City, North West", Image = "suncity", PricePerNight = 1600, OriginalPrice = 2100, Rating = 4.3, ReviewCount = 567, Stars = 4, FreeCancellation = true, BreakfastIncluded = true,
                Amenities = ["Pool", "Casino", "Golf", "Restaurant", "Bar", "Water Park", "WiFi", "Spa"],
                Latitude = -25.3400, Longitude = 27.0900,
                Description = "Iconic resort with world-class entertainment and the Valley of Waves." },
            new() { Id = 11, Name = "Chobe Game Lodge", Type = "Lodge", DestinationId = 7, DestinationName = "Okavango Delta", Location = "Chobe NP, Botswana", Image = "chobe", PricePerNight = 5200, OriginalPrice = 6200, Rating = 4.9, ReviewCount = 203, Stars = 5, FreeCancellation = true, BreakfastIncluded = true,
                Amenities = ["Pool", "Safari Drives", "Boat Cruises", "Restaurant", "Bar", "WiFi", "Helipad"],
                Latitude = -17.8200, Longitude = 25.1500,
                Description = "Botswana's premier game lodge on the banks of the Chobe River." },
            new() { Id = 12, Name = "Protea Hotel Stellenbosch", Type = "Hotel", DestinationId = 1, DestinationName = "Cape Town", Location = "Stellenbosch, Cape Winelands", Image = "proteastell", PricePerNight = 1100, Rating = 4.1, ReviewCount = 345, Stars = 3, FreeCancellation = true, BreakfastIncluded = true,
                Amenities = ["Pool", "Parking", "WiFi", "Restaurant", "Wine Tours"],
                Latitude = -33.9321, Longitude = 18.8602,
                Description = "Charming hotel in the heart of South Africa's premier wine region." },
            new() { Id = 13, Name = "Time + Tide Chinzombo", Type = "Lodge", DestinationId = 9, DestinationName = "South Luangwa", Location = "South Luangwa National Park, Zambia", Image = "chinzombo", PricePerNight = 4800, Rating = 4.9, ReviewCount = 87, Stars = 5, FreeCancellation = true, BreakfastIncluded = true,
                Amenities = ["Pool", "Safari Drives", "Walking Safaris", "Restaurant", "Bar", "WiFi", "Airport Shuttle"],
                Latitude = -13.065, Longitude = 31.791,
                Description = "Award-winning luxury lodge offering a high level of comfort and privacy on the banks of the Luangwa River." },
            new() { Id = 14, Name = "Chiawa Camp", Type = "Lodge", DestinationId = 10, DestinationName = "Lower Zambezi", Location = "Lower Zambezi National Park, Zambia", Image = "chiawa", PricePerNight = 4200, Rating = 4.8, ReviewCount = 64, Stars = 5, FreeCancellation = true, BreakfastIncluded = true,
                Amenities = ["Pool", "Canoe Safaris", "Boat Cruises", "Angling", "Restaurant", "Bar", "WiFi"],
                Latitude = -15.651, Longitude = 29.378,
                Description = "Nestled in a grove of mahogany trees, Chiawa Camp offers premier river and land guiding." },
            new() { Id = 15, Name = "Chisa Busanga Camp", Type = "Lodge", DestinationId = 11, DestinationName = "Kafue National Park", Location = "Busanga Plains, Kafue, Zambia", Image = "chisabusanga", PricePerNight = 4000, Rating = 4.9, ReviewCount = 42, Stars = 5, FreeCancellation = true, BreakfastIncluded = true,
                Amenities = ["Pool", "Safari Drives", "Balloon Safaris", "Restaurant", "Bar", "WiFi"],
                Latitude = -14.996, Longitude = 25.864,
                Description = "Famous for its unique bird nest-inspired rooms elevated above the remote Busanga Plains." },
            new() { Id = 16, Name = "Chita Lodge Samfya", Type = "Resort", DestinationId = 13, DestinationName = "Lake Bangweulu & Samfya", Location = "Samfya Beach, Luapula, Zambia", Image = "chitasamfya", PricePerNight = 1800, Rating = 4.7, ReviewCount = 98, Stars = 4, FreeCancellation = true, BreakfastIncluded = true,
                Amenities = ["Pool", "Beach Access", "Boat Cruises", "Restaurant", "Bar", "WiFi", "Water Sports"],
                Latitude = -11.362, Longitude = 29.551,
                Description = "Lakeside beach retreat offering beautiful views and white sand relaxation on the shores of Lake Bangweulu." },
            new() { Id = 17, Name = "Ndole Bay Lodge", Type = "Lodge", DestinationId = 14, DestinationName = "Lake Tanganyika", Location = "Lake Tanganyika, Northern Province, Zambia", Image = "ndolebay", PricePerNight = 2400, Rating = 4.8, ReviewCount = 76, Stars = 4, FreeCancellation = true, BreakfastIncluded = true,
                Amenities = ["Pool", "Beach Access", "Scuba Diving", "Angling", "Restaurant", "Bar", "WiFi", "Safari Tours"],
                Latitude = -8.468, Longitude = 30.432,
                Description = "Adventure lodge on a private beach along Lake Tanganyika, serving as the gateway to Nsumbu National Park." },
            new() { Id = 18, Name = "Isanga Bay Resort", Type = "Resort", DestinationId = 14, DestinationName = "Lake Tanganyika", Location = "Lake Tanganyika, Northern Province, Zambia", Image = "isangabay", PricePerNight = 2100, Rating = 4.7, ReviewCount = 52, Stars = 4, FreeCancellation = true, BreakfastIncluded = true,
                Amenities = ["Beach Access", "Dhow Sailing", "Kayaking", "Restaurant", "Bar", "WiFi"],
                Latitude = -8.512, Longitude = 30.505,
                Description = "A secluded island-paradise style resort located on a beautiful sandy bay of Lake Tanganyika." },
            new() { Id = 19, Name = "The Royal Livingstone Hotel", Type = "Hotel", DestinationId = 15, DestinationName = "Livingstone / Victoria Falls", Location = "Mosi-oa-Tunya Road, Livingstone, Zambia", Image = "royallivingstone", PricePerNight = 3500, OriginalPrice = 4500, Rating = 4.9, ReviewCount = 289, Stars = 5, FreeCancellation = true, BreakfastIncluded = true,
                Amenities = ["Pool", "Spa", "Zambezi Cruises", "Helipad", "Restaurant", "Bar", "WiFi", "Gym", "Butler Service"],
                Latitude = -17.918, Longitude = 25.852,
                Description = "5-star luxury hotel on the banks of the Zambezi, offering colonial elegance and direct entry to Victoria Falls." },
            new() { Id = 20, Name = "Tongabezi Lodge", Type = "Lodge", DestinationId = 15, DestinationName = "Livingstone / Victoria Falls", Location = "Zambezi River, Livingstone, Zambia", Image = "tongabezi", PricePerNight = 3800, Rating = 4.9, ReviewCount = 143, Stars = 5, FreeCancellation = true, BreakfastIncluded = true,
                Amenities = ["Pool", "Boat Cruises", "Canoeing", "Private Dining", "Restaurant", "Bar", "WiFi"],
                Latitude = -17.901, Longitude = 25.768,
                Description = "Legendary, privately owned romantic hideaway located on the banks of the Zambezi River, famous for treehouses." },
            new() { Id = 21, Name = "Garden Court Kitwe", Type = "Hotel", DestinationId = 16, DestinationName = "Copperbelt", Location = "Kitwe, Copperbelt Province, Zambia", Image = "gardencourt", PricePerNight = 1600, Rating = 4.6, ReviewCount = 198, Stars = 4, FreeCancellation = true, BreakfastIncluded = true,
                Amenities = ["Pool", "Parking", "WiFi", "Gym", "Restaurant", "Bar", "Conference Center"],
                Latitude = -12.798, Longitude = 28.232,
                Description = "A premium business hotel in Kitwe offering modern rooms, large conference centers, and outdoor pools." },
            new() { Id = 22, Name = "Protea Hotel Ndola", Type = "Hotel", DestinationId = 16, DestinationName = "Copperbelt", Location = "Ndola, Copperbelt Province, Zambia", Image = "proteandola", PricePerNight = 1500, Rating = 4.5, ReviewCount = 154, Stars = 4, FreeCancellation = true, BreakfastIncluded = true,
                Amenities = ["Pool", "Parking", "WiFi", "Restaurant", "Bar", "Airport Shuttle"],
                Latitude = -12.982, Longitude = 28.641,
                Description = "Adjacent to the Levy Mwanawasa Stadium, a premier hotel option for business, events, and sports tourists." },
            new() { Id = 23, Name = "Mokorro Game Ranch & Lodge", Type = "Lodge", DestinationId = 16, DestinationName = "Copperbelt", Location = "Chingola, Copperbelt Province, Zambia", Image = "mokorro", PricePerNight = 1800, Rating = 4.6, ReviewCount = 92, Stars = 4, FreeCancellation = true, BreakfastIncluded = true,
                Amenities = ["Pool", "Game Drives", "Restaurant", "Bar", "WiFi"],
                Latitude = -12.521, Longitude = 27.871,
                Description = "Combining town hotel hospitality with a game ranch experience, located on the outskirts of Chingola." },
            new() { Id = 24, Name = "The Urban Hotel Ndola", Type = "Hotel", DestinationId = 16, DestinationName = "Copperbelt", Location = "Ndola, Copperbelt Province, Zambia", Image = "urbanndola", PricePerNight = 1700, Rating = 4.7, ReviewCount = 104, Stars = 4, FreeCancellation = true, BreakfastIncluded = true,
                Amenities = ["Pool", "Garden Bar", "WiFi", "Gym", "Restaurant"],
                Latitude = -12.964, Longitude = 28.618,
                Description = "A trendy boutique design hotel in Ndola featuring modern art, a cocktail deck, and high-speed executive facilities." },
        ];

        _deals =
        [
            new() { Id = 1, Title = "Safari Special", Description = "40% off luxury safari lodges", DiscountPercent = 40, ValidUntil = DateTime.UtcNow.AddDays(20), Badge = "Limited Time", Image = "safarideal" },
            new() { Id = 2, Title = "Early Bird", Description = "15% off when you book 60+ days ahead", DiscountPercent = 15, ValidUntil = DateTime.UtcNow.AddDays(90), Badge = "Early Bird", Image = "earlybird" },
            new() { Id = 3, Title = "Weekend Escape", Description = "Flash sale: 3 days only", DiscountPercent = 25, ValidUntil = DateTime.UtcNow.AddDays(3), Badge = "Flash Sale", Image = "weekend" },
            new() { Id = 4, Title = "Honeymoon Package", Description = "Free upgrade + romantic dinner", DiscountPercent = 20, ValidUntil = DateTime.UtcNow.AddDays(60), Badge = "Romance", Image = "honeymoon" },
        ];

        _visaInfo =
        [
            new() { Nationality = "South Africa", DestinationCountry = "Botswana", Status = "Visa Free", MaxStayDays = 90, Notes = "Valid passport required" },
            new() { Nationality = "South Africa", DestinationCountry = "Zambia", Status = "Visa Free", MaxStayDays = 90, Notes = "Valid passport required" },
            new() { Nationality = "South Africa", DestinationCountry = "Kenya", Status = "Visa Free", MaxStayDays = 30, Notes = "E-visa available" },
            new() { Nationality = "South Africa", DestinationCountry = "Tanzania", Status = "Visa on Arrival", MaxStayDays = 90, Notes = "USD 50 fee, passport photos required" },
            new() { Nationality = "South Africa", DestinationCountry = "Namibia", Status = "Visa Free", MaxStayDays = 90, Notes = "Valid passport required" },
            new() { Nationality = "South Africa", DestinationCountry = "Zimbabwe", Status = "Visa Free", MaxStayDays = 90, Notes = "Valid passport required" },
            new() { Nationality = "South Africa", DestinationCountry = "Mozambique", Status = "Visa Free", MaxStayDays = 30, Notes = "Border processing fee applicable" },
            new() { Nationality = "South Africa", DestinationCountry = "United Kingdom", Status = "Visa Required", MaxStayDays = 0, Notes = "Standard Visitor Visa required. Apply 3+ months ahead." },
            new() { Nationality = "South Africa", DestinationCountry = "United States", Status = "Visa Required", MaxStayDays = 0, Notes = "B1/B2 Visa required. Apply 6+ months ahead." },
            new() { Nationality = "South Africa", DestinationCountry = "Schengen Area", Status = "Visa Required", MaxStayDays = 90, Notes = "Schengen Visa required. Apply 3+ months ahead." },
        ];

        _guideSections =
        [
            new() { Title = "Packing List", Icon = "luggage", Route = "/guide/packing", Description = "What to bring for your African adventure" },
            new() { Title = "Visa Guide", Icon = "passport", Route = "/guide/visa", Description = "Visa requirements and visa-free travel" },
            new() { Title = "Health & Safety", Icon = "health", Route = "/guide/health", Description = "Vaccinations, insurance, and safety tips" },
            new() { Title = "Park Rules", Icon = "park", Route = "/guide/rules", Description = "National park regulations and etiquette" },
            new() { Title = "Currency & Payments", Icon = "currency", Route = "/guide/currency", Description = "Payment methods and tipping guide" },
            new() { Title = "Travel Insurance", Icon = "insurance", Route = "/guide/insurance", Description = "Protect your trip with comprehensive cover" },
        ];

        _bookings = [
            new() { Reference = "TRAV1001", PropertyId = 1, PropertyName = "The Table Bay Hotel", PropertyImage = "tablebay", CheckIn = DateTime.UtcNow.AddDays(30), CheckOut = DateTime.UtcNow.AddDays(33), Guests = 2, Rooms = 1, TotalAmount = 5400, AmountPaid = 5400, Currency = "R", Status = "Confirmed", CreatedAt = DateTime.UtcNow.AddDays(-5), GuestName = "Sarah Mwale", GuestEmail = "sarah@example.com" },
            new() { Reference = "TRAV1002", PropertyId = 6, PropertyName = "Zanzibar White Sands", PropertyImage = "whitesands", CheckIn = DateTime.UtcNow.AddDays(60), CheckOut = DateTime.UtcNow.AddDays(67), Guests = 2, Rooms = 1, TotalAmount = 15400, AmountPaid = 7700, Currency = "R", Status = "Confirmed", CreatedAt = DateTime.UtcNow.AddDays(-20), GuestName = "Sarah Mwale", GuestEmail = "sarah@example.com" },
            new() { Reference = "TRAV1003", PropertyId = 5, PropertyName = "City Lodge Sandton", PropertyImage = "citylodge", CheckIn = DateTime.UtcNow.AddDays(-10), CheckOut = DateTime.UtcNow.AddDays(-8), Guests = 1, Rooms = 1, TotalAmount = 1900, AmountPaid = 1900, Currency = "R", Status = "Completed", CreatedAt = DateTime.UtcNow.AddDays(-30), GuestName = "Sarah Mwale", GuestEmail = "sarah@example.com" },
        ];
    }
}
