using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sellpander_task.Faire;

public class Item
{
    [JsonProperty("product_option_id")]
    public string ProductOptionId { get; set; }

    [JsonProperty("quantity")]
    public int Quantity { get; set; }

    [JsonProperty("sku")]
    public string Sku { get; set; }

    [JsonProperty("price_cents")]
    public decimal PriceCents { get; set; }

    [JsonProperty("product_option_name")]
    public string ProductOptionName { get; set; }
}
