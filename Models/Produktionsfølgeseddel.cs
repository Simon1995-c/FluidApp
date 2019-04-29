using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    class Produktionsfølgeseddel
    {
        public int ID { get; set; }
        public int FK_Kolonne { get; set; }
        public DateTime Slut { get; set; }
        public DateTime Start { get; set; }
        public int Bemanding { get; set; }
        public float Timer { get; set; }
        public string Signatur{ get; set; }
        public int Pause { get; set; }
    }
}
