using ClassLibrary2.Classes;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Hubs
{
    public class TableHub : Hub
    {
        private readonly ISingletonSaver _singletonSaver;

        public TableHub(ISingletonSaver saver)
        {
            _singletonSaver = saver;
        }

        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
            await Clients.Caller.SendAsync("ServerIsReady");
        }

        public async Task GetBlock(int blockId)
        {
            Block[] blocks = _singletonSaver.GetBlocks();
            //Block bl = new Block();

            /*
            bl.name = "TestBlock";
            bl.jury = new string[] { "Me", "Me", "Also me" };
            bl.rows = new ParticipantRow[2];

            bl.rows[0] = new ParticipantRow();
            bl.rows[0].name = "Alex";

            bl.rows[1] = new ParticipantRow();
            bl.rows[1].name = "Fedora";
            */

            await Clients.Caller.SendAsync("SendBlockToUser", blocks[blockId]);
        }

        public async Task ChangeScoreOnServer(int blockId, int row, int column, string value)
        {
            Block[] blocks = _singletonSaver.GetBlocks();
            _singletonSaver.ChangeScore(blockId, row, column, float.Parse(value));
            await Clients.Others.SendAsync("ChangeScore",blockId, row, column, value);
            await Clients.All.SendAsync("ChangeScore", blockId, row, blocks[blockId].rows[row].scores.Length*3, blocks[blockId].rows[row].finalScore.ToString());
        }
    }
}
