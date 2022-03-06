using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClassLibrary2.Classes;

namespace WebApplication1.Hubs
{
    public class ViewHub : Hub
    {
        private readonly ISingletonSaver _singletonSaver;

        public ViewHub(ISingletonSaver saver)
        {
            _singletonSaver = saver;
        }

        public async Task RecieveDataOnServer(string[] array)
        {
            //Console.WriteLine("Receive array from Server, Last: " + array[array.Length-1]);
            await Clients.Others.SendAsync("SendDataToUser", array);
            _singletonSaver.SetBlocks(array);
        }

        public override async Task OnConnectedAsync()
        {
            //await Clients.Caller.SendAsync("SendDataToUser", new string[] { "Sama", "lama", "Duma" });
            await base.OnConnectedAsync();
            await Clients.Caller.SendAsync("SendDataToUser", _singletonSaver.GetBlockNames());
        }

        public async Task RecieveJuryOnServer(int blockId, string[] array)
        {
            _singletonSaver.SetJury(blockId, array);
        }

        public async Task RecieveParticipantsOnServer(int blockId, string[] array)
        {
            _singletonSaver.SetParticipants(blockId, array);
        }

        public async Task RecieveNotesOnServer(int blockId, string[] array)
        {
            _singletonSaver.SetNotes(blockId, array);
        }

        public async Task GetInfoAboutBlock(int blockId)
        {
            Block[] blocks = _singletonSaver.GetBlocks();
            await Clients.Caller.SendAsync("SendBlockInfoToUser", $"Jury: {blocks[blockId].jury.Length}, Participants: {blocks[blockId].rows.Length},Notes: { blocks[blockId].notes.Length}");
        }

    }

}
