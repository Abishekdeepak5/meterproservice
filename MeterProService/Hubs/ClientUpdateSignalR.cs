using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
namespace MeterProService.Hubs
{
    public class ClientUpdateSignalR:Hub
    {
        String strVal="abc";
       
        public async Task LocationFromServer(double latitude, double longitude)
        {
            await Clients.Caller.SendAsync("ReceiveNewValue", latitude, longitude,strVal);
        }

        public async Task AddToGroup(String id)
        {
            strVal = id;
            //var groups = await Groups.RemoveFromGroupAsync;
            //var groups=await Groups.GetGroups
            await Groups.AddToGroupAsync(Context.ConnectionId, id);
            await Clients.Group(id).SendAsync("ResponseServer", id);
        }

        public async Task updateLatitudeLongitude(String id, double lat, double lng)
        {
            await Clients.OthersInGroup(id).SendAsync("LocationUpdated",id,lat,lng );
        }

        public async Task removeFromGroup(String id)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, id);
        }
    }
}

//public override async Task OnConnectedAsync()
//{
//    String str = Context.GetHttpContext().Request.Query["userId"];
//    strVal = str;
//    await Groups.AddToGroupAsync(Context.ConnectionId, str);
//    await base.OnConnectedAsync();
//}