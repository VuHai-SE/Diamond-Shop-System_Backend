using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using BusinessObjects;
using DAOs;
using Services.DTOs.Request;

[ApiVersion("2.0")]
[ApiExplorerSettings(GroupName = "v2")]
[Route("api/v2/Refund/")]
[ApiController]
public class RefundsController : ControllerBase
{
    private readonly RefundDAO _refundDAO;
    private readonly PaymentDAO _paymentDAO;

    public RefundsController(RefundDAO refundDAO, PaymentDAO paymentDAO)
    {
        _refundDAO = refundDAO;
        _paymentDAO = paymentDAO;
    }

    //[HttpPost]
    //public async Task<IActionResult> Refund([FromBody] RefundRequest refundRequest)
    //{
    //    var client = _httpClientFactory.CreateClient();

    //     Get PayPal access token
    //    var byteArray = Encoding.ASCII.GetBytes($"{_payPalOptions.ClientId}:{_payPalOptions.Secret}");
    //    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
    //    var tokenResponse = await client.PostAsync("https://api.sandbox.paypal.com/v1/oauth2/token", new StringContent("grant_type=client_credentials", Encoding.UTF8, "application/x-www-form-urlencoded"));
    //    var tokenResult = await tokenResponse.Content.ReadAsStringAsync();
    //    var token = JsonConvert.DeserializeObject<PayPalToken>(tokenResult);

    //     Create refund request
    //    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.access_token);
    //    var refundContent = new StringContent(JsonConvert.SerializeObject(new { amount = new { total = refundRequest.Amount, currency = refundRequest.Currency } }), Encoding.UTF8, "application/json");
    //    var refundResponse = await client.PostAsync($"https://api.sandbox.paypal.com/v1/payments/sale/{refundRequest.TransactionId}/refund", refundContent);
    //    var refundResult = await refundResponse.Content.ReadAsStringAsync();

    //    if (!refundResponse.IsSuccessStatusCode)
    //    {
    //        return BadRequest(refundResult);
    //    }

    //     Save refund information to database
    //    var refund = new TblRefund
    //    {
    //        PaymentId = refundRequest.PaymentID,
    //        RefundAmount = decimal.Parse(refundRequest.Amount),
    //        RefundStatus = "Completed",
    //        RefundDate = DateTime.UtcNow,
    //        Reason = refundRequest.Reason
    //    };

    //    await _refundDAO.MakeRefund(refund);

    //    return Ok(refund);
    //}
}

