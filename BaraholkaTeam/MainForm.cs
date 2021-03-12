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
            ShowFilters(GetFilters().ToList());
        }
        private void GroupBox_Click(object sender, EventArgs e) 
        {
            var groupBox = sender as GroupBox;
            var fName = (groupBox.Tag as FilterNameModel);

            if (fName.IsCollapsed)
            {
                fName.IsCollapsed = false;
            }
            else 
            {
                fName.IsCollapsed = true;
            }

            var checkedList = groupBox.Controls.OfType<CheckedListBox>().FirstOrDefault();
            checkedList.Visible = fName.IsCollapsed;
            if (fName.IsCollapsed)
            {
                groupBox.Height = checkedList.Height +30;
            }
            else 
            {
                groupBox.Height = 30;
            }

            FillGroupBoxes(this.Controls.OfType<GroupBox>().ToList());
        }
        private void FillGroupBoxes(IEnumerable<GroupBox> groupBoxes) 
        {
            int dy = 50;
            
            foreach (var groupBox in groupBoxes) 
            {
                if (groupBox.Name.Contains("gbFilter")) 
                {
                    groupBox.Location = new Point(825, dy);
                    dy += groupBox.Height + 10;
                }
            }
        }
        private IEnumerable<FilterNameModel> GetFilters() 
        {
            List<FilterNameModel> model = new List<FilterNameModel>();
            var filterNames = ConfigService.context.FilterNames.AsQueryable();
            var filterNameValues = ConfigService.context.FilterNameValues.AsQueryable();

            var joinedColl = from x in filterNames
                             join g in filterNameValues on x.Id equals g.FilterNameId into newJoinColl
                             from s in newJoinColl
                             select new 
                             {
                             FilterName = x.Name,
                             FilterNameId = x.Id,
                             FilterValue = s.FilterValue,
                             FilterValueId = s.FilterValueId
                             };

            var groupedCollection = from x in joinedColl.AsEnumerable()
                                    group x by new { x.FilterName, x.FilterNameId } into gColl
                                    select gColl;

            foreach (var item in groupedCollection) 
            {
                model.Add(new FilterNameModel { 
                Id = item.Key.FilterNameId,
                Name = item.Key.FilterName,
                Children = item.Select(x => new FilterValueModel {
                Id = x.FilterValueId,
                Name = x.FilterValue.Name
                }).ToArray()
                });
            }
            return model;
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
        private void ShowAllProducts(ProductCollection coll, string query = "")
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
                else if (res == DialogResult.No)
                {
                    MessageBox.Show("Невірний логін або пароль!");
                }
            }
        }
        private void ShowFilters(IEnumerable<FilterNameModel> models) 
        {
            System.Windows.Forms.GroupBox gbFilter = new GroupBox();
            System.Windows.Forms.CheckedListBox listFilter = new CheckedListBox();

            int dy = 50;
            foreach (var item in models) 
            {
                gbFilter = new System.Windows.Forms.GroupBox();
                listFilter = new System.Windows.Forms.CheckedListBox();
                gbFilter.SuspendLayout();
                // 
                // gbFilter
                // 
                gbFilter.Controls.Add(listFilter);
                gbFilter.ForeColor = System.Drawing.Color.Red;
                gbFilter.Location = new System.Drawing.Point(823, 47);
                gbFilter.Name = $"gbFilter{item.Id}";
                gbFilter.Size = new System.Drawing.Size(260, 171);
                gbFilter.TabStop = false;
                gbFilter.Text = item.Name;
                gbFilter.Tag = item;
                // 
                // listFilter
                // 
                listFilter.FormattingEnabled = true;
                listFilter.Location = new System.Drawing.Point(7, 27);
                listFilter.Name = $"listFilter{item.Id}";
                listFilter.Size = new System.Drawing.Size(247, 136);
                gbFilter.Location = new Point(825, dy);
                gbFilter.Click += GroupBox_Click;
                gbFilter.Size = new  Size(gbFilter.Size.Width,  listFilter.Height + 30);
                foreach (var child in item.Children)
                {
                    listFilter.Items.Add(child);
                }
                this.Controls.Add(gbFilter);
                dy += gbFilter.Height + 10;
            }
        }
        private void editForFilters_Click(object sender, EventArgs e)
        {
            //  Колекція ідентифікаторів чекнутих елементів
            List<int> CheckedItems = new List<int>();
            //  Колекція моделей, які формується на основі ідентифікаторів чекнутих елементів
            List<FilterProductModel> models = new List<FilterProductModel>();
            //  Витягування елементів GroupBox з колекції Controls, які є фільтрами
            var filters = this.Controls.OfType<GroupBox>().Where(x => x.Name.Contains("gbFilter"));
            //  Число блоків фільтрів
            int count = 0;
            foreach (var filterBlok in filters) 
            {
                var checkedList = filterBlok.Controls.OfType<CheckedListBox>().FirstOrDefault();
                foreach (var fValue in checkedList.CheckedItems) 
                {
                    CheckedItems.Add((fValue as FilterValueModel).Id);
                    int fNameModel = (filterBlok.Tag as FilterNameModel).Id;
                    int fValueModel = (fValue as FilterValueModel).Id;

                    models.Add(new FilterProductModel {
                        Id = count,
                        FilterNameId = fNameModel,
                        FilterValueId = fValueModel
                    }) ;
                }

                count++;
            }
            //  Витягуються усі продукти з БД
            var products = context.products.ToList();
            //  Проміжна колекція, яка використовуюється для запису у неї нових елементів
            //  Щоб в подальшому переприсвоїти колекцію products, а саме зменшити цю колекцію
            //  при формуанні ігнорувавши непідходящі елементи
            List<Product> newProduct;
            for (int i = 0; i < count; i++) 
            {
                //  Витягування усіх чекнутих фільтрів (Проміжна таблицяі tblFilterProducts), 
                //  які належать до i-го блока
                var filtersParams = models.Where(x => x.Id == i).ToList();
                //  Перевірка чи в блоці існують чекнуті елементи
                if (filtersParams.Count > 0) 
                {
                    //  Ініціювання проміжної колекції
                    newProduct = new List<Product>();
                    //  Проходження по продуктах
                    foreach (var product in products) 
                    {
                        //  Проходження по чекнутих елементах CheckedListBox-а
                        foreach (var filterParam in filtersParams) 
                        {
                            //  Якщо існує елемент в проміжній таблиці, у якого ProductId дорівнює 
                            //  ідентифікатору даного продукта
                            //  і FilterNameId дорінює filterParam.FilterNameId (Назва блоку фільтрів)
                            //  і FilterValueId дорінює filterParam.FilterValueId (Назва елемента фільтра)
                            //  FilterNameId i FilterValueId - це дані про чекнутий елемент фільтра,
                            //  який звязується з продуктом проміжною табличкою FilterProduct
                            if (FilterService.IsFind(product, filterParam) && !newProduct.Contains(product)) 
                            {
                                //  Якщо існує такий елемент в проміжній таблиці то додається
                                //  продукт до проміжної колекції
                                newProduct.Add(product);
                            }
                        }
                    }
                    //  Переприсвоєння
                    products = newProduct;
                }
                
            }
            //  Присвоєння до нового елемента кінцевого реультата
            var productsResult = products.ToList();
            //  Перегрупування груп-боксів на головній формі
            replaceGroupBox(productsResult);
            ///string strParam = "\"FilterValueId\" = ";
            ///string query = "SELECT * from \"tblFilterProductsTeamProject\" WHERE (";
            ///var filt = GetFilters().ToList();
            ///foreach (var fNameModel in filt) 
            ///{
            ///    int count = 1;
            ///    foreach (var fValueModel in fNameModel.Children) 
            ///    {
            ///        if (CheckedItems.Contains(fValueModel.Id)) 
            ///        {
            ///            query += strParam + fValueModel.Id.ToString() + " or ";
            ///         
            ///            count++;
            ///        }
            ///    }
            ///    if (count > 1)
            ///    {
            ///        filt.Remove(fNameModel);
            ///        query = query.Substring(0, query.Length - 4);
            ///        query += ")"; //and (";
            ///        fNameModel.IsCheckedItems = true;
            ///        break;
            ///    }
            ///    else 
            ///    {
            ///        fNameModel.IsCheckedItems = false;
            ///    }
            ///}
            ///query = query.Substring(0, query.Length - 6);
            ///if (CheckedItems.Count() != 0) 
            ///{
            ///    List<Product> products = FilterService.GetProducts(query, context).ToList();
            ///    foreach (var product in products) 
            ///    {
            ///        FilterService.CheckElement(product, filt);
            ///    }
            ///}
        }
        private void replaceGroupBox(List<Product> products) 
        {
            //  Видаляються усі елементи GroupBox, які повязані з продуктами
            var gbProd = this.Controls.OfType<GroupBox>().Where(x => x.Name.Contains("gbUser"));
            foreach (var box in gbProd) 
            {
                this.Controls.Remove(box);
            }

            //  Формується нові елементи
            //  Номер блока фільтрів, він впливає на розміщення dy елемента
            int count = 0;
            foreach (var product in products) 
            {
                //  Метод, який створює блок
                CreateGroupBox(product, count++);
            }
        }
    }
}
