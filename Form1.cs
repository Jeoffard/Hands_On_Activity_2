using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Inventory
{
    public partial class FrmAddProduct : Form
    {
        string[] ListOfProductCategory;
        private double _SellPrice;
        private int _Quantity;
        private BindingSource showProductList;

        public string _ProductName;
        public string _Category;
        public string _MfgDate;
        public string _ExpDate;
        public string _Description;
        public string _Catefory;
        

        public FrmAddProduct()
        {
            InitializeComponent();
		    showProductList = new BindingSource();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string[] ListOfProductCategory =
            {
               "Beverages","Bread/Bakery","Canned/Jarred Goods","Dairy","Frozen Goods","Meat","Personal Care","Other"
            };

            foreach (string category in ListOfProductCategory)
            {
                cbCategory.Items.Add(category);
            }
        }

        public string Product_Name(string name)
		{
			if (!Regex.IsMatch(name, @"^[a-zA-Z]+$"))     
                throw new StringFormatException();
            return name;
        }
        public int Quantity(string qty)
		{
			if (!Regex.IsMatch(qty, @"^[0-9]"))
                throw new NumberFormatException();
			return Convert.ToInt32(qty);
		}
        public double SellingPrice(string price)
        {
            if (!Regex.IsMatch(price.ToString(), @"^(\d*\.)?\d+$"))
                throw new CurrencyFormatException();            
            return Convert.ToDouble(price);
        }

        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            try
            {
                _ProductName = Product_Name(txtProductName.Text);
                _Category = cbCategory.Text;
                _MfgDate = dtPickerMfgDate.Value.ToString("yyyy-MM-dd");
                _ExpDate = dtPickerExpDate.Value.ToString("yyyy-MM-dd");
                _Description = richTextDescription.Text;
                _Quantity = Quantity(txtQuantity.Text);
                _SellPrice = SellingPrice(txtSellPrice.Text);
                showProductList.Add(new ProductClass(_ProductName, _Category, _MfgDate, _ExpDate, _SellPrice, _Quantity, _Description));
                gridViewProductList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                gridViewProductList.DataSource = showProductList;
            }
            catch (NumberFormatException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (StringFormatException ex)
            {
                MessageBox.Show(ex.Message);

            }
            catch (CurrencyFormatException ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        
        public class NumberFormatException : Exception
        {
            public NumberFormatException()
            {

            }
            public NumberFormatException(string quantity) : base(quantity)
            {

            }
        }
        public class StringFormatException : Exception
        {
            public StringFormatException()
            {

            }
            public StringFormatException(string name) :base(name)
            {

            }
        }
        public class CurrencyFormatException : Exception
        {
            public CurrencyFormatException()
            {

            }
            public CurrencyFormatException(string price) :base(price)
            {

            }
        }

    }
}
