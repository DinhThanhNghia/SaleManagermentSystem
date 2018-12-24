namespace SaleManagementSystem.Model.DAL
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public partial class Purchase
    {
        public System.Guid Id { get; set; }
        [Display(Name = "Supplier")]
        public System.Guid SupplierId { get; set; }
        [StringLength(200, ErrorMessage = "Description cannot be longer than 200 characters")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Price is required")]
        [DataType(DataType.Currency)]
        [Range(0, int.MaxValue, ErrorMessage = "Price is not least than 0")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "Purchased Date is required")]
        [Display(Name = "Purchased Date")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime PurchaseDate { get; set; }
        public bool IsDeleted { get; set; }
        public System.DateTime InsAt { get; set; }
        public string InsBy { get; set; }
        public System.DateTime UpdAt { get; set; }
        public string UpdBy { get; set; }

        public virtual Supplier Supplier { get; set; }
    }
}
