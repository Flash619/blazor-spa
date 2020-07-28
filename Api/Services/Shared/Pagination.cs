namespace Cmta.Clients.Spa.Features.Shared
{
    public class Pagination
    {
        public int CurrentPage { get; set; } = 1;
        public int Pages { get; set; } = 5;
        public int PageSize { get; set; } = 10;
    }
}