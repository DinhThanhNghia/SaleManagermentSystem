namespace SaleManagementSystem.Model.DAL
{
    using System.ComponentModel.DataAnnotations;

    public partial class Property
    {
        public System.Guid Id { get; set; }
        [Display(Name = "Item Name")]
        public System.Guid ItemId { get; set; }
        [StringLength(200, ErrorMessage = "Description cannot be longer than 200 characters")]
        public string Description { get; set; }
        [StringLength(100, ErrorMessage = "Status cannot be longer than 100 characters")]
        public string Status { get; set; }
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Owned Date")]
        [Required(ErrorMessage = "Owned Date is required")]
        public System.DateTime OwnedDate { get; set; }
        public bool IsDeleted { get; set; }
        public System.DateTime InsAt { get; set; }
        public string InsBy { get; set; }
        public System.DateTime UpdAt { get; set; }
        public string UpdBy { get; set; }

        public virtual Item Item { get; set; }
    }
}
