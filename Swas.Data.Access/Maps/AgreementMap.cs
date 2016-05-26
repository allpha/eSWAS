namespace Swas.Data.Access.Maps
{
    using Entity;
    using System.Data.Entity.ModelConfiguration;


    public class AgreementMap : EntityTypeConfiguration<Agreement>
    {

        public AgreementMap()
        {
            HasKey(a => a.Id);
            Property(one => one.Code).HasMaxLength(250).IsRequired();
            Property(one => one.StartDate).IsRequired();


            HasRequired(a => a.Customer)
              .WithMany(a => a.Agreements)
              .HasForeignKey(a => a.CustomerId).WillCascadeOnDelete(false);
        }

    }

}
