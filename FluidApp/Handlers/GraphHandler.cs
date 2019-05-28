using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Models;

namespace FluidApp.Handlers
{
    class GraphHandler
    {
        public List<Record> DrawLudKoncentration(int d)
        {
            List<Record> LudKoncnetrationGraf = new List<Record>();
            KontrolSkema k = new KontrolSkema();

            foreach (var x in k.GetAll())
            {
                Record r = new Record();

                if (x.Ludkoncentration != null)
                {
                    r.Max = 2;
                    r.Min = 1;
                    if (d == 0)
                    {
                        r.Name = x.Klokkeslæt;
                        r.Amount = x.Ludkoncentration;
                        LudKoncnetrationGraf.Add(r);
                        continue;
                    }

                    if (x.Klokkeslæt > DateTime.Now.Subtract(TimeSpan.FromDays(d)))
                    {
                        r.Name = x.Klokkeslæt;
                        r.Amount = x.Ludkoncentration;
                        LudKoncnetrationGraf.Add(r);
                    }
                }
            }

            return LudKoncnetrationGraf;
        }

        public List<Record> DrawVægt(int d, string size)
        {
            List<Record> LudKoncnetrationGraf = new List<Record>();
            KontrolSkema k = new KontrolSkema();
            
            Kontrolregistrering kr = new Kontrolregistrering();
            List<Kontrolregistrering> listKr = kr.GetAll();
            List<Kontrolregistrering> confirmed;

            foreach (var x in k.GetAll())
            {
                Record r = new Record();
                confirmed = new List<Kontrolregistrering>(listKr.Where(e => e.FK_Kolonne == x.FK_Kolonne));
                
                if (confirmed.Count > 0 && x.Vægt != null && confirmed[0].Fustage == size)
                {
                    if (d == 0)
                    {
                        r.Name = x.Klokkeslæt;
                        r.Amount = x.Vægt;
                        LudKoncnetrationGraf.Add(r);
                        continue;
                    }

                    if (x.Klokkeslæt > DateTime.Now.Subtract(TimeSpan.FromDays(d)))
                    {
                        r.Name = x.Klokkeslæt;
                        r.Amount = x.Vægt;
                        LudKoncnetrationGraf.Add(r);
                    }
                }
            }

            return LudKoncnetrationGraf;
        }

        public List<Record> DrawMs(int d)
        {
            List<Record> LudKoncnetrationGraf = new List<Record>();
            KontrolSkema k = new KontrolSkema();
            
            foreach (var x in k.GetAll())
            {
                Record r = new Record();

                if (x.MS != null)
                {
                    r.Max = 24;
                    r.Min = 26.5;
                    if (d == 0)
                    {
                        r.Name = x.Klokkeslæt;
                        r.Amount = x.MS;
                        LudKoncnetrationGraf.Add(r);
                        continue;
                    }

                    if (x.Klokkeslæt > DateTime.Now.Subtract(TimeSpan.FromDays(d)))
                    {
                        r.Name = x.Klokkeslæt;
                        r.Amount = x.MS;
                        LudKoncnetrationGraf.Add(r);
                    }
                }
            }

            return LudKoncnetrationGraf;
        }

        public List<Record> DrawPauser(int d)
        {
            List<Record> L = new List<Record>();
            Produktionsfølgeseddel p = new Produktionsfølgeseddel();

            foreach (var x in p.GetAll())
            {
                Record r = new Record();
                r.NameString = x.Start.ToString("dd-MM-yyyy");
                r.Amount = x.Pauser;
                if (d == 0)
                {
                    if (!L.Exists(y => y.NameString == r.NameString))
                    {

                        L.Add(r);
                    }
                    else
                    {
                        var index = L.FindIndex(c => c.NameString == r.NameString);
                        L[index].Amount += x.Pauser;
                    }
                    continue;
                }

                if (x.Start > DateTime.Now.Subtract(TimeSpan.FromDays(d)))
                {
                    if (!L.Exists(y => y.NameString == r.NameString))
                    {

                        L.Add(r);
                    }
                    else
                    {
                        var index = L.FindIndex(c => c.NameString == r.NameString);
                        L[index].Amount += x.Pauser;
                    }
                }
            }
            return L;
        }
    }
}
