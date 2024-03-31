namespace StackOverflowTagsAPI.Models.API {
    public class TagInfoRequest {
        public int Page { get; set; } = 1;
        public int ResultsPerPage { get; set; } = 100;
        public SortType SortType { get; set; }
        public bool DescendingOrder { get; set; } = true;
    }
}
