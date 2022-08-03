using System.ComponentModel.DataAnnotations;

namespace ToDoApp.Models.Payloads.Requests
{
    public class TodoPaginationRequest
    {
        //string sortOrder, string searching, string filter_complete, string filter_status, int maxPageSize, int pageNumber
        public string sortOrder { get; set; } = String.Empty;

        public string searching { get; set; } = String.Empty;

        public string filter_complete { get; set; } = String.Empty;

        public string filter_status { get; set; } = String.Empty;

        [Required(ErrorMessage = "max page size is invalid")]
        public int maxPageSize { get; set; }

        [Required(ErrorMessage = "page number is invalid")]
        public int pageNumber { get; set; }
    }
}
