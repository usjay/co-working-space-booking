namespace coreworking_space_booking_backend.Dtos.Responses
{
    public class BaseResponse<T>
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }

        public BaseResponse(int statusCode, string message = null, T data = default)
        {
            StatusCode = statusCode;
            Message = message;
            Data = data;
        }

        public static BaseResponse<T> SuccessResponse(T data, string message = "Request processed successfully")
        {
            return new BaseResponse<T>(200, message, data);
        }

        public static BaseResponse<T> ErrorResponse(int statusCode, string message)
        {
            return new BaseResponse<T>(statusCode, message, default);
        }

        public static BaseResponse<T> CreateSuccessResponse(string message = "Request processed successfully")
        {
            return new BaseResponse<T>(200, message, default);
        }
    }
}
