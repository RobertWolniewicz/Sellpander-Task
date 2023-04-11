using Newtonsoft.Json;
using RestSharp;
using Sellpander_task.BaseLinker;
using Sellpander_task.Faire;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Sellpander_task.Services;

public class BaselinkerService
{
    readonly string _accessToken;

    public BaselinkerService(string accessToken)
    {
        _accessToken = accessToken;
    }
    public List<GetBaselinkerOrder> GetBaselinkerOrders()
    {
        var client = new RestClient("https://api.baselinker.com/connector.php");
        var request = new RestRequest("POST");
        request.AddHeader("X-BLToken", _accessToken);
        request.AddParameter("method", "getOrders");
        var dateConfirmedFrom = ((DateTimeOffset)DateTime.UtcNow.AddMinutes(-11)).ToUnixTimeSeconds();
        request.AddParameter("date_confirmed_from", dateConfirmedFrom);
        request.AddParameter("filter_order_source_id", 1024);
        request.AddParameter("status_id", 8069);

        var orders = new List<GetBaselinkerOrder>();
        do
        {
            var response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK)
                throw new Exception($"Failed to get Baselinker orders. Status code: {response.StatusCode}");
            var orderResponse = JsonConvert.DeserializeObject<GetBaselinkerOrdersModel>(response.Content);
            if (orderResponse.Status == "error")
                throw new Exception($"Failed to get Baselinker orders. Error message: {orderResponse.ErrorMessage}");
            orders.AddRange(orderResponse.Orders);

            dateConfirmedFrom = orderResponse.Orders.LastOrDefault().DateConfirmed.AddSeconds(1).ToUnixTimeSeconds();
            request.Parameters.RemoveParameter("date_confirmed_from");
            request.AddParameter("date_confirmed_from", dateConfirmedFrom);
        } while (orders.Count == 100);
        return orders;
    }
    public void AddBaselinkerOrders(List<FaireOrder> orders)
    {
        var client = new RestClient("https://api.baselinker.com/connector.php");
        var request = new RestRequest("POST");
        request.AddHeader("X-BLToken", _accessToken);
        request.AddParameter("method", "addOrder");

        var baselinkerOrders = new List<BaselinkerOrder>();
        foreach (var order in orders)
        {
            var baselinkerProducts = new List<BaselinkerProduct>();

            foreach (var item in order.Items)
            {
                var product = new BaselinkerProduct
                {
                    ProductId = item.ProductOptionId.ToString(),
                    Name = item.ProductOptionName,
                    Quantity = item.Quantity,
                    PriceBrutto = (float)item.PriceCents,
                    Sku = item.Sku
                };

                baselinkerProducts.Add(product);
            }
            var shippingAddress = new BaselinkerAddress
            {
                DeliveryFullname = order.Address.Name,
                Phone = order.Address.PhoneNumber,
                DeliveryAddress = order.Address.Address1 + order.Address.Address2,
                DeliveryCity = order.Address.City,
                DeliveryPostCode = order.Address.PostalCode,
                DeliveryState = order.Address.State,
                DeliveryCountry = order.Address.Country,
                DeliveryCountryCode = order.Address.CountryCode,
                DeliveryCompany = order.Address.CompanyName
            };
            var baselinkerOrder = new BaselinkerOrder
            {
                OrderSourceId = 1024,
                DateAdd = ((DateTimeOffset)order.CreatedAt).ToUnixTimeSeconds().ToString(),
                OrderSource = order.Source,
                Products = baselinkerProducts,
                ShippingAddress = shippingAddress,
                DateInStatus = ((DateTimeOffset)order.UpdateAt).ToUnixTimeSeconds().ToString(),
                OrderStatusId = 8069,
                ExtraField1 = order.Id.ToString(),
                Currency = "USD"
            };
            baselinkerOrders.Add(baselinkerOrder);
        }
        var json = JsonConvert.SerializeObject(baselinkerOrders);
        request.AddParameter("orders", json);
        var response = client.Execute(request);
        var content = response.Content;
        if (response.StatusCode != HttpStatusCode.OK)
        {
            throw new Exception($"Failed to post orders to Baselinker. Status code: {response.StatusCode}");
        }
    }
}
