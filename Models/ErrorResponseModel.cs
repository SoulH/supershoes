namespace Models
{
    public class ErrorResponseModel
    {
        public bool Success => false;
        public int ErrorCode { get; set; }
        public string ErrorMessage { get; set; }

        public static ErrorResponseModel BadRequest => new ErrorResponseModel() { ErrorCode = 400, ErrorMessage = "Bad request" };
        public static ErrorResponseModel NotAuthorized => new ErrorResponseModel() { ErrorCode = 401, ErrorMessage = "Not authorized" };
        public static ErrorResponseModel RecordNotFound => new ErrorResponseModel() { ErrorCode = 404, ErrorMessage = "Record not found" };
        public static ErrorResponseModel ServerError => new ErrorResponseModel() { ErrorCode = 500, ErrorMessage = "Server Error" };
    }
}
