using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sellpander_task.BaseLinker;

public class BaselinkerProduct
{
    [JsonProperty("product_id")]
    public string ProductId { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("quantity")]
    public int Quantity { get; set; }

    [JsonProperty("price_brutto")]
    public float PriceBrutto { get; set; }

    [JsonProperty("sku")]
    public string Sku { get; set; }
}
