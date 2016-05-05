namespace Swas.Data.Access.Maps
{
    using Entity;
    using System.Data.Entity.ModelConfiguration;

    public class ReceiverMap : EntityTypeConfiguration<Receiver>
    {

        public ReceiverMap()
        {
            HasKey(a => a.Id);
            Property(a => a.Name).HasMaxLength(500).IsRequired();
        }

    }

}
