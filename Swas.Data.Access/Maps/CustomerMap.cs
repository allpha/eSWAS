namespace Swas.Data.Access.Maps
{
    using Entity;
    using System.Data.Entity.ModelConfiguration;


    public class CustomerMap : EntityTypeConfiguration<Customer>
    {

        public CustomerMap()
        {
            HasKey(a => a.Id);
            Property(one => one.Type).IsRequired();
            Property(one => one.Name).HasMaxLength(250).IsRequired();
            Property(one => one.Code).HasMaxLength(50).IsRequired();
            Property(one => one.ContactInfo).HasMaxLength(4000).IsRequired();
        }

    }

}
