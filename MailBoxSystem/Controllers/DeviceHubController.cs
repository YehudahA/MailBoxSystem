using MailBoxSystem.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;
using System.Text;

namespace MailBoxSystem.Controllers;

public class DeviceHubController : ControllerBase
{
    [HttpGet("/ws")]
    public async Task Get([FromServices] IDeviceHubService deviceHubService)
    {
        if (HttpContext.WebSockets.IsWebSocketRequest)
        {
            CancellationToken ct = HttpContext.RequestAborted;
            using var webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();

            while (!ct.IsCancellationRequested)
            {
                var door = deviceHubService.GetDoor();

                if (door.HasValue)
                {
                    await SendStringAsync(webSocket, "OPEN " + door);
                }

                await Task.Delay(1000);
            }
        }
        else
        {
            HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
        }
    }

    private static async Task Echo(WebSocket webSocket)
    {
        var buffer = new byte[1024 * 4];
        var receiveResult = await webSocket.ReceiveAsync(
            new ArraySegment<byte>(buffer), CancellationToken.None);

        while (!receiveResult.CloseStatus.HasValue)
        {
            await webSocket.SendAsync(
                new ArraySegment<byte>(buffer, 0, receiveResult.Count),
                receiveResult.MessageType,
                receiveResult.EndOfMessage,
                CancellationToken.None);

            receiveResult = await webSocket.ReceiveAsync(
                new ArraySegment<byte>(buffer), CancellationToken.None);
        }

        await webSocket.CloseAsync(
            receiveResult.CloseStatus.Value,
            receiveResult.CloseStatusDescription,
            CancellationToken.None);
    }

    private static Task SendStringAsync(WebSocket socket, string data, CancellationToken ct = default)
    {
        var buffer = Encoding.UTF8.GetBytes(data);
        var segment = new ArraySegment<byte>(buffer);
        return socket.SendAsync(segment, WebSocketMessageType.Text, true, ct);
    }
}