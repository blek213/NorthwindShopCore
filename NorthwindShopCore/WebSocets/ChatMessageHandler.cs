﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NorthwindShopCore.Controllers.CustomClasses;

namespace NorthwindShopCore.WebSocets
{
    public class ChatMessageHandler : WebSocketHandler
    {
        HandleCookies handleCookies = new HandleCookies();


        public ChatMessageHandler(WebSocketConnectionManager webSocketConnectionManager) : base(webSocketConnectionManager)
        {

        }


        public override async Task OnConnected(WebSocket socket)
        {
              
            await base.OnConnected(socket);

            /*
            await SendMessageToAllAsync($"is now connected");
            */
        }

        public override async Task ReceiveAsync(WebSocket socket, WebSocketReceiveResult result, byte[] buffer)
        {
            var message = $"{Encoding.UTF8.GetString(buffer, 0, result.Count)}";

            await SendMessageToAllAsync(message);
        }

    }
}
