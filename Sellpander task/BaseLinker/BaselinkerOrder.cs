using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sellpander_task.BaseLinker;

public class BaselinkerOrder
{
    [JsonProperty("order_source_id")]
    public int OrderSourceId { get; set; }

    [JsonProperty("order_status_id")]
    public int OrderStatusId { get; set; }

    [JsonProperty("date_add")]
    public string DateAdd { get; set; }

    [JsonProperty("order_source")]
    public string OrderSource { get; set; }

    [JsonProperty("products")]
    public List<BaselinkerProduct> Products { get; set; }

    [JsonProperty("shipping_address")]
    public BaselinkerAddress ShippingAddress { get; set; }

    [JsonProperty("date_in_status")]
    public string DateInStatus { get; set; }

    [JsonProperty("extra_field_1")]
    public string ExtraField1 { get; set; }

    [JsonProperty("currency")]
    public string Currency { get; set; }
}
