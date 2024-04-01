namespace StackOverflowTagsAPI.Models.Logic {
    public class ApiException : Exception {
        public string ResponseTitle { get; set; } = "Error";
        public string ResponseMessage { get; set; }

        public ApiException() : base("Unexpected error") {
            ResponseMessage = "Unexpected error";
        }

        public ApiException(string message) {
            ResponseMessage = message;
        }

        public ApiException(string message, Exception innerException) : base(message, innerException) {
            ResponseMessage = message;
        }

        public ApiException(string title, string message) {
            ResponseTitle = title;
            ResponseMessage = message;
        }
    }
}
