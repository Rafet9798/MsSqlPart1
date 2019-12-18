using System;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

namespace MsSqlPart1
{
    public partial class Form1 : Form
    {
        SqlConnection connection = new SqlConnection("Server=.;Initial Catalog=UniversityDb;Integrated Security=True");
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            GetDepartments();
        }
      
        private void GetDepartments()
        {
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("select * from Departments", connection);
            sqlDataAdapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            GetDepartments();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string insertQuery = $"insert into Departments values('{txtDepartmentName.Text}')";
            if (connection.State != ConnectionState.Open)
                connection.Open();
            SqlCommand insertCommand = new SqlCommand(insertQuery, connection);
            if (insertCommand.ExecuteNonQuery() != -1)
            {
                MessageBox.Show("Department added");
                GetDepartments();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string deleteQuery = $"Delete from Departments where DepartmentId='{txtDepartmentId.Text}'";
            if (connection.State != ConnectionState.Open)
                connection.Open();
            SqlCommand deleteCommand = new SqlCommand(deleteQuery, connection);
            if (deleteCommand.ExecuteNonQuery() >0)
            {
                MessageBox.Show("Department deleted");
                GetDepartments();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtDepartmentId.Text = dataGridView1.CurrentCell.Value.ToString();
        }
    }
}
