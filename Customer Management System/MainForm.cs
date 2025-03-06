using CustomerManagementSystem.DAL;
using CustomerManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace Customer_Management_System
{
    public partial class MainForm : Form
    {
        private CustomerDAL customerDAL = new CustomerDAL(); 
        private CategoryDAL categoryDAL = new CategoryDAL();
        public MainForm()
        {
            InitializeComponent();
            LoadCustomers();
            LoadCategories();
        }
        private void LoadCustomers()
        {
            dgvCustomers.DataSource = customerDAL.GetCustomers();
            dgvCustomers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
        private void LoadCategories()
        {
            List<Category> categories = categoryDAL.GetCategories();
            cmbCategory.DataSource = categories;
            cmbCategory.DisplayMember = "CategoryName";
            cmbCategory.ValueMember = "CategoryID";
        }


        private void MainForm_Load(object sender, EventArgs e)
        {
            this.customersTableAdapter.Fill(this.customerManagementDataSet.Customers);
            try
            {
                var customers = customerDAL.GetCustomers();
                MessageBox.Show($"Loaded {customers.Count} customers.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvCustomers.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a customer to delete.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int customerID = Convert.ToInt32(dgvCustomers.SelectedRows[0].Cells[0].Value);

            DialogResult result = MessageBox.Show("Are you sure you want to delete this customer?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                customerDAL.DeleteCustomer(customerID);
                LoadCustomers();
                MessageBox.Show("Customer deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string searchQuery = txtSearch.Text.ToLower();
            List<Customer> filteredList = customerDAL.GetCustomers()
                .FindAll(c => c.CustomerName.ToLower().Contains(searchQuery) || c.Email.ToLower().Contains(searchQuery));

            dgvCustomers.DataSource = filteredList;
        }


        private void txtName_TextChanged(object sender, EventArgs e)
        {

        }

        private void cmbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtName.Text) || string.IsNullOrEmpty(txtEmail.Text))
            {
                MessageBox.Show("Name and Email are required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Customer newCustomer = new Customer
            {
                CustomerCode = txtCode.Text,
                CustomerName = txtName.Text,
                CustomerCategory = Convert.ToInt32(cmbCategory.SelectedValue),
                Email = txtEmail.Text,
                Phone = txtPhone.Text
            };

            customerDAL.AddCustomer(newCustomer);
            LoadCustomers();
            MessageBox.Show("Customer added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvCustomers.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a customer to update.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var cellValue = dgvCustomers.SelectedRows[0].Cells[0].Value;

            if (cellValue == null || cellValue == DBNull.Value)
            {
                MessageBox.Show("Invalid customer selection.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int customerID = Convert.ToInt32(cellValue);

            Customer updatedCustomer = new Customer
            {
                CustomerID = customerID,
                CustomerCode = txtCode.Text,
                CustomerName = txtName.Text,
                CustomerCategory = Convert.ToInt32(cmbCategory.SelectedValue),
                Email = txtEmail.Text,
                Phone = txtPhone.Text
            };

            customerDAL.UpdateCustomer(updatedCustomer);
            LoadCustomers();
            MessageBox.Show("Customer updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
        }

        private void textSearch_TextChanged(object sender, EventArgs e)
        {
            string searchQuery = txtSearch.Text.ToLower();
            List<Customer> filteredList = customerDAL.GetCustomers()
                .FindAll(c => c.CustomerName.ToLower().Contains(searchQuery) || c.Email.ToLower().Contains(searchQuery));

            dgvCustomers.DataSource = filteredList;

        }
        private void btnManageCategories_Click(object sender, EventArgs e)
        {
            CategoryForm categoryForm = new CategoryForm();
            categoryForm.ShowDialog();
            LoadCategories(); // Reload categories after closing the form
        }

    }
}
