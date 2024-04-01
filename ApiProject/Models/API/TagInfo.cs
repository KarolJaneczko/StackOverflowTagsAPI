using StackOverflowTagsAPI.Models.StackOverflow;

namespace StackOverflowTagsAPI.Models.API {
    [Serializable]
    public class TagInfo {
        public int Count { get; set; }
        public string Name { get; set; }
        public decimal PercentageShare { get; set; }

        public TagInfo() {
            Count = 0;
            Name = string.Empty;
            PercentageShare = 0;
        }

        public TagInfo(Tag tag) {
            Count = tag.Count;
            Name = tag.Name;
            PercentageShare = 0.0m;
        }
    }
}
