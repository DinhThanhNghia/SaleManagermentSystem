namespace SaleManagementSystem.Model.DAL
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Sale
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Sale()
        {
            this.Deliveries = new HashSet<Delivery>();
        }
    
        public System.Guid Id { get; set; }
        [Display(Name = "Employee")]
        public System.Guid EmployeeId { get; set; }
        [Display(Name = "Customer")]
        public System.Guid CustomerId { get; set; }
        [Required(ErrorMessage = "Sale Code is required")]
        [Display(Name = "Sale Code")]
        [StringLength(50, ErrorMessage = "Sale cannot be longer than 50 characters")]
        public string SaleCode { get; set; }
        [Required(ErrorMessage = "Unit is required")]
        [StringLength(50, ErrorMessage = "Unit cannot be longer than 50 characters")]
        public string Unit { get; set; }
        [Required(ErrorMessage = "Price is required")]
        [DataType(DataType.Currency)]
        [Range(0, int.MaxValue, ErrorMessage = "Price is not least than 0")]
        public decimal Price { get; set; }
        [Display(Name = "Item Name")]
        public System.Guid ItemId { get; set; }
        [Required(ErrorMessage = "Quantity is required")]
        [Range(0, int.MaxValue, ErrorMessage = "Quantity is greater than 0")]
        public int Quantity { get; set; }
        public bool IsDeleted { get; set; }
        public System.DateTime InsAt { get; set; }
        public string InsBy { get; set; }
        public System.DateTime UpdAt { get; set; }
        public string UpdBy { get; set; }
        [DataType(DataType.Currency)]
        [Display(Name = "Total Amount")]
        public decimal TotalAmount => Price * Quantity;
        public virtual Customer Customer { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Delivery> Deliveries { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual Item Item { get; set; }
    }
}
