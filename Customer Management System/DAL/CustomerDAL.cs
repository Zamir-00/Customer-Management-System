using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using CustomerManagementSystem.Models;

namespace CustomerManagementSystem.DAL
{
    public class CustomerDAL
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["CustomerDB"].ConnectionString;

        // GET ALL CUSTOMERS
        public List<Customer> GetCustomers()
        {
            List<Customer> customers = new List<Customer>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"SELECT c.CustomerID, c.CustomerCode, c.CustomerName, c.CustomerCategory,
                                        c.Email, c.Phone, c.CreatedDate, c.UpdateDate, cat.CategoryName 
                                 FROM Customers c 
                                 INNER JOIN Categories cat ON c.CustomerCategory = cat.CategoryID";

                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    customers.Add(new Customer
                    {
                        CustomerID = Convert.ToInt32(reader["CustomerID"]),
                        CustomerCode = reader["CustomerCode"].ToString(),
                        CustomerName = reader["CustomerName"].ToString(),
                        CustomerCategory = Convert.ToInt32(reader["CustomerCategory"]),
                        Email = reader["Email"].ToString(),
                        Phone = reader["Phone"].ToString(),
                        CreatedDate = Convert.ToDateTime(reader["CreatedDate"]),
                        UpdateDate = Convert.ToDateTime(reader["UpdateDate"]),
                        CategoryName = reader["CategoryName"].ToString() // Display category name
                    });
                }
            }
            return customers;
        }

        // ADD CUSTOMER
        public void AddCustomer(Customer customer)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Customers (CustomerCode, CustomerName, CustomerCategory, Email, Phone) " +
                               "VALUES (@Code, @Name, @Category, @Email, @Phone)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Code", customer.CustomerCode);
                cmd.Parameters.AddWithValue("@Name", customer.CustomerName);
                cmd.Parameters.AddWithValue("@Category", customer.CustomerCategory);
                cmd.Parameters.AddWithValue("@Email", customer.Email);
                cmd.Parameters.AddWithValue("@Phone", customer.Phone);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // UPDATE CUSTOMER
        public void UpdateCustomer(Customer customer)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "UPDATE Customers SET CustomerCode = @Code, CustomerName = @Name, " +
                               "CustomerCategory = @Category, Email = @Email, Phone = @Phone, UpdateDate = GETDATE() " +
                               "WHERE CustomerID = @ID";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Code", customer.CustomerCode);
                cmd.Parameters.AddWithValue("@Name", customer.CustomerName);
                cmd.Parameters.AddWithValue("@Category", customer.CustomerCategory);
                cmd.Parameters.AddWithValue("@Email", customer.Email);
                cmd.Parameters.AddWithValue("@Phone", customer.Phone);
                cmd.Parameters.AddWithValue("@ID", customer.CustomerID);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // DELETE CUSTOMER
        public void DeleteCustomer(int customerId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Customers WHERE CustomerID = @ID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ID", customerId);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
