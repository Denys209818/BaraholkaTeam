using BaraholkaTeam.DAL.ContextData;
using BaraholkaTeam.Models;
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
using System.Threading;
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
            ConfigService.context = this.context;
            DBSeeder.SeedAll(this.context);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            ShowAllProducts(SearchProducts.SearchProduct(context));
        }
        private void btnContact_Click(object sender, EventArgs e)
        {
            Button btn = (sender as Button);
            int id = int.Parse(btn.Name.Last().ToString());
            UserInfo user = SearchProducts.SearchOnId(context, id);
            UserInfoForm userInfo = new UserInfoForm(user.Name + " " + user.Surname, user.telNum);
            userInfo.StartPosition = FormStartPosition.CenterParent;
            userInfo.ShowDialog();
        }
        private void CreateGroupBox(Product product, int id, int dy = 305) 
        {
            GroupBox gbUser = GetGroupBox(product, id, dy);

            this.Controls.Add(gbUser);
            this.boxes.Add(gbUser);
        }
        private GroupBox GetGroupBox(Product product, int id, int dy = 305) 
        {
            GroupBox gbUser = new GroupBox();
            PictureBox pcBox = new PictureBox();
            Label lblProductDescription = new Label();
            LinkLabel lblNameProduct = new LinkLabel();
            Button btnContact = new Button();
            Button btnAdd = new Button();
            Label lblPrice = new Label();
            // 
            // btnContact
            // 
            btnContact.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            btnContact.Font = new System.Drawing.Font("Segoe UI", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            btnContact.Location = new System.Drawing.Point(20, 220);
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
            lblProductDescription.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lblProductDescription.Location = new System.Drawing.Point(180, 62);
            lblProductDescription.Name = $"lblProductDescription{product.Id}";
            lblProductDescription.Size = new System.Drawing.Size(430, 140);
            lblProductDescription.TabIndex = 2;
            lblProductDescription.Text = product.Description;
            // 
            // lblNameProduct
            // 
            lblNameProduct.AutoSize = true;
            lblNameProduct.Location = new System.Drawing.Point(180, 23);
            lblNameProduct.Name = $"lblNameProduct{product.Id}";
            lblNameProduct.Size = new System.Drawing.Size(156, 28);
            lblNameProduct.TabIndex = 1;
            lblNameProduct.Text = product.Name;
            lblNameProduct.Tag = product;
            lblNameProduct.Click += new EventHandler(LinkTitle_Click);
            // 
            // pcBox
            // 
            pcBox.Location = new System.Drawing.Point(20, 23);
            pcBox.Name = $"pcBox{product.Id}";
            pcBox.Size = new System.Drawing.Size(150, 150);
            pcBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            pcBox.TabIndex = 0;
            pcBox.TabStop = false;
            pcBox.Image = Image.FromStream(new MemoryStream(product.image));
            // 
            // btnAdd
            // 
            btnAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            btnAdd.Font = new System.Drawing.Font("Segoe UI", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            btnAdd.Location = new System.Drawing.Point(161, 220);
            btnAdd.Name = $"btnAdd{product.Id}";
            btnAdd.Size = new System.Drawing.Size(114, 47);
            btnAdd.TabIndex = 4;
            btnAdd.Text = "Додати в корзину";
            btnAdd.UseVisualStyleBackColor = false;
            btnAdd.Tag = product;
            btnAdd.Click += new EventHandler(AddToCart);
            //
            //lblPrice
            //
            lblPrice.Name = $"lblPrice{product.Id}";
            lblPrice.Location = new Point(500, 230);
            lblPrice.Text = "Ціна: " + product.Price.ToString();
            lblPrice.AutoSize = true;


            gbUser.Controls.Add(btnAdd);
            gbUser.Controls.Add(btnContact);
            gbUser.Controls.Add(lblProductDescription);
            gbUser.Controls.Add(lblNameProduct);
            gbUser.Controls.Add(pcBox);
            gbUser.Controls.Add(lblPrice);
            gbUser.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            gbUser.Location = new System.Drawing.Point(153, (113 + ((id) * dy)));
            gbUser.Name = $"gbUser{product.Id}";
            gbUser.Size = new System.Drawing.Size(650, 270);
            gbUser.TabIndex = 2;
            gbUser.TabStop = false;

            return gbUser;
        }
        private void ShowAllProducts(ProductCollection coll, string query ="") 
        {
            foreach (GroupBox gb in this.boxes)
            {
                this.Controls.Remove(gb);
            }
            this.boxes.Clear();
            int x = 0;
            foreach (var product in SearchProducts.SearchProduct(context, query).products) 
            {
                CreateGroupBox(product, x++);
            }
        }
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            ShowAllProducts(SearchProducts.SearchProduct(context, txtSearch.Text), txtSearch.Text);
        }
        private void LinkTitle_Click(object sender, EventArgs e) 
        {
            var product = (sender as LinkLabel).Tag as Product;
            FullInfo info = new FullInfo(this.context, product);
            this.Visible = false;
            info.ShowDialog();
            this.Visible = true;
        }
        private void toolStripMenuItemKorsina_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            CartForm form = new CartForm();
            form.ShowDialog();
            this.Visible = true;
        }
        private void AddToCart(object sender, EventArgs e) 
        {
            Product product = (sender as Button).Tag as Product;
            if (!ConfigService.products.Contains(product))
            {
                ConfigService.products.Add(product);
                MessageBox.Show("Товар доданий у корзину!");
            }
            else 
            {
                MessageBox.Show("Товар уже був доданий!");
            }
        }

        private void toolStripMenuItemProfile_Click(object sender, EventArgs e)
        {
            if (ConfigService.IsAuth)
            {
            this.Visible = false;
                ProfileForm form = new ProfileForm();
                form.ShowDialog();
            this.Visible = true;
            }
            else 
            {
                AuthForm auth = new AuthForm();
                DialogResult res = auth.ShowDialog();
                if (res == DialogResult.OK)
                {
                    ConfigService.IsAuth = true;
                    MessageBox.Show("Авторизовано!");
                }
                else if(res == DialogResult.No)
                {
                    MessageBox.Show("Невірний логін або пароль!");
                }
            }
        }
    }
}
