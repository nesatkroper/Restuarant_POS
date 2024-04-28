using Restuarant_POS.Item;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Restuarant_POS
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnFood_Click(object sender, EventArgs e)
        {
            FoodForm foodForm = new FoodForm();
            foodForm.TopLevel = false;
            foodForm.WindowState = FormWindowState.Maximized;
            foodForm.Dock = DockStyle.Fill;
            pnData.Controls.Add(foodForm);
            foodForm.Show();
            foodForm.BringToFront();
        }

        private void btnAllItem_Click(object sender, EventArgs e)
        {
            OrderForm orderForm = new OrderForm();
            orderForm.TopLevel = false;
            orderForm.WindowState = FormWindowState.Maximized;
            orderForm.Dock = DockStyle.Fill;
            pnData.Controls.Add(orderForm);
            orderForm.Show();
            orderForm.BringToFront();
        }
    }
}
