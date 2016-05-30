namespace Swas.Data.Access.Maps
{
    using Entity;
    using System.Data.Entity.ModelConfiguration;


    public class UserDetailMap : EntityTypeConfiguration<UserDetail>
    {

        public UserDetailMap()
        {
            HasKey(a => a.Id);

            Property(one => one.FirstName).HasMaxLength(200).IsRequired();
            Property(one => one.LastName).HasMaxLength(200).IsRequired();
            Property(one => one.PrivateNumber).HasMaxLength(100);
            Property(one => one.JobPosition).HasMaxLength(500);
            Property(one => one.CreateDate).IsRequired();

            HasRequired(a => a.User)
              .WithMany(a => a.UserDetails)
              .HasForeignKey(a => a.UserId).WillCascadeOnDelete(false);
        }

    }

}

