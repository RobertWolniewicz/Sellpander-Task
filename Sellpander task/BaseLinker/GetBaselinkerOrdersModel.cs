using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sellpander_task.BaseLinker;

public class GetBaselinkerOrdersModel
{
    [JsonProperty("status")]
    public string Status { get; set; }

    [JsonProperty("orders")]
    public List<GetBaselinkerOrder> Orders { get; set; }

    [JsonProperty("error_message")]
    public string ErrorMessage { get; set; }

    [JsonProperty("error_code")]
    public int ErrorCode { get; set; }
}
