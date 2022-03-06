using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClassLibrary2.Classes;

namespace WebApplication1
{
    public class SingletonSaver : ISingletonSaver
    {
        public string[] buttonNamesArray = new string[] { "Sama", "lama", "Duma" };
        public Block[] blocks;

        public SingletonSaver()
        {
            Block bl = new Block();

            
            bl.name = "TestBlock";
            bl.jury = new string[] { "Me", "Me", "Also me" };
            bl.notes = new string[] { "One", "Two", "Three" };
            bl.rows = new ParticipantRow[2];

            bl.rows[0] = new ParticipantRow();
            bl.rows[0].name = "Alex";

            bl.rows[1] = new ParticipantRow();
            bl.rows[1].name = "Fedora";

            blocks = new Block[] { bl };
        }

        public string[] GetBlockNames()
        {
            string[] arr;
            try
            {
                arr = (from b in blocks
                                select b.name).ToArray();
            }
            catch { arr = buttonNamesArray; }
            return arr;
        }

        public Block[] GetBlocks()
        {
            return blocks;
        }

        public void SetBlocks(string[] data)
        {
            blocks = new Block[data.Length];
            //buttonNamesArray = data;

            for (int i = 0; i < data.Length; i++)
            {
                blocks[i] = new Block();
                blocks[i].name = data[i];
                blocks[i].jury = new string[0];
                blocks[i].notes = new string[0];
                blocks[i].rows = new ParticipantRow[0];

            }
        }

        public void SetJury(int blockId, string[] jury)
        {
            blocks[blockId].jury = jury;
        }

        public void SetNotes(int blockId, string[] notes)
        {
            blocks[blockId].notes = notes;
        }

        public void SetParticipants(int blockId, string[] participants)
        {
            blocks[blockId].rows = (from p in participants
                                    select new ParticipantRow(blocks[blockId].jury.Length)
                                    {name = p}).ToArray();

        }

        public void ChangeScore(int blockId, int row,int column, float value)
        {
            blocks[blockId].rows[row].scores[column / 3].score[column % 3] = value;
        }
    }
}
