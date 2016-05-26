namespace Swas.Data.Access.Maps
{
    using Entity;
    using System.Data.Entity.ModelConfiguration;


    public class PaymentMap : EntityTypeConfiguration<Payment>
    {

        public PaymentMap()
        {
            HasKey(a => a.Id);
            Property(one => one.PayDate).IsRequired();
            Property(one => one.Amount).IsRequired();

            HasRequired(a => a.SolidWasteAct)
              .WithMany(a => a.Payments)
              .HasForeignKey(a => a.SolidWasteActId).WillCascadeOnDelete(false);
        }

    }

}
