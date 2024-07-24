using BusinessObject;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;


namespace AppleStore.Hubs
{
    public class ProductHub : Hub
    {
        public async Task SendProductUpdate(Product product)
        {
            await Clients.All.SendAsync("ReceiveProductUpdate", product);
        }
        public async Task NotifyProductDeleted(int ProductId)
        {
            await Clients.All.SendAsync("ReceiveProductDeletion", ProductId);
        }
    }
}
