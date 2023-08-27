using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MilkRun.ApplicationCore.Contracts
{
    public class PagedResponse<T> : ApiResult<T> where T : class
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }

        public static PagedResponse<T> Success(T data, int pageNumber, int pageSize, int recordsCount)
        {
            return new PagedResponse<T>()
            {
                Data = data,
                Error = null,
                IsSuccess = true,
                PageIndex = pageNumber,
                TotalCount = recordsCount
            };
        }

        // Create BadRequest
        public static PagedResponse<T> BadRequest(string error, int pageNumber, int pageSize, int recordsCount)
        {
            return new PagedResponse<T>()
            {
                Data = default,
                Error = error,
                IsSuccess = false,
                PageIndex = pageNumber,
                TotalCount = recordsCount
            };
        }

    }
}
