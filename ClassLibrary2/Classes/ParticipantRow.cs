using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClassLibrary2.Classes
{
    [Serializable]
    public class ParticipantRow
    {
        public ParticipantRow()
        {

        }

        public ParticipantRow(int numberOfJudjes)
        {
            scores = new JuryScore[numberOfJudjes];
            for (int i = 0; i < numberOfJudjes; i++)
                scores[i] = new JuryScore { score = new float[] { 0, 0, 0 } };
        }

        public int id { get; set; }
        public string name { get; set; }
        public JuryScore[] scores { get; set; }
        public float finalScore { get {
                int divider = 0;
                float sum = 0;
                foreach(JuryScore score in scores)
                    foreach(float i in score.score)
                        if (i != 0)
                        {
                            divider++;
                            sum += i;
                        }

                if (divider != 0)
                    return sum / divider;
                else
                    return 0;
            }}
        public string prize { get; set; }
    }
}
