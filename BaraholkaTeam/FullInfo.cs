using BaraholkaTeam.DAL.ContextData;
using BaraholkaTeam.Models;
using BaraholkaTeam.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace BaraholkaTeam
{
    public partial class FullInfo : Form
    {
        private Product product { get; set; }
        public MyContext context { get; set; }
        public FullInfo(MyContext context, Product product)
        {
            InitializeComponent();
            this.Text = product.Name;
            this.product = product;
            this.context = context;
        }

        private void FullInfo_Load(object sender, EventArgs e)
        {
            this.pbImage.Image = Image.FromStream(new MemoryStream(product.image));
            this.lblTitle.Text = product.Name;
            this.lblDesc.Text = product.Description;
            this.lblFullDesc.Text = product.FullDescription;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
        }

        private void btnContanct_Click(object sender, EventArgs e)
        {
            UserInfo user = SearchProducts.SearchOnId(this.context, product.UserId);
            UserInfoForm form = new UserInfoForm(user.Surname + " " + user.Name, user.telNum);
            form.ShowDialog();
        }
    }
}
