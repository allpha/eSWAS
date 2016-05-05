namespace Swas.Data.Access.Maps
{
    using Entity;
    using System.Data.Entity.ModelConfiguration;

    public class SolidWasteActMap : EntityTypeConfiguration<SolidWasteAct>
    {

        public SolidWasteActMap()
        {
            HasKey(a => a.Id);
            Property(one => one.Remark).HasMaxLength(4000).IsRequired();

            HasRequired(a => a.Landfill)
              .WithMany(a => a.SolidWasteActs)
              .HasForeignKey(a => a.LandfillId).WillCascadeOnDelete(false);

            HasRequired(a => a.Receiver)
              .WithMany(a => a.SolidWasteActs)
              .HasForeignKey(a => a.ReceiverId).WillCascadeOnDelete(false);

            HasRequired(a => a.Position)
              .WithMany(a => a.SolidWasteActs)
              .HasForeignKey(a => a.PositionId).WillCascadeOnDelete(false);

            HasRequired(a => a.Customer)
              .WithMany(a => a.SolidWasteActs)
              .HasForeignKey(a => a.CustomerId).WillCascadeOnDelete(false);

            HasRequired(a => a.Transporter)
              .WithMany(a => a.SolidWasteActs)
              .HasForeignKey(a => a.TransporterId).WillCascadeOnDelete(false);

            HasRequired(a => a.Representative)
              .WithMany(a => a.SolidWasteActs)
              .HasForeignKey(a => a.RepresentativeId).WillCascadeOnDelete(false);
        }

    }

}
