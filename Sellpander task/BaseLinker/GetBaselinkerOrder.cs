using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sellpander_task.BaseLinker;

public class GetBaselinkerOrder
{

    [JsonProperty("date_confirmed")]
    public DateTimeOffset DateConfirmed { get; set; }

    [JsonProperty("extra_field_1")]
    public string ExtraField1 { get; set; }
}
