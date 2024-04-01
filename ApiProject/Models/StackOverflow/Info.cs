using System.Text.Json.Serialization;

namespace StackOverflowTagsAPI.Models.StackOverflow {
    public class Info {
        [JsonPropertyName("new_active_users")]
        public int NewActiveUsers { get; set; }

        [JsonPropertyName("total_users")]
        public int TotalUsers { get; set; }

        [JsonPropertyName("badges_per_minute")]
        public decimal BadgesPerMinute { get; set; }

        [JsonPropertyName("total_badges")]
        public int TotalBadges { get; set; }

        [JsonPropertyName("total_votes")]
        public int TotalVotes { get; set; }

        [JsonPropertyName("total_comments")]
        public int TotalComments { get; set; }

        [JsonPropertyName("answers_per_minute")]
        public decimal AnswersPerMinute { get; set; }

        [JsonPropertyName("questions_per_minute")]
        public decimal QuestionsPerMinute { get; set; }

        [JsonPropertyName("total_answers")]
        public int TotalAnswers { get; set; }

        [JsonPropertyName("total_accepted")]
        public int TotalAccepted { get; set; }

        [JsonPropertyName("total_unanswered")]
        public int TotalUnanswered { get; set; }

        [JsonPropertyName("total_questions")]
        public int TotalQuestions { get; set; }

        [JsonPropertyName("api_revision")]
        public string ApiRevision { get; set; }
    }
}
