namespace Swas.Data.Access.Maps
{
    using Entity;
    using System.Data.Entity.ModelConfiguration;

    public class WasteTypeMap : EntityTypeConfiguration<WasteType>
    {

        public WasteTypeMap()
        {
            HasKey(a => a.Id);
            Property(x => x.LessQuantity).HasPrecision(16, 4);
            Property(x => x.FromQuantity).HasPrecision(16, 3);
            Property(x => x.EndQuantity).HasPrecision(16, 3);
            Property(x => x.MoreQuantity).HasPrecision(16, 3);

            Property(x => x.MunicipalityLessQuantityPrice).HasPrecision(16, 3);
            Property(x => x.MunicipalityIntervalQuantityPrice).HasPrecision(16, 3);
            Property(x => x.MunicipalityMoreQuantityPrice).HasPrecision(16, 4);

            Property(x => x.LegalPersonLessQuantityPrice).HasPrecision(16, 3);
            Property(x => x.LegalPersonIntervalQuantityPrice).HasPrecision(16, 3);
            Property(x => x.LegalPersonMoreQuantityPrice).HasPrecision(16, 4);

            Property(x => x.PhysicalPersonLessQuantityPrice).HasPrecision(16, 3);
            Property(x => x.PhysicalPersonIntervalQuantityPrice).HasPrecision(16, 3);
            Property(x => x.PhysicalPersonMoreQuantityPrice).HasPrecision(16, 4);

            Property(x => x.Coeficient).HasPrecision(16, 4);

        }

    }

}
