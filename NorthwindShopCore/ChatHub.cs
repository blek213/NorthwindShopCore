using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NorthwindShopCore
{
    public class ChatHub: Hub
    {
        public async Task Send()
        {
            await this.Clients.All.SendAsync("Send", "It's good weather today... ");
        }
    }
}
