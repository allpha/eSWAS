namespace Swas.Data.Access.Maps
{
    using Entity;
    using System.Data.Entity.ModelConfiguration;

    public class LandfillMap : EntityTypeConfiguration<Landfill>
    {

        public LandfillMap()
        {
            HasKey(a => a.Id);
            Property(one => one.Name).HasMaxLength(30).IsRequired();
            HasRequired(a => a.Region)
              .WithMany(a => a.Landfills)
              .HasForeignKey(a => a.RegionId).WillCascadeOnDelete(false);

        }

    }

}
