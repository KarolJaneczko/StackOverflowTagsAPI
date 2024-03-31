namespace StackOverflowTagsAPI.Models.Logic {
    [Serializable]
    public class ApiLoggerConfiguration {
        public bool IsLoggerEnabled { get; set; }
        public IEnumerable<string> EnabledLogs { get; set; }
    }
}
