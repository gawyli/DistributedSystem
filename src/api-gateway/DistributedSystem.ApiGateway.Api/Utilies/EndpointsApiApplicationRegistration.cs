using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace DistributedSystem.ApiGateway.Api.Utilies;

public static class EndpointsApiApplicationRegistration
{
    public static WebApplication RegisterDistributedSystemEndpoints(this WebApplication app)
    {
        var httpClient = new HttpClient();

        app.MapGet("/price/{itemId}", async (string itemId) =>
        {
            var response = await httpClient.GetAsync($"http://localhost:5001/price/{itemId}");
            return Results.Stream(await response.Content.ReadAsStreamAsync(), response.Content.Headers.ContentType?.ToString());
        });

        app.MapPost("/price/{itemId}", async (string itemId, [FromBody] JsonElement body) =>
        {
            var content = new StringContent(body.GetRawText(), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync($"http://localhost:5001/price/{itemId}", content);
            return Results.Stream(await response.Content.ReadAsStreamAsync(), response.Content.Headers.ContentType?.ToString());
        });

        app.MapGet("/inventory/{itemId}", async (string itemId) =>
        {
            var response = await httpClient.GetAsync($"http://localhost:5002/inventory/{itemId}");
            return Results.Stream(await response.Content.ReadAsStreamAsync(), response.Content.Headers.ContentType?.ToString());
        });

        app.MapPost("/inventory/{itemId}", async (string itemId, [FromBody] JsonElement body) =>
        {
            var content = new StringContent(body.GetRawText(), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync($"http://localhost:5002/inventory/{itemId}", content);
            return Results.Stream(await response.Content.ReadAsStreamAsync(), response.Content.Headers.ContentType?.ToString());
        });

        app.MapPost("/finance/approve", async ([FromBody] JsonElement body) =>
        {
            var content = new StringContent(body.GetRawText(), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync("http://localhost:5003/approve", content);
            return Results.Stream(await response.Content.ReadAsStreamAsync(), response.Content.Headers.ContentType?.ToString());
        });

        return app;
    }
}
