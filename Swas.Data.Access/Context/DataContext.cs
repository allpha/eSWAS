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
        public virtual DbSet<Transporter> Transporters { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Position> Positions { get; set; }
        public virtual DbSet<Receiver> Receivers { get; set; }
        public virtual DbSet<Representative> Representatives { get; set; }
        public virtual DbSet<WasteType> WasteTypes { get; set; }
        public virtual DbSet<CustomerRepresentative> CustomerRepresentatives { get; set; }
        public virtual DbSet<ReceiverPosition> ReceiverPositions { get; set; }
        public virtual DbSet<SolidWasteAct> SolidWasteActs { get; set; }
        public virtual DbSet<SolidWasteActDetail> SolidWasteActDetails { get; set; }
        public virtual DbSet<Agreement> Agreements { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<Permission> Permissions { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<RolePermission> RolePermissions { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserRegion> UserRegions { get; set; }
        public virtual DbSet<SolidWasteActHistory> SolidWasteActHistories { get; set; }



        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.Add(new RegionMap());
            modelBuilder.Configurations.Add(new LandfillMap());
            modelBuilder.Configurations.Add(new WasteTypeMap());

            modelBuilder.Configurations.Add(new TransporterMap());
            modelBuilder.Configurations.Add(new CustomerMap());
            modelBuilder.Configurations.Add(new ReceiverMap());
            modelBuilder.Configurations.Add(new PositionMap());
            modelBuilder.Configurations.Add(new RepresentativeMap());
            modelBuilder.Configurations.Add(new CustomerRepresentativeMap());
            modelBuilder.Configurations.Add(new ReceiverPositionMap());
            modelBuilder.Configurations.Add(new SolidWasteActMap());
            modelBuilder.Configurations.Add(new SolidWasteActDetailMap());
            modelBuilder.Configurations.Add(new AgreementMap());
            modelBuilder.Configurations.Add(new PaymentMap());
            modelBuilder.Configurations.Add(new PermissionMap());
            modelBuilder.Configurations.Add(new RoleMap());
            modelBuilder.Configurations.Add(new RolePermissionMap());
            modelBuilder.Configurations.Add(new UserMap());
            modelBuilder.Configurations.Add(new UserRegionMap());
            modelBuilder.Configurations.Add(new SolidWasteActHistoryMap());
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



