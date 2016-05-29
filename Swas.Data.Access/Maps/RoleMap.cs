namespace Swas.Data.Access.Maps
{
    using Entity;
    using System.Data.Entity.ModelConfiguration;


    public class RoleMap : EntityTypeConfiguration<Role>
    {

        public RoleMap()
        {
            HasKey(a => a.Id);
            Property(one => one.Description).HasMaxLength(250).IsRequired();
        }

    }

}
