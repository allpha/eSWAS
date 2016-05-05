namespace Swas.Data.Access.Maps
{
    using Entity;
    using System.Data.Entity.ModelConfiguration;


    public class CustomerRepresentativeMap : EntityTypeConfiguration<CustomerRepresentative>
    {

        public CustomerRepresentativeMap()
        {
            HasKey(a => a.Id);

            HasRequired(a => a.Customer)
              .WithMany(a => a.CustomerRepresentative)
              .HasForeignKey(a => a.CustomerId).WillCascadeOnDelete(false);

            HasRequired(a => a.Representative)
              .WithMany(a => a.CustomerRepresentatives)
              .HasForeignKey(a => a.RepresentativeId).WillCascadeOnDelete(false);

            HasRequired(a => a.Transporter)
              .WithMany(a => a.CustomerRepresentatives)
              .HasForeignKey(a => a.TransporterId).WillCascadeOnDelete(false);

        }

    }

}
