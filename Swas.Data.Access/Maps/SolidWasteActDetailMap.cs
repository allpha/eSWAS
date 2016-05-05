namespace Swas.Data.Access.Maps
{
    using Entity;
    using System.Data.Entity.ModelConfiguration;


    public class SolidWasteActDetailMap : EntityTypeConfiguration<SolidWasteActDetail>
    {

        public SolidWasteActDetailMap()
        {
            HasKey(a => a.Id);

            HasRequired(a => a.SolidWasteAct)
              .WithMany(a => a.SolidWasteActDetails)
              .HasForeignKey(a => a.SolidWasteActId).WillCascadeOnDelete(false);


            HasRequired(a => a.WasteType)
              .WithMany(a => a.SolidWasteActDetails)
              .HasForeignKey(a => a.WasteTypeId).WillCascadeOnDelete(false);
        }

    }

}
