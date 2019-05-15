using System;
using System.Collections.Generic;
using Models;

namespace FluidApp.Handlers
{
    class GraphHandler
    {
        public List<Record> DrawLudKoncentration(int d)
        {
            List<Record> LudKoncnetrationGraf = new List<Record>();
            KontrolSkema k = new KontrolSkema();
            
            int i = 0;

            foreach (var x in k.GetAll())
            {
                Record r = new Record();

                if (d == 0)
                {
                    r.Name = i++.ToString();
                    r.Amount = x.Ludkoncentration;
                    LudKoncnetrationGraf.Add(r);
                    continue;
                }
                if (x.Klokkeslæt > DateTime.Now.Subtract(TimeSpan.FromDays(d)))
                {
                    r.Name = i++.ToString();
                    r.Amount = x.Ludkoncentration;
                    LudKoncnetrationGraf.Add(r);
                }
            }

            return LudKoncnetrationGraf;
        }

        public List<Record> DrawVægt(int d)
        {
            List<Record> LudKoncnetrationGraf = new List<Record>();
            KontrolSkema k = new KontrolSkema();

            int i = 0;

            foreach (var x in k.GetAll())
            {
                Record r = new Record();

                if (d == 0)
                {
                    r.Name = i++.ToString();
                    r.Amount = x.Vægt;
                    LudKoncnetrationGraf.Add(r);
                    continue;
                }
                if (x.Klokkeslæt > DateTime.Now.Subtract(TimeSpan.FromDays(d)))
                {
                    r.Name = i++.ToString();
                    r.Amount = x.Vægt;
                    LudKoncnetrationGraf.Add(r);
                }
            }

            return LudKoncnetrationGraf;
        }

        public List<Record> DrawMs(int d)
        {
            List<Record> LudKoncnetrationGraf = new List<Record>();
            KontrolSkema k = new KontrolSkema();

            int i = 0;

            foreach (var x in k.GetAll())
            {
                Record r = new Record();

                if (d == 0)
                {
                    r.Name = i++.ToString();
                    r.Amount = x.MS;
                    LudKoncnetrationGraf.Add(r);
                    continue;
                }
                if (x.Klokkeslæt > DateTime.Now.Subtract(TimeSpan.FromDays(d)))
                {
                    r.Name = i++.ToString();
                    r.Amount = x.MS;
                    LudKoncnetrationGraf.Add(r);
                }
            }

            return LudKoncnetrationGraf;
        }
    }
}
