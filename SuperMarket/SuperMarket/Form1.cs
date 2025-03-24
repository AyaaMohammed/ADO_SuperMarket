using DataBusinessLayer;
using Microsoft.Data.SqlClient;
using System.Windows.Forms;

namespace SuperMarket
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            dataGridView1.DataSource = ProductManager.GetAll();
            dataGridView1.Columns["CategoryID"].Visible = false;
            dataGridView1.Columns["Image"].Visible = false;

            InitializeComboBox(comboBox2, CategoryManager.GetAllNames(), "Choose");
            InitializeComboBox(comboBox1, CategoryManager.GetAllNames(), "Choose");
            InitializeComboBox(comboBox3, SupplierManager.GetAllNames(), "Choose");
            InitializeComboBox(comboBox4, SupplierManager.GetAllNames(), "Choose");

            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.SelectionChanged += DataGridView1_SelectionChanged;

            if (dataGridView1.Rows.Count > 0)
            {
                dataGridView1.Rows[0].Selected = true;  
            }
        }
        private void DataGridView1_SelectionChanged(object sender, EventArgs e)
        { 
            if (dataGridView1.CurrentRow != null)
            {
                var selectedRow = dataGridView1.CurrentRow;
                TxtBox1.Text = selectedRow.Cells["Name"].Value?.ToString() ?? "";
                TxtBox3.Text = selectedRow.Cells["Price"].Value?.ToString() ?? "";
                TxtBox2.Text = selectedRow.Cells["NumOfStock"].Value?.ToString() ?? "";
                comboBox2.SelectedValue = selectedRow.Cells["CategoryID"].Value ?? DBNull.Value;
                comboBox4.SelectedValue = selectedRow.Cells["ID"].Value ?? DBNull.Value;
            }
        }
        
        private void InitializeComboBox(ComboBox comboBox, List<KeyValuePair<int, string>> dataSource, string defaultText)
        {
            comboBox.DataSource = new BindingSource(dataSource, null);
            comboBox.DisplayMember = "Value";
            comboBox.ValueMember = "Key";
            comboBox.SelectedIndex = -1;
            comboBox.Text = defaultText;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            TxtBox1.Text = "";
            TxtBox2.Text = "";
            TxtBox3.Text = "";
            InitializeComboBox(comboBox2, CategoryManager.GetAllNames(), "Choose");
            InitializeComboBox(comboBox4, SupplierManager.GetAllNames(), "Choose");
        }
        private Product GetProductFromForm()
        {
            string name = TxtBox1.Text;
            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Please enter a valid product name.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
            if (!int.TryParse(TxtBox3.Text, out int price))
            {
                MessageBox.Show("Please enter a valid price.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
            if (!int.TryParse(TxtBox2.Text, out int quantity))
            {
                MessageBox.Show("Please enter a valid quantity.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
            int categoryId = (comboBox2.SelectedItem != null && comboBox2.SelectedValue != null)
                             ? Convert.ToInt32(comboBox2.SelectedValue)
                             : -1;

            int supplierId = (comboBox4.SelectedItem != null && comboBox4.SelectedValue != null)
                             ? Convert.ToInt32(comboBox4.SelectedValue)
                             : -1;

            if (categoryId == -1)
            {
                MessageBox.Show("Please select a valid category.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
            if (supplierId == -1)
            {
                MessageBox.Show("Please select a valid supplier.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }

            Product product = new Product
            {
                Name = name,
                Price = price,
                Image = null,
                NumOfStock = quantity,
                CategoryID = categoryId,
                SupplierID = supplierId,
                SupplierName = SupplierManager.GetById(supplierId),
                CategoryName = CategoryManager.GetById(categoryId)
            };
            return product;
        }
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {                
                ProductManager.Insert(GetProductFromForm());
                MessageBox.Show("Product saved successfully.");
                dataGridView1.DataSource = ProductManager.GetAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An unexpected error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                var selectedRow = dataGridView1.CurrentRow;

                // Get ID from the selected row
                object idValue = selectedRow.Cells["ID"].Value;
                int id = (idValue != null && idValue != DBNull.Value) ? Convert.ToInt32(idValue) : 0;
                if (id > 0)
                {
                    
                    int count = ProductManager.Update(GetProductFromForm());
                    if(count > 0)
                    {
                        MessageBox.Show("Product edit successfully.");
                        dataGridView1.DataSource = ProductManager.GetAll();
                    }
                    else
                    {
                        MessageBox.Show("Product edit failed.");
                    }
                    dataGridView1.DataSource = ProductManager.GetAll();
                }
                else
                {
                    ProductManager.Insert(GetProductFromForm());
                    MessageBox.Show("Product saved successfully.");
                    dataGridView1.DataSource = ProductManager.GetAll();
                }
            }
        }

    }


    }
