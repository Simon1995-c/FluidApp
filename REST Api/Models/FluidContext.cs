using System.Data.Entity;

namespace REST_Api.Models
{
    public partial class FluidContext : DbContext
    {
        public FluidContext()
            : base("name=FluidContext")
        {
            base.Configuration.ProxyCreationEnabled = false;
        }

        public virtual DbSet<Administrator> Administrator { get; set; }
        public virtual DbSet<Forside> Forside { get; set; }
        public virtual DbSet<Færdigvarekontrol> Færdigvarekontrol { get; set; }
        public virtual DbSet<IPrange> IPrange { get; set; }
        public virtual DbSet<Kolonne2> Kolonne2 { get; set; }
        public virtual DbSet<Kolonner> Kolonner { get; set; }
        public virtual DbSet<KontrolRegistrering> KontrolRegistrering { get; set; }
        public virtual DbSet<KontrolSkema> KontrolSkema { get; set; }
        public virtual DbSet<Produktionsfølgeseddel> Produktionsfølgeseddel { get; set; }
        public virtual DbSet<RengøringsKolonne> RengøringsKolonne { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Administrator>()
                .Property(e => e.Brugernavn)
                .IsUnicode(false);

            modelBuilder.Entity<Administrator>()
                .Property(e => e.Kodeord)
                .IsUnicode(false);

            modelBuilder.Entity<Forside>()
                .Property(e => e.FærdigvareNavn)
                .IsUnicode(false);

            modelBuilder.Entity<Forside>()
                .Property(e => e.Produktionsinitialer)
                .IsUnicode(false);

            modelBuilder.Entity<Færdigvarekontrol>()
                .Property(e => e.Initialer)
                .IsUnicode(false);

            modelBuilder.Entity<Færdigvarekontrol>()
                .Property(e => e.LågFarve)
                .IsUnicode(false);

            modelBuilder.Entity<Færdigvarekontrol>()
                .Property(e => e.RingFarve)
                .IsUnicode(false);

            modelBuilder.Entity<Færdigvarekontrol>()
                .Property(e => e.Parametre)
                .IsUnicode(false);

            modelBuilder.Entity<KontrolRegistrering>()
                .Property(e => e.Kommentar)
                .IsUnicode(false);

            modelBuilder.Entity<KontrolRegistrering>()
                .Property(e => e.Fustage)
                .IsUnicode(false);

            modelBuilder.Entity<KontrolRegistrering>()
                .Property(e => e.Signatur)
                .IsUnicode(false);

            modelBuilder.Entity<KontrolSkema>()
                .Property(e => e.Signatur)
                .IsUnicode(false);

            modelBuilder.Entity<Produktionsfølgeseddel>()
                .Property(e => e.Signatur)
                .IsUnicode(false);

            modelBuilder.Entity<RengøringsKolonne>()
                .Property(e => e.Kommentar)
                .IsUnicode(false);

            modelBuilder.Entity<RengøringsKolonne>()
                .Property(e => e.Opgave)
                .IsUnicode(false);

            modelBuilder.Entity<RengøringsKolonne>()
                .Property(e => e.Udstyr)
                .IsUnicode(false);

            modelBuilder.Entity<RengøringsKolonne>()
                .Property(e => e.VejledningsNr)
                .IsUnicode(false);

            modelBuilder.Entity<RengøringsKolonne>()
                .Property(e => e.Udførsel)
                .IsUnicode(false);

            modelBuilder.Entity<RengøringsKolonne>()
                .Property(e => e.Signatur)
                .IsUnicode(false);
        }
    }
}
