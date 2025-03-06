using System;

namespace CustomerManagementSystem.Models
{
    public class Customer
    {
        public int CustomerID { get; set; }  // Primary Key
        public string CustomerCode { get; set; }  // Unique Code
        public string CustomerName { get; set; }  // Customer Name
        public int CustomerCategory { get; set; }  // Foreign Key (CategoryID)
        public string Email { get; set; }  // Unique Email
        public string Phone { get; set; }  // Phone Number
        public DateTime CreatedDate { get; set; }  // Default to GETDATE()
        public DateTime UpdateDate { get; set; }  // Default to GETDATE()

        // Navigation Property (for Category Name display)
        public string CategoryName { get; set; }
    }
}
