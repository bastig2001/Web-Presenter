using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace WebPresenter.Hubs {
    public class TextHub : Hub {
        public async Task ChangeText(object text) {
            await Clients.Others.SendCoreAsync("textChanged", new[] {text});
        }
    }
}