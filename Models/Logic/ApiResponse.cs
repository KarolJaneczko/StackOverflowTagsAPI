namespace StackOverflowTagsAPI.Models.Logic {
    public class ApiResponse {
        public bool IsSuccess { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
        private Exception Exception { get; }

        public ApiResponse(bool isSuccess, string title, string message, object data = null) {
            IsSuccess = isSuccess;
            Title = title;
            Message = message;
            Data = data;
        }

        public ApiResponse(string message, object data = null) {
            IsSuccess = data is not null;
            Title = string.Empty;
            Message = message;
            Data = data;
        }

        public ApiResponse(Exception exception) {
            IsSuccess = false;
            Title = "Exception";
            Message = exception.Message;
            Data = exception.Data;
            Exception = exception;
        }

        public ApiResponse(ApiException exception) {
            IsSuccess = false;
            Title = exception.ResponseTitle;
            Message = exception.ResponseMessage;
            Data = exception.Data;
            Exception = exception;
        }

        public Exception GetException() => Exception;
    }
}
