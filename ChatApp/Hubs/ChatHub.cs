﻿using Microsoft.AspNetCore.SignalR;

namespace ChatApp.Hubs
{
    /* connected Client  + Notification (Push)*/
    /* request URL ==> Hub */
    public class ChatHub:Hub
    {
        //notify all client when any client are connected
        //public override async Task OnConnectedAsync()
        //{
        //    await Clients.All.SendAsync("NewUserConnectNotify");//all online client
        //    //.................
            
        //}

        //Cousumer 
        public void SendText(string name,string text)//,int rate)
        {
            //save db
            //notify 
            //Clients.AllExcept(Context.ConnectionId).SendAsync("NewMessageNotify", name, text); //[mobile -WebApplication - desk]

            Clients.All.SendAsync("NewMessageNotify", name, text); //[mobile -WebApplication - desk]
        }

    }
}
