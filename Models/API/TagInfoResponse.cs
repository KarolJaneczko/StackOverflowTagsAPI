namespace StackOverflowTagsAPI.Models.API {
    public class TagInfoResponse {
        public int Page { get; set; }
        public int ResultsPerPage { get; set; }
        public IEnumerable<TagInfo> TagInfos { get; set; }
    }
}
