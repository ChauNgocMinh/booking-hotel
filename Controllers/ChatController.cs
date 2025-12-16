using HotelManagement.Response;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

public class ChatController : Controller
{
    private readonly IHttpClientFactory _factory;
    private readonly IConfiguration _config;

    public ChatController(IHttpClientFactory factory, IConfiguration config)
    {
        _factory = factory;
        _config = config;
    }
    [HttpPost]
    public async Task<IActionResult> Ask([FromBody] ChatRequest request)
    {
        var apiKey = _config["Gemini:ApiKey"];
        if (string.IsNullOrEmpty(apiKey))
            return Json(new { reply = "Không tìm thấy Gemini API Key" });

        var client = _factory.CreateClient();
        client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

        var body = new
        {
            model = "gemini-2.5-flash",
            messages = new object[]
            {
            new
            {
                role = "system",
                content =
                "Bạn là nhân viên tư vấn cho website đặt phòng khách sạn. " +
                "Luôn trả lời bằng tiếng Việt. " +
                "Giọng điệu lịch sự, chuyên nghiệp, ngắn gọn. " +
                "Ưu tiên tư vấn phòng, giá, thời gian lưu trú, dịch vụ, chính sách đặt phòng. " +
                "Không trả lời nội dung ngoài phạm vi khách sạn."
            },
            new
            {
                role = "user",
                content = request.Message
            }
            }
        };

        var response = await client.PostAsync(
            "https://generativelanguage.googleapis.com/v1beta/openai/chat/completions",
            new StringContent(
                JsonConvert.SerializeObject(body),
                Encoding.UTF8,
                "application/json")
        );

        var json = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            return Json(new { reply = json });
        }

        dynamic result = JsonConvert.DeserializeObject(json);

        return Json(new
        {
            reply = result.choices[0].message.content.ToString()
        });
    }
}

public class ChatRequest
{
    public string Message { get; set; }
}
