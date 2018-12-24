namespace SaleManagementSystem.Model.DAL
{
    using System.ComponentModel.DataAnnotations;

    public partial class Delivery
    {
        public System.Guid Id { get; set; }
        public System.Guid SaleId { get; set; }
        [Required(ErrorMessage = "Quantity is required")]
        [Range(0, int.MaxValue, ErrorMessage = "Quantity is greater than 0")]
        public int Quantity { get; set; }
        [Required(ErrorMessage = "Deliveried Date is required")]
        [Display(Name = "Deliveried Date")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public System.DateTime DeliveriedDate { get; set; }
        public bool IsDeleted { get; set; }
        public System.DateTime InsAt { get; set; }
        public string InsBy { get; set; }
        public System.DateTime UpdAt { get; set; }
        public string UpdBy { get; set; }

        public virtual Sale Sale { get; set; }
    }
}
