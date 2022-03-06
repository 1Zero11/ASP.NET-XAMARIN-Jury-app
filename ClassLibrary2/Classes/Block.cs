using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClassLibrary2.Classes
{
    [Serializable]
    public class Block
    {

        public Block()
        {

        }

        public Block(int numberOfJudjes, int numberOfRows)
        {
            rows = new ParticipantRow[numberOfRows];
            for (int i = 0; i < numberOfRows; i++)
                rows[i] = new ParticipantRow(numberOfJudjes);
        }

        public string name { get; set; }
        public string[] jury { get; set; }
        public string[] notes { get; set; }
        public ParticipantRow[] rows { get; set; }
    }
}
