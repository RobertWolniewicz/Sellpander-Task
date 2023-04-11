using Newtonsoft.Json;
using RestSharp;
using Sellpander_task.Faire;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Sellpander_task.Services;

public class FaireService
{
    readonly string _accessToken;

    public FaireService(string accessToken)
    {
        _accessToken = accessToken;
    }

    public List<FaireOrder> GetFaireOrders()
    {
        var client = new RestClient("https://www.faire.com/api/v1/orders");
        var request = new RestRequest("GET");
        request.AddHeader("X-FAIRE-ACCESS-TOKEN", _accessToken);

        List<FaireOrder> allOrders = new List<FaireOrder>();
        int page = 1;
        bool moreOrders = true;

        while (moreOrders)
        {
            request.Parameters.RemoveParameter("created_at_min");
            request.Parameters.RemoveParameter("page");
            request.AddParameter("created_at_min", DateTime.UtcNow.AddMinutes(-11).ToString("s"));
            request.AddParameter("page", page.ToString());

            var response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception($"Failed to get Faire orders. Status code: {response.StatusCode}");
                moreOrders = false;
            }
            else
            {
                OrdersModel orders = JsonConvert.DeserializeObject<OrdersModel>(response.Content);
                allOrders.AddRange(orders.Orders);
                page++;
                moreOrders = orders.Orders.Count == 50;
            }
        }

        return allOrders;
    }
}
