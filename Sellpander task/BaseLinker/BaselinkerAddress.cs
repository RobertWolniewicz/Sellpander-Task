using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sellpander_task.BaseLinker;

public class BaselinkerAddress
{

    [JsonProperty("delivery_fullname")]
    public string DeliveryFullname{ get; set; }

    [JsonProperty("phone")]
    public string Phone { get; set; }

    [JsonProperty("delivery_address")]
    public string DeliveryAddress { get; set; }

    [JsonProperty("delivery_postcode")]
    public string DeliveryPostCode { get; set; }

    [JsonProperty("delivery_city")]
    public string DeliveryCity { get; set; }

    [JsonProperty("delivery_state")]
    public string DeliveryState { get; set; }

    [JsonProperty("delivery_country")]
    public string DeliveryCountry { get; set; }

    [JsonProperty("delivery_country_code")]
    public string DeliveryCountryCode { get; set; }

    [JsonProperty("delivery_company")]
    public string DeliveryCompany { get; set; }
}
