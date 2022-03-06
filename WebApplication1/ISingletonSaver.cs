using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClassLibrary2.Classes;

namespace WebApplication1
{
    public interface ISingletonSaver
    {
        string[] GetBlockNames();
        void SetBlocks(string[] data);

        void SetJury(int blockId, string[] jury);
        void SetParticipants(int blockId, string[] participants);
        void SetNotes(int blockId, string[] notes);
        void ChangeScore(int blockId, int row, int column, float value);
        Block[] GetBlocks();
    }
}
