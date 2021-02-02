using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BaraholkaTeam
{
    public partial class UserInfoForm : Form
    {
        public UserInfoForm(string name, string telNum)
        {
            InitializeComponent();
            this.lblName.Text = "Ім'я продавця: " + name;
            this.lblTel.Text = "Контактні дані: " + telNum;
        }
    }
}
