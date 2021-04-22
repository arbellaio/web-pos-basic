using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using RecompildPOS.Models.Businesses;
using RecompildPOS.Models.Users;
using RecompildPOS.Services.Businesses;
using RecompildPOS.Services.Hub;
using RecompildPOS.Services.Sync;

namespace RecompildPOS.Web.Helpers
{
    public class RequestHub : Hub
    {
        public async Task RegisterBusinessSignal(RegisterBusinessSync registerBusinessSync)
        {
            if (registerBusinessSync != null)
            {
                await Clients.All.SendAsync("RegisterBusinessSignal", registerBusinessSync);
            }
        }
    }
}
