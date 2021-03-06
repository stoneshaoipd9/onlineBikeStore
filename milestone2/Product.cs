//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace milestone2
{
    using Foolproof;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            this.SalesOrderDetails = new HashSet<SalesOrderDetail>();
        }
    
        public int ProductID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string ProductNumber { get; set; }

        [Required]
        public string Color { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Do not accept negative values.")]
        [DataType(DataType.Currency)]
        public decimal StandardCost { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Do not accept negative values.")]
        [GreaterThan("StandardCost")]
        [DataType(DataType.Currency)]
        public decimal ListPrice { get; set; }

        [Required]
        public string Size { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Do not accept negative values.")]
        public Nullable<decimal> Weight { get; set; }

        public Nullable<int> ProductCategoryID { get; set; }
        public Nullable<int> ProductModelID { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public System.DateTime SellStartDate { get; set; }

        [DataType(DataType.Date)]
        public Nullable<System.DateTime> SellEndDate { get; set; }

        [DataType(DataType.Date)]
        public Nullable<System.DateTime> DiscontinuedDate { get; set; }

        [ScaffoldColumn(false)]
        public byte[] ThumbNailPhoto { get; set; }


        [ScaffoldColumn(false)]
        public string ThumbnailPhotoFileName { get; set; }

        [ScaffoldColumn(false)]
        public System.Guid rowguid { get; set; }

        [ScaffoldColumn(false)]
        public System.DateTime ModifiedDate { get; set; }
    
        public virtual ProductCategory ProductCategory { get; set; }
        public virtual ProductModel ProductModel { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SalesOrderDetail> SalesOrderDetails { get; set; }
    }
}
