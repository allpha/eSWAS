namespace Swas.Data.Access.Maps
{
    using Entity;
    using System.Data.Entity.ModelConfiguration;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.ComponentModel.DataAnnotations.Schema;


    public class ReceiverPositionMap : EntityTypeConfiguration<ReceiverPosition>
    {
        public ReceiverPositionMap()
        {
            HasKey(a => a.Id);


            HasRequired(a => a.Receiver)
              .WithMany(a => a.ReceiverPositions)
              .HasForeignKey(a => a.ReceiverId).WillCascadeOnDelete(false);

            HasRequired(a => a.Position)
              .WithMany(a => a.ReceiverPositions)
              .HasForeignKey(a => a.PositionId).WillCascadeOnDelete(false);

        }

    }

}
