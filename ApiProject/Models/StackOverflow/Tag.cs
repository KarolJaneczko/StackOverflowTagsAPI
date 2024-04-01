using System.Text.Json.Serialization;

namespace StackOverflowTagsAPI.Models.StackOverflow {
    public class Tag {
        [JsonPropertyName("count")]
        public int Count { get; set; }

        [JsonPropertyName("has_synonyms")]
        public bool HasSynonyms { get; set; }

        [JsonPropertyName("is_moderator_only")]
        public bool IsModeratorOnly { get; set; }

        [JsonPropertyName("is_required")]
        public bool IsRequired { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("user_id")]
        public int UserId { get; set; }
    }
}
