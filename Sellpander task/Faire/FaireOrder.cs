using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sellpander_task.Faire;

public class FaireOrder
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("created_at")]
    public DateTime CreatedAt { get; set; }

    [JsonProperty("update_at")]
    public DateTime UpdateAt { get; set; }

    [JsonProperty("items")]
    public List<Item> Items { get; set; }

    [JsonProperty("address")]
    public ShippingAddress Address { get; set; }

    [JsonProperty("source")]
    public string Source { get; set; }

}
