namespace Swas.Data.Access.Maps
{
    using Entity;
    using System.Data.Entity.ModelConfiguration;


    public class UserRegionMap : EntityTypeConfiguration<UserRegion>
    {

        public UserRegionMap()
        {
            HasKey(a => a.Id);

            HasRequired(a => a.User)
              .WithMany(a => a.UserRegions)
              .HasForeignKey(a => a.UserId).WillCascadeOnDelete(false);

            HasRequired(a => a.Region)
              .WithMany(a => a.UserRegions)
              .HasForeignKey(a => a.RegionId).WillCascadeOnDelete(false);
        }

    }

}

