using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MilkRun.ApplicationCore.Contracts.QueryParams
{
    public class PagingParameter
    {
        private const int maxPageSize = 200;

        [JsonPropertyName("pageNumber")]
        public int PageNumber { get; set; } = 1;
        private int _pageSize = 10;

        [JsonPropertyName("pageSize")]
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = (value > maxPageSize) ? maxPageSize : value;
            }
        }
    }
}
