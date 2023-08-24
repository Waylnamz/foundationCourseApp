using Microsoft.EntityFrameworkCore;

namespace efcoreApp.Data
{
    public class DataContext : DbContext
    {
        
        public DataContext(DbContextOptions<DataContext> options):base(options)
        {
        }
        public DbSet<Kurs> Kurslar => Set<Kurs>();

        public DbSet<Ogrenci> Ogrenciler => Set<Ogrenci>();
        public DbSet<KursKayit> KursKayitlari => Set<KursKayit>();
        public DbSet<Ogretmen> Ogretmenler => Set<Ogretmen>();

        internal void Update(int id, Ogrenci model)
        {
            throw new NotImplementedException();
        }
    }

    //code-first => entity, dbconext => database (postgresql)
    //database-first => sql server
}