using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MilkRun.ApplicationCore.Contracts.QueryParams
{
    public class QueryParameter : PagingParameter
    {
        [JsonPropertyName("orderBy")]
        public virtual string? OrderBy { get; set; }
        [JsonPropertyName("orderDir")]
        public virtual string? OrderDir { get; set; }
    }
}
