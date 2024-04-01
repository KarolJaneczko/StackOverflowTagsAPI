using StackOverflowTagsAPI.Models.API;
using StackOverflowTagsAPI.Models.Logic;

namespace ApiTests {
    public class UnitTests {
        [Fact]
        public void IsSuccess_ExceptionInput_ReturnsFalse() {
            var apiResponse = new ApiResponse(new Exception());
            Assert.False(apiResponse.IsSuccess);
        }

        [Fact]
        public void IsSuccess_MessageInput_ReturnsFalse() {
            var apiResponse = new ApiResponse("Error");
            Assert.False(apiResponse.IsSuccess);
        }

        [Fact]
        public void IsSuccess_DataInput_ReturnsTrue() {
            var apiResponse = new ApiResponse("Success", new TagInfo { Count = 1 });
            Assert.True(apiResponse.IsSuccess);
        }
    }
}