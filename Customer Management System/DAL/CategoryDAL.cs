using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using CustomerManagementSystem.Models;

namespace CustomerManagementSystem.DAL
{
    public class CategoryDAL
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["CustomerDB"].ConnectionString;

        // GET ALL CATEGORIES
        public List<Category> GetCategories()
        {
            List<Category> categories = new List<Category>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Categories";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    categories.Add(new Category
                    {
                        CategoryID = Convert.ToInt32(reader["CategoryID"]),
                        CategoryName = reader["CategoryName"].ToString(),
                        CategoryDescription = reader["CategoryDescription"].ToString()
                    });
                }
            }
            return categories;
        }

        // ADD CATEGORY
        public void AddCategory(Category category)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Categories (CategoryName, CategoryDescription) VALUES (@Name, @Description)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Name", category.CategoryName);
                cmd.Parameters.AddWithValue("@Description", category.CategoryDescription);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
