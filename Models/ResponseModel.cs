using Newtonsoft.Json;
using System.Collections.Generic;

namespace Models
{
    public class ResponseModel<T>
    {
        [JsonProperty("success")]
        public bool Success { get; set; }
        [JsonProperty("error_code")]
        public int ErrorCode { get; set; }
        [JsonProperty("error_message")]
        public string ErrorMessage { get; set; }
        [JsonProperty("data")]
        public List<T> Data { get; set; }
        [JsonProperty("count")]
        public int Count { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }

        public ResponseModel() { Success = true; Type = typeof(T).Name; }

        public ResponseModel(dynamic model)
        {
            Success = true;
            if (model.GetType().GenericTypeArguments.Length > 0)
            {
                Count = model.Count;
                Data = model;
            }
            else
            {
                Count = 1;
                Data = new List<T>() { model };
            }
            Type = typeof(T).Name;
        }

        public static ResponseModel<T> BadRequest => new ResponseModel<T>() { Success = false, ErrorCode = 400, ErrorMessage = "Bad request", Type = typeof(T).Name };
        public static ResponseModel<T> NotAuthorized => new ResponseModel<T>() { Success = false, ErrorCode = 401, ErrorMessage = "Not authorized", Type = typeof(T).Name };
        public static ResponseModel<T> RecordNotFound => new ResponseModel<T>() { Success = false, ErrorCode = 404, ErrorMessage = "Record not found", Type = typeof(T).Name };
        public static ResponseModel<T> ServerError => new ResponseModel<T>() { Success = false, ErrorCode = 500, ErrorMessage = "Server Error", Type = typeof(T).Name };
    }
}
