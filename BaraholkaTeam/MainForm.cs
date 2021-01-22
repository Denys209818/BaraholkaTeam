using BaraholkaTeam.DAL.ContextData;
using BaraholkaTeam.Services;
using BaraholkaTeamProject.DAL.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BaraholkaTeam
{
    public partial class MainForm : Form
    {
        MyContext context { set; get; }

        public List<GroupBox> boxes { get; set; } = new List<GroupBox>();
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.context = new MyContext();
            DBSeeder.SeedAll(this.context);
            int x = 0;

            foreach (Product product in SearchProducts.SearchProduct(context)) 
            {
                CreateGroupBox(product, x++);
            }
        }

        private void btnContact_Click(object sender, EventArgs e)
        {
            Button btn = (sender as Button);
        }

        private void CreateGroupBox(Product product, int id, int dy = 255) 
        {
            foreach (GroupBox gb in this.boxes) 
            {
                this.Controls.Remove(gb);
            }
            GroupBox gbUser = new GroupBox();
            PictureBox pcBox = new PictureBox();
            Label lblProductDescription = new Label();
            Label lblNameProduct = new Label();
            Button btnContact = new Button();
            Button btnAdd = new Button();
            // 
            // btnContact
            // 
            btnContact.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            btnContact.Font = new System.Drawing.Font("Segoe UI", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            btnContact.Location = new System.Drawing.Point(20, 171);
            btnContact.Name = $"btnContact{product.Id}";
            btnContact.Size = new System.Drawing.Size(125, 47);
            btnContact.TabIndex = 3;
            btnContact.Text = "Зв\'язатися з автором";
            btnContact.UseVisualStyleBackColor = false;
            btnContact.Click += new System.EventHandler(this.btnContact_Click);
            // 
            // lblProductDescription
            // 
            lblProductDescription.AutoEllipsis = true;
            lblProductDescription.Font = new System.Drawing.Font("Segoe UI", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lblProductDescription.Location = new System.Drawing.Point(161, 62);
            lblProductDescription.Name = $"lblProductDescription{product.Id}";
            lblProductDescription.Size = new System.Drawing.Size(327, 82);
            lblProductDescription.TabIndex = 2;
            lblProductDescription.Text = product.Description;
            // 
            // lblNameProduct
            // 
            lblNameProduct.AutoSize = true;
            lblNameProduct.Location = new System.Drawing.Point(161, 23);
            lblNameProduct.Name = $"lblNameProduct{product.Id}";
            lblNameProduct.Size = new System.Drawing.Size(156, 28);
            lblNameProduct.TabIndex = 1;
            lblNameProduct.Text = product.Name;
            // 
            // pcBox
            // 
            pcBox.Location = new System.Drawing.Point(20, 23);
            pcBox.Name = $"pcBox{product.Id}";
            pcBox.Size = new System.Drawing.Size(125, 121);
            pcBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            pcBox.TabIndex = 0;
            pcBox.TabStop = false;
            pcBox.Image = Image.FromStream(new MemoryStream(product.image));
            // 
            // btnAdd
            // 
            btnAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            btnAdd.Font = new System.Drawing.Font("Segoe UI", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            btnAdd.Location = new System.Drawing.Point(161, 171);
            btnAdd.Name = $"btnAdd{product.Id}";
            btnAdd.Size = new System.Drawing.Size(114, 47);
            btnAdd.TabIndex = 4;
            btnAdd.Text = "Додати в корзину";
            btnAdd.UseVisualStyleBackColor = false;




            gbUser.Controls.Add(btnAdd);
            gbUser.Controls.Add(btnContact);
            gbUser.Controls.Add(lblProductDescription);
            gbUser.Controls.Add(lblNameProduct);
            gbUser.Controls.Add(pcBox);
            gbUser.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            gbUser.Location = new System.Drawing.Point(153, (113 + ((id)*dy)));
            gbUser.Name = $"gbUser{product.Id}";
            gbUser.Size = new System.Drawing.Size(494, 224);
            gbUser.TabIndex = 2;
            gbUser.TabStop = false;

            this.Controls.Add(gbUser);
        }
    }
}
