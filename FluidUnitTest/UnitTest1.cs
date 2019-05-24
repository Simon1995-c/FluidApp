using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;

namespace FluidUnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestGetOne()
        {
            Administrator a = new Administrator();
            Assert.AreEqual(23, a.GetOne(23).ID);

            Færdigvarekontrol f = new Færdigvarekontrol();
            Assert.AreEqual(6, f.GetOne(6).ProcessordreNr);

            Forside fo = new Forside();
            Assert.AreEqual("EGEKILDE 0.5 L", fo.GetOne(12).FærdigvareNavn);

            IPrange ip = new IPrange();
            Assert.AreEqual(1, ip.GetOne(1).ID);

            Kolonne2 k2 = new Kolonne2();
            Assert.AreEqual(8, k2.GetOne(8).ID);

            Kontrolregistrering kr = new Kontrolregistrering();
            Assert.AreEqual(33, kr.GetOne(33).ID);

            KontrolSkema ks = new KontrolSkema();
            Assert.AreEqual(48, ks.GetOne(48).ID);

            Produktionsfølgeseddel pf = new Produktionsfølgeseddel();
            Assert.AreEqual(9, pf.GetOne(9).ID);

            RengøringsKolonne rk = new RengøringsKolonne();
            Assert.AreEqual(1, rk.GetOne(1).ID);
        }

        [TestMethod]
        public void TestAdministrator()
        {
            //Get all & Post
            Administrator a = new Administrator();

            List<Administrator> alladmins = a.GetAll();

            int orgNumOfAdmins = alladmins.Count;

            a.Post(new Administrator() { Brugernavn = "TestFraUnit", Kodeord = "KodeFraUnit", Rolle = 1 });

            List<Administrator> alladmindsPlusOne = a.GetAll();

            int newNumOfAdmins = alladmindsPlusOne.Count;

            Assert.AreEqual(newNumOfAdmins, orgNumOfAdmins + 1);


            //Update
            Administrator lastItem = alladmindsPlusOne[alladmindsPlusOne.Count - 1];

            a.Put(lastItem.ID, new Administrator() { ID = lastItem.ID, Brugernavn = "Opdateret", Kodeord = "KodeFraUnit", Rolle = 1 });

            List<Administrator> allAdminsAfterEdit = a.GetAll();

            Assert.AreEqual(allAdminsAfterEdit[allAdminsAfterEdit.Count - 1].Brugernavn, "Opdateret");


            //Delete
            a.Delete(lastItem.ID);

            int newNumOfAdminsAfterDelete = a.GetAll().Count;

            Assert.AreEqual(newNumOfAdminsAfterDelete, orgNumOfAdmins);
        }

        [TestMethod]
        public void TestFærdigvarekontrol()
        {
            //Get all & Post
            Færdigvarekontrol a = new Færdigvarekontrol();

            List<Færdigvarekontrol> all = a.GetAll();

            int orgNum = all.Count;

            a.Post(new Færdigvarekontrol() { FK_Kolonne = 8, DåseNr = 1, Initialer = "Test", LågNr = 1, DatoMærkning = DateTime.Now, LågFarve = "Rød", RingFarve = "Grøn", Enheder = 1, Parametre = "Test", Multipack = 1, FolieNr = 1, KartonNr = 1, PalleNr = 1 });

            List<Færdigvarekontrol> allPlusOne = a.GetAll();

            int newNum = allPlusOne.Count;

            Assert.AreEqual(newNum, orgNum + 1);


            //Update
            Færdigvarekontrol lastItem = allPlusOne[allPlusOne.Count - 1];

            a.Put(lastItem.ProcessordreNr, new Færdigvarekontrol() { ProcessordreNr = lastItem.ProcessordreNr, FK_Kolonne = 8, DåseNr = 1, Initialer = "Opdateret", LågNr = 1, DatoMærkning = DateTime.Now, LågFarve = "Rød", RingFarve = "Grøn", Enheder = 1, Parametre = "Test", Multipack = 1, FolieNr = 1, KartonNr = 1, PalleNr = 1 });

            List<Færdigvarekontrol> allAFterEdit = a.GetAll();

            Assert.AreEqual(allAFterEdit[allAFterEdit.Count - 1].Initialer, "Opdateret");


            //Delete
            a.Delete(lastItem.ProcessordreNr);

            int newNumOfAdminsAfterDelete = a.GetAll().Count;

            Assert.AreEqual(newNumOfAdminsAfterDelete, orgNum);
        }

        [TestMethod]
        public void TestForside()
        {
            //Get all & Post
            Forside a = new Forside();

            List<Forside> all = a.GetAll().Result;

            int orgNum = all.Count;

            a.Post(new Forside() { FK_Kolonne = 8, FærdigvareNr = 1, FærdigvareNavn = "test", ProcessordreNr = 1, Produktionsinitialer = "Test", Dato = DateTime.Now });

            List<Forside> allPlusOne = a.GetAll().Result;

            int newNum = allPlusOne.Count;

            Assert.AreEqual(newNum, orgNum + 1);


            //Update
            Forside lastItem = allPlusOne[allPlusOne.Count - 1];

            a.Put(lastItem.ID, new Forside() { ID = lastItem.ID, FK_Kolonne = 8, FærdigvareNr = 1, FærdigvareNavn = "Opdateret", ProcessordreNr = 1, Produktionsinitialer = "Test", Dato = DateTime.Now });

            List<Forside> allAFterEdit = a.GetAll().Result;

            Assert.AreEqual(allAFterEdit[allAFterEdit.Count - 1].FærdigvareNavn, "Opdateret");


            //Delete
            a.Delete(lastItem.ID);

            int newNumOfAdminsAfterDelete = a.GetAll().Result.Count;

            Assert.AreEqual(newNumOfAdminsAfterDelete, orgNum);
        }

        [TestMethod]
        public void TestIPrange()
        {
            //Get all & Post
            IPrange a = new IPrange();

            List<IPrange> all = a.GetAll();

            int orgNum = all.Count;

            a.Post(new IPrange() { IP = "Test ip" });

            List<IPrange> allPlusOne = a.GetAll();

            int newNum = allPlusOne.Count;

            Assert.AreEqual(newNum, orgNum + 1);


            //Update
            IPrange lastItem = allPlusOne[allPlusOne.Count - 1];

            a.Put(lastItem.ID, new IPrange() { ID = lastItem.ID, IP = "Opdateret" });

            List<IPrange> allAFterEdit = a.GetAll();

            Assert.AreEqual(allAFterEdit[allAFterEdit.Count - 1].IP, "Opdateret");


            //Delete
            a.Delete(lastItem.ID);

            int newNumOfAdminsAfterDelete = a.GetAll().Count;

            Assert.AreEqual(newNumOfAdminsAfterDelete, orgNum);
        }

        [TestMethod]
        public void TestKontrolRegistering()
        {
            //Get all & Post
            Kontrolregistrering a = new Kontrolregistrering();

            List<Kontrolregistrering> all = a.GetAll();

            int orgNum = all.Count;

            a.Post(new Kontrolregistrering() { FK_Kolonne = 8, Klokkeslæt = DateTime.Now, Holdbarhedsdato = DateTime.Now, Produktionsdato = DateTime.Now, FærdigvareNr = 1, Kommentar = "Test", Spritkontrol = true, HætteNr = 1, EtiketNr = 1, Fustage = "Test", Signatur = "Test" });

            List<Kontrolregistrering> allPlusOne = a.GetAll();

            int newNum = allPlusOne.Count;

            Assert.AreEqual(newNum, orgNum + 1);


            //Update
            Kontrolregistrering lastItem = allPlusOne[allPlusOne.Count - 1];

            a.Put(lastItem.ID, new Kontrolregistrering() { ID = lastItem.ID, FK_Kolonne = 8, Klokkeslæt = DateTime.Now, Holdbarhedsdato = DateTime.Now, Produktionsdato = DateTime.Now, FærdigvareNr = 1, Kommentar = "Opdateret", Spritkontrol = true, HætteNr = 1, EtiketNr = 1, Fustage = "Test", Signatur = "Test" });

            List<Kontrolregistrering> allAFterEdit = a.GetAll();

            Assert.AreEqual(allAFterEdit[allAFterEdit.Count - 1].Kommentar, "Opdateret");


            //Delete
            a.Delete(lastItem.ID);

            int newNumOfAdminsAfterDelete = a.GetAll().Count;

            Assert.AreEqual(newNumOfAdminsAfterDelete, orgNum);
        }

        [TestMethod]
        public void TestKontrolSkema()
        {
            //Get all & Post
            KontrolSkema a = new KontrolSkema();

            List<KontrolSkema> all = a.GetAll();

            int orgNum = all.Count;
            KontrolSkema test = new KontrolSkema()
            {
                FK_Kolonne = 8,
                Klokkeslæt = DateTime.Now,
                Ludkoncentration = 1.5,
                Fustage = "Test",
                Kvittering = 1,
                MS = 1.5,
                LudKontrol = true,
                Signatur = "Test",
                MSKontrol = true,
                Vægt = 5.0
            };

            a.Post(test);

            List<KontrolSkema> allPlusOne = a.GetAll();

            int newNum = allPlusOne.Count;

            Assert.AreEqual(newNum, orgNum + 1);
            

            //Update
            KontrolSkema lastItem = allPlusOne[allPlusOne.Count - 1];

            a.Put(lastItem.ID, new KontrolSkema()
            {
                ID = lastItem.ID,
                FK_Kolonne = 8,
                Klokkeslæt = DateTime.Now,
                Ludkoncentration = 1.5,
                Fustage = "Opdateret",
                Kvittering = 1,
                MS = 1.5,
                LudKontrol = true,
                Signatur = "Test",
                MSKontrol = true,
                Vægt = 5.0
            });

            List<KontrolSkema> allAFterEdit = a.GetAll();

            Assert.AreEqual(allAFterEdit[allAFterEdit.Count - 1].Fustage, "Opdateret");


            //Delete
            a.Delete(lastItem.ID);

            int newNumOfAdminsAfterDelete = a.GetAll().Count;

            Assert.AreEqual(newNumOfAdminsAfterDelete, orgNum);
        }

        [TestMethod]
        public void TestProduktionsfølgeseddel()
        {
            //Get all & Post
            Produktionsfølgeseddel a = new Produktionsfølgeseddel();

            List<Produktionsfølgeseddel> all = a.GetAll();

            int orgNum = all.Count;

            Produktionsfølgeseddel test = new Produktionsfølgeseddel()
            {
                FK_Kolonne = 8,
                Slut = DateTime.Now,
                Start = DateTime.Now,
                Bemanding = 5,
                Timer = 5,
                Signatur = "Test",
                Pauser = 5
            };

            a.Post(test);

            List<Produktionsfølgeseddel> allPlusOne = a.GetAll();

            int newNum = allPlusOne.Count;

            Assert.AreEqual(newNum, orgNum + 1);


            //Update
            Produktionsfølgeseddel lastItem = allPlusOne[allPlusOne.Count - 1];

            a.Put(lastItem.ID, new Produktionsfølgeseddel()
            {
                ID = lastItem.ID,
                FK_Kolonne = 8,
                Slut = DateTime.Now,
                Start = DateTime.Now,
                Bemanding = 5,
                Timer = 5,
                Signatur = "Opdateret",
                Pauser = 5
            });

            List<Produktionsfølgeseddel> allAFterEdit = a.GetAll();

            Assert.AreEqual(allAFterEdit[allAFterEdit.Count - 1].Signatur, "Opdateret");


            //Delete
            a.Delete(lastItem.ID);

            int newNumOfAdminsAfterDelete = a.GetAll().Count;

            Assert.AreEqual(newNumOfAdminsAfterDelete, orgNum);
        }

        [TestMethod]
        public void TestRengøringsKolonne()
        {
            //Get all & Post
            RengøringsKolonne a = new RengøringsKolonne();

            List<RengøringsKolonne> all = a.GetAll();

            int orgNum = all.Count;

            a.Post(new RengøringsKolonne()
            {
                KolonneNr = 1,
                UgeNr = 1,
                Kommentar = "tst",
                Opgave = "test",
                Udstyr = "test",
                VejledningsNr = "test",
                Frekvens = 1,
                SidstDato = DateTime.Now,
                IgenDato = DateTime.Now,
                Udførsel = "Test",
                Signatur = "Min signatur"
            });

            List<RengøringsKolonne> allPlusOne = a.GetAll();

            int newNum = allPlusOne.Count;

            Assert.AreEqual(newNum, orgNum + 1);


            //Update
            RengøringsKolonne lastItem = allPlusOne[allPlusOne.Count - 1];

            a.Put(lastItem.ID, new RengøringsKolonne()
            {
                ID = lastItem.ID,
                KolonneNr = 1,
                UgeNr = 1,
                Kommentar = "Opdateret",
                Opgave = "test",
                Udstyr = "test",
                VejledningsNr = "test",
                Frekvens = 1,
                SidstDato = DateTime.Now,
                IgenDato = DateTime.Now,
                Udførsel = "Test",
                Signatur = "Min signatur"
            });

            List<RengøringsKolonne> allAFterEdit = a.GetAll();

            Assert.AreEqual(allAFterEdit[allAFterEdit.Count - 1].Kommentar, "Opdateret");


            //Delete
            a.Delete(lastItem.ID);

            int newNumOfAdminsAfterDelete = a.GetAll().Count;

            Assert.AreEqual(newNumOfAdminsAfterDelete, orgNum);
        }
    }
}
