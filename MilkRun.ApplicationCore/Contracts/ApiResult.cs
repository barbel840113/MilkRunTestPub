using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MilkRun.ApplicationCore.Contracts
{
    [Serializable]
    public class ApiResult<T> where T : class
    {
        [JsonPropertyName("isSuccess")]
        public bool IsSuccess { get; set; }

        [JsonPropertyName("data")]
        public T? Data  { get; set; }

        [JsonPropertyName("error")]
        public string? Error { get; set; }
        public ApiResult() { }

        public static ApiResult<T> CreateSuccessResponse(T data)
        {
            return new ApiResult<T>
            {
                IsSuccess = true,
                Data = data
            };
        }

        public static ApiResult<T> CreateBadResponse(string error)
        {
            return new ApiResult<T>
            {
                IsSuccess = false,
                Error = error
            };
        }
    }
}
