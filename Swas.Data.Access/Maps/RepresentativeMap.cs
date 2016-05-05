namespace Swas.Data.Access.Maps
{
    using Entity;
    using System.Data.Entity.ModelConfiguration;


    public class RepresentativeMap : EntityTypeConfiguration<Representative>
    {

        public RepresentativeMap()
        {
            HasKey(a => a.Id);
            Property(one => one.Name).HasMaxLength(250).IsRequired();
        }

    }

}
