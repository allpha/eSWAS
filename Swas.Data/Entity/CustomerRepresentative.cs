namespace Swas.Data.Entity
{
    using System.ComponentModel.DataAnnotations.Schema;

    public class CustomerRepresentative
    {
        public int Id { get; set; }
        [Index("IX_CustomerRepresentative_CustomerId_RepresentativeId_TransporterId", 1)]
        public int CustomerId { get; set; }
        [Index("IX_CustomerRepresentative_CustomerId_RepresentativeId_TransporterId", 2)]
        public int RepresentativeId { get; set; }
        [Index("IX_CustomerRepresentative_CustomerId_RepresentativeId_TransporterId", 3)]
        public int TransporterId { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Representative Representative { get; set; }
        public virtual Transporter Transporter { get; set; }
    }
}
