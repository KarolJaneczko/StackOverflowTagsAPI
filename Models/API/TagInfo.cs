using StackOverflowTagsAPI.Models.StackOverflow;

namespace StackOverflowTagsAPI.Models.API {
    [Serializable]
    public class TagInfo(Tag tag) {
        public int Count { get; set; } = tag.Count;
        public string Name { get; set; } = tag.Name;
        public decimal PercentageShare { get; set; }
    }
}
