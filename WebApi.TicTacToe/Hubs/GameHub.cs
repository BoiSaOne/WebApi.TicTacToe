using Microsoft.AspNetCore.SignalR;
using WebApi.TicTacToe.Models;

namespace WebApi.TicTacToe.Hubs
{
    public class GameHub : Hub<IGameHub>
    {
        //public async Task NotifyGroupAsync(string groupName)
        //{
        //    await Clients.Groups(groupName).NotifyGroupAsync();
        //}

        public async Task AddUserInGroupAsync(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        }

        public async Task RemoveUserFromGroupAsync(string groupName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
        }
    }
}
