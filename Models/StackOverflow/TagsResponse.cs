using System.Text.Json.Serialization;

namespace StackOverflowTagsAPI.Models.StackOverflow {
    public class TagsResponse {
        [JsonPropertyName("items")]
        public List<Tag> Items { get; set; } = [];

        [JsonPropertyName("has_more")]
        public bool HasMore { get; set; }

        [JsonPropertyName("quota_max")]
        public int QuotaMax { get; set; }

        [JsonPropertyName("quota_remaining")]
        public int QuotaRemaining { get; set; }
    }
}
