namespace Swas.Data.Access.Maps
{
    using Entity;
    using System.Data.Entity.ModelConfiguration;


    public class TransporterMap : EntityTypeConfiguration<Transporter>
    {

        public TransporterMap()
        {
            HasKey(a => a.Id);
            Property(one => one.CarNumber).HasMaxLength(50).IsRequired();
            Property(one => one.CarModel).HasMaxLength(500).IsRequired();
            Property(one => one.DriverInfo).HasMaxLength(500).IsRequired();
        }

    }

}
