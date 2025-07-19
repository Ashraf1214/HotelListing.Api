using System.ComponentModel;

namespace HotelListing.Api.Data.Pagination
{
    public class QueryParameters
    {
        private int _pageSize = 5;
        private int _currentPage = 1;
        public int Skip => (CurrentPage - 1) * PageSize;

        public int CurrentPage 
        {
            get => _currentPage;
            set => _currentPage = value < 1 ? 1 : value; // Ensure current page is at least 1
        }
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = value;
        }
    }
}
