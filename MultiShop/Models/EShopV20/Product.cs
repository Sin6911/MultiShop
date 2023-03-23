namespace MultiShop.Models.EShopV20
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(40)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string UnitBrief { get; set; }

        public double UnitPrice { get; set; }

        [Required]
        [StringLength(50)]
        public string Image { get; set; }

        public DateTime ProductDate { get; set; }

        public bool Available { get; set; }

        [StringLength(2000)]
        public string Description { get; set; }

        public int CategoryId { get; set; }

        [Required]
        [StringLength(50)]
        public string SupplierId { get; set; }

        public int Quantity { get; set; }

        public double Discount { get; set; }

        public bool Special { get; set; }

        public bool Latest { get; set; }

        public int Views { get; set; }

        public virtual Category Category { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }

        public virtual Supplier Supplier { get; set; }
    }
}
