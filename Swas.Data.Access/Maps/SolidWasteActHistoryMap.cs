namespace Swas.Data.Access.Maps
{
    using Entity;
    using System.Data.Entity.ModelConfiguration;
    using System.ComponentModel.DataAnnotations.Schema;

    public class SolidWasteActHistoryMap : EntityTypeConfiguration<SolidWasteActHistory>
    {

        public SolidWasteActHistoryMap()
        {
            HasKey(a => a.Id);
            Property(one => one.Content).IsRequired();
          //  Property(one => one.CreateDate).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);


            HasRequired(a => a.SolidWasteAct)
              .WithMany(a => a.SolidWasteActHistories)
              .HasForeignKey(a => a.SolidWasteActId).WillCascadeOnDelete(false);
        }

    }

}
