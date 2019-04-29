using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    class Forside
    {
        public int ID { get; set; }
        public int FK_Kolonne { get; set; }
        public int FærdigvareNr { get; set; }
        public string Færdigvarenavn { get; set; }
        public int Processordrenummer { get; set; }
        public string Produktionsinitialer { get; set; }
        public DateTime dato { get; set; }
    }
}