namespace Swas.Data.Access.Maps
{
    using Entity;
    using System.Data.Entity.ModelConfiguration;
    using System.ComponentModel.DataAnnotations.Schema;


    public class UserMap : EntityTypeConfiguration<User>
    {

        public UserMap()
        {
            HasKey(a => a.Id);
            Property(one => one.UserName).HasMaxLength(200).IsRequired();
            Property(one => one.Password).HasMaxLength(200).IsRequired();
            Property(one => one.Email).HasMaxLength(200).IsRequired();
            Property(one => one.UseEmailAsUserName).IsRequired();
            Property(one => one.MaxAttamptPassword).IsRequired();
            Property(one => one.IsLocked).IsRequired();
            Property(one => one.IsDisabled).IsRequired();
            Property(one => one.CreateDate).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);


            Property(one => one.FirstName).HasMaxLength(200).IsRequired();
            Property(one => one.LastName).HasMaxLength(200).IsRequired();
            Property(one => one.JobPosition).HasMaxLength(500).IsRequired();
            Property(one => one.ChangePassword).IsRequired();

            HasRequired(a => a.Role)
              .WithMany(a => a.Users)
              .HasForeignKey(a => a.RoleId).WillCascadeOnDelete(false);
        }

    }

}

