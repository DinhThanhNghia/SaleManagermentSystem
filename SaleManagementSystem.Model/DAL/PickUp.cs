namespace SaleManagementSystem.Model.DAL
{
    using System.ComponentModel.DataAnnotations;

    public partial class PickUp
    {
        public System.Guid Id { get; set; }
        [Display(Name = "Supplier")]
        public System.Guid SupplierId { get; set; }
        [Display(Name = "Employee")]
        public System.Guid EmployeeId { get; set; }
        [Required(ErrorMessage = "Pickup Date is required")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Pickup Date")]
        public System.DateTime PickUpDate { get; set; }
        [Required(ErrorMessage = "Price is required")]
        [Range(0, int.MaxValue, ErrorMessage = "Price is not least than 0")]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "Quantity is required")]
        [Range(0, int.MaxValue, ErrorMessage = "Quantity is greater than 0")]
        public int Quantity { get; set; }
        [StringLength(200, ErrorMessage = "Address cannot be longer than 200 characters")]
        public string Address { get; set; }
        [StringLength(50, ErrorMessage = "Unit cannot be longer than 50 characters")]
        public string Unit { get; set; }
        public bool IsDeleted { get; set; }
        public System.DateTime InsAt { get; set; }
        public string InsBy { get; set; }
        public System.DateTime UpdAt { get; set; }
        public string UpdBy { get; set; }
        [Display(Name = "Total Amount")]
        [DataType(DataType.Currency)]
        public decimal TotalAmount => Price * Quantity;
        public virtual Employee Employee { get; set; }
        public virtual Supplier Supplier { get; set; }
    }
}
