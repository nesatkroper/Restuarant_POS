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

namespace Restuarant_POS.Food
{
    public partial class AddNewFood : Form
    {
        public AddNewFood()
        {
            InitializeComponent();
        }

        string conString = @"Data Source=(localdb)\local;Initial Catalog=Restuarant_DB;Integrated Security=True;";
        public string imgLocation = null;
        public string curentImageName = null;
        public string curentImagePath = null;

        FoodForm food = new FoodForm();

        // THIS FUNC USE TO BROWSE IMAGE INTO PICTURE BOX.
        void BrowseImage_()
        {
            try
            {
                OpenFileDialog dailog = new OpenFileDialog();
                dailog.Filter = "jpg files(*.jpg)|*.jpg| PNG files(*.png)|*.png| All Files(*.*)|*.*";
                dailog.FilterIndex = 1;
                if (dailog.ShowDialog() == DialogResult.OK)
                {
                    imgLocation = dailog.FileName.ToString();
                    pcbBrowse.ImageLocation = imgLocation;
                    curentImageName = Path.GetFileName(imgLocation);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //THIS FUNC USE TO COPY IMAGE TO APPLICATION LOCATION
        void CopyImage_()
        {
            string imgDesPath = Path.Combine(Application.StartupPath, "Images", curentImageName);
            try
            {
                File.Copy(imgLocation, imgDesPath);
                curentImagePath = imgDesPath;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //THIS FUNC USE TO ADD VALUE TO DATABASE.
        void InsertDatabase_()
        {
            SqlConnection conn = new SqlConnection(conString);
            try
            {
                conn.Open();
                string query_ = "INSERT INTO Food_TB (foo_code, foo_name, foo_price, foo_pic) VALUES (@code, @name, @price, @pic);";
                SqlCommand cmd = new SqlCommand(query_, conn);
                cmd.Parameters.AddWithValue("@code", txtCode.Text);
                cmd.Parameters.AddWithValue("@name", txtName.Text);
                cmd.Parameters.AddWithValue("@price", txtPrice.Text);
                cmd.Parameters.AddWithValue("@pic", curentImagePath);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (SqlException ex)
            {
                conn.Close();
                MessageBox.Show(ex.Message);
            }
        }

        private void AddNewFood_Load(object sender, EventArgs e)
        {

        }

        private void pcbBrowse_Click(object sender, EventArgs e)
        {
            BrowseImage_();
            CopyImage_();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            InsertDatabase_();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
