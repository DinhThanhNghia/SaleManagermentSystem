namespace SaleManagementSystem.Model.DAL
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public partial class MenuItem
    {
        public System.Guid Id { get; set; }
        [Display(Name = "Item Name")]
        public System.Guid ItemId { get; set; }
        [Required(ErrorMessage = "Created Date is required")]
        [Display(Name = "Created Date")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public System.DateTime CreateDate { get; set; }
        [Required(ErrorMessage = "Last Used Date is required")]
        [Display(Name = "Last Used Date")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public System.DateTime LastUsedDate { get; set; }
        [Range(1, 5, ErrorMessage = "Rating range from 1 to 5")]
        public Nullable<int> Rating { get; set; }
        public bool IsDeleted { get; set; }
        public System.DateTime InsAt { get; set; }
        public string InsBy { get; set; }
        public System.DateTime UpdAt { get; set; }
        public string UpdBy { get; set; }

        public virtual Item Item { get; set; }
    }
}
