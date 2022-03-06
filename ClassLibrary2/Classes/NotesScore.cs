using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClassLibrary2.Classes
{
    [Serializable]
    public class NotesScore
    {
        public string name { get; set; }
        public int[] score { get; set; }
    }
}
