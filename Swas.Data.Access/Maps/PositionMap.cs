namespace Swas.Data.Access.Maps
{
    using Entity;
    using System.Data.Entity.ModelConfiguration;

    public class PositionMap : EntityTypeConfiguration<Position>
    {

        public PositionMap()
        {
            HasKey(a => a.Id);
            Property(a => a.Name).HasMaxLength(500).IsRequired();
        }

    }

}