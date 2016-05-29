namespace Swas.Data.Access.Maps
{
    using Entity;
    using System.Data.Entity.ModelConfiguration;


    public class RolePermissionMap : EntityTypeConfiguration<RolePermission>
    {

        public RolePermissionMap()
        {
            HasKey(a => a.Id);

            HasRequired(a => a.Role)
              .WithMany(a => a.RolePermissions)
              .HasForeignKey(a => a.RoleId).WillCascadeOnDelete(false);

            HasRequired(a => a.Permission)
              .WithMany(a => a.RolePermissions)
              .HasForeignKey(a => a.PermissionId).WillCascadeOnDelete(false);
        }
    }

}
