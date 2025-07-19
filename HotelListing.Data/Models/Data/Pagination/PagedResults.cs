namespace HotelListing.Api.Data.Pagination
{
    public class PagedResults<T>
    {
        public int TotalRecord { get; set; }
        // Total number of records in DB
        public int CurrentPageNumber { get; set; }
        // The current page requested
        public int RecordsOnPage { get; set; }
        // Number of records in the current page
        public List<T> ResultsSet { get; set; }
        // The actual data/items (records) for the page
    }
}
