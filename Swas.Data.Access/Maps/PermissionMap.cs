namespace Swas.Data.Access.Maps
{
    using Entity;
    using System.Data.Entity.ModelConfiguration;


    public class PermissionMap : EntityTypeConfiguration<Permission>
    {

        public PermissionMap()
        {
            HasKey(a => a.Id);
            Property(one => one.Description).HasMaxLength(250).IsRequired();
            Property(one => one.Name).HasMaxLength(250).IsRequired();
        }

    }

}
