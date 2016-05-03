namespace Swas.Data.Access.Context
{
    using Entity;
    using Maps;
    using System.Data.Entity;

    public class DataContext : DbContext
    {
        public DataContext()
            : base("Name=eSWASContext")
        {
            base.Database.CommandTimeout = 360;
            base.Configuration.LazyLoadingEnabled = true;
            base.Configuration.UseDatabaseNullSemantics = true;
        }

        public virtual DbSet<Region> Regions { get; set; }
        public virtual DbSet<Landfill> Landfills { get; set; }
        public virtual DbSet<WasteType> WasteTypes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.Add(new RegionMap());
            modelBuilder.Configurations.Add(new LandfillMap());
            modelBuilder.Configurations.Add(new WasteTypeMap());
        }

        public void OpenConection()
        {
            if (Database == null)
                throw new System.Exception("Datebase Is Not Created");

            if (Database.Connection.State != System.Data.ConnectionState.Open)
            {
                Database.Connection.Open();
            }
        }

        public void CloseConnection()
        {
            if (Database == null) return;

            if (Database.Connection.State != System.Data.ConnectionState.Closed)
                Database.Connection.Close();
        }

    }
}
