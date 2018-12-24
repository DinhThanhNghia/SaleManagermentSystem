using System;

namespace SaleManagementSystem.Common
{
    public class AuditTable
    {
        public static void InsertAuditFields(object obj)
        {
            dynamic item = obj;
            item.IsDeleted = false;
            item.InsAt = DateTime.Now;
            item.InsBy = "Admin";
            item.UpdAt = DateTime.Now;
            item.UpdBy = "Admin";
        }
        public static void UpdateAuditFields(object obj)
        {
            dynamic item = obj;
            item.UpdAt = DateTime.Now;
            item.UpdBy = "Admin";
        }
    }
}
