using Restuarant_POS.Food;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Restuarant_POS
{
    public partial class FoodForm : Form
    {
        public FoodForm()
        {
            InitializeComponent();
        }

        string conString = @"Data Source=(localdb)\local;Initial Catalog=Restuarant_DB;Integrated Security=True;";
        string imgLocation = null;
        string curentImageName = null;
        string curentImagePath = null;
        string id_ = null;

        //THIS FUNC USE TO RELOAD DATA FROM DATABASE.
        public void Reload_()
        {
            SqlConnection conn = new SqlConnection(conString);
            try
            {
                conn.Open();
                string query_ = "SELECT * FROM Food_TB";
                SqlCommand cmd = new SqlCommand(query_, conn);
                SqlDataReader dr = cmd.ExecuteReader();
                dgvData.Rows.Clear();
                dgvData.Refresh();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        dgvData.Rows.Add(dr[0], dr[1], dr[2], dr[3], Image.FromFile(dr[4].ToString()));
                    }
                }
                conn.Close();
            }
            catch (SqlException exp)
            {
                conn.Close();
                MessageBox.Show(exp.Message);
            }
        }

        //THIS USE TO DELETE IMAGE 
        void DeleteImage_()
        {
            try
            {
                File.Delete(curentImagePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //THIS USE TO SELECT DATA
        void SelectData_()
        {
            SqlConnection conn = new SqlConnection(conString);
            try
            {
                string query_ = "SELECT * FROM Food_TB  WHERE foo_id = @id;";
                conn.Open();
                SqlCommand cmd = new SqlCommand(query_, conn);
                cmd.Parameters.AddWithValue("@id", id_);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        curentImagePath = dr[4].ToString();
                    }
                }
                conn.Close();
            }
            catch (SqlException exp)
            {
                conn.Close();
                MessageBox.Show(exp.Message);
            }
        }

        //THIS USE TO DELETE DATA IN DATABASE
        void deleteDatabase_()
        {
            try
            {
                SqlConnection conn = new SqlConnection(conString);
                string query_ = "DELETE FROM Food_TB  WHERE foo_id = @id;";
                conn.Open();
                SqlCommand cmd = new SqlCommand(query_, conn);
                cmd.Parameters.AddWithValue("@id", id_);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void FoodForm_Load(object sender, EventArgs e)
        {
            dgvData.Columns[0].Width = 100;
            //dgvData.Columns[1].Width = 80;
            //dgvData.Columns[2].Width = 120;
            //dgvData.Columns[3].Width = 120;
            //dgvData.Columns[4].Width = 80;
            dgvData.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(239, 120, 40);

            dgvData.RowTemplate.Height = 250;
            Reload_();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddNewFood addNewFood = new AddNewFood();
            addNewFood.Show();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            Reload_();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            SelectData_();
            deleteDatabase_();
            Reload_();
            DeleteImage_();
            curentImagePath = null;
        }

        private void dgvData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvData.SelectedRows.Count > 0)
            {
                id_ = dgvData.SelectedRows[0].Cells[0].Value.ToString();
            }
        }
    }
}
