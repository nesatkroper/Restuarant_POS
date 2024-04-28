using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Restuarant_POS.Item
{
    public partial class OrderForm : Form
    {
        public OrderForm()
        {
            InitializeComponent();
        }

        int count = 0;
        int curentIncres = 0;
        string itemName = "gdtg";
        int orQty = 0;
        double orCost = 0;
        double orTotalPrice = 0;

        void OrderItem_()
        {
            itemName = "Salad";
            curentIncres++;
            orQty = 1;
            orCost = 6.99;
            orTotalPrice = orTotalPrice + orCost;
            lblTotalPrice.Text = orTotalPrice.ToString("$ #.00");
            dgvOrder.Rows.Add(curentIncres, itemName, orQty, orCost);
        }

        private void OrderForm_Load(object sender, EventArgs e)
        {
            dgvOrder.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(239, 120, 40);
            dgvOrder.Columns[0].Width = 50;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            lvOrder.Items.Clear();
            dgvOrder.Rows.Clear();
            dgvOrder.Refresh();
        }

        private void pnBurger_Click(object sender, EventArgs e)
        {
            itemName = "Hamberger";
            for (int i = 0; i < dgvOrder.Rows.Count; i++)
            {
                if (string.IsNullOrEmpty(dgvOrder.SelectedRows[i].Cells[1].Value.ToString()) && dgvOrder.SelectedRows[i].Cells[1].Value.ToString() == itemName)
                {
                    orQty++;
                    orCost++;
                    orTotalPrice += (orTotalPrice + (orCost / 2));
                }
                else
                {
                    curentIncres++;

                    orQty = 1;
                    orCost = 4.99;
                    orTotalPrice = orTotalPrice + orCost;
                }
            }
            lblTotalPrice.Text = orTotalPrice.ToString("$ #.00");
            dgvOrder.Rows.Add(curentIncres, itemName, orQty, orCost);
        }

        private void pnSalad_Click(object sender, EventArgs e)
        {
            itemName = "Salad";
            curentIncres++;
            orQty = 1;
            orCost = 6.99;

            ListViewItem lvi = new ListViewItem();

            if (!string.IsNullOrEmpty(lvOrder.Items[0].SubItems[1].Text) || !string.IsNullOrWhiteSpace(lvOrder.Items[0].SubItems[1].Text))
            {
                if (lvOrder.Items[0].SubItems[1].Text == itemName)
                {

                    lvi.Text = curentIncres.ToString();
                    lvi.SubItems.Add(itemName.ToString());
                    lvi.SubItems.Add(orQty.ToString());
                    lvi.SubItems.Add(orCost.ToString());
                    lvOrder.Items.Add(lvi);
                }
            }
            else
            {
                lvi.Text = curentIncres.ToString();
                lvi.SubItems.Add(itemName.ToString());
                lvi.SubItems.Add(orQty.ToString());
                lvi.SubItems.Add(orCost.ToString());
                lvOrder.Items.Add(lvi);
            }

            orTotalPrice = orTotalPrice + orCost;
            lblTotalPrice.Text = orTotalPrice.ToString("$ #.00");
            MessageBox.Show(lvOrder.Items[0].SubItems[1].Text);
        }

    }
}
