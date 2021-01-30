using BaraholkaTeam.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BaraholkaTeam
{
    public partial class AuthForm : Form
    {
        public AuthForm()
        {
            InitializeComponent();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtLogin.Text) &&
                !string.IsNullOrEmpty(txtPassword.Text))
            {
               var user = ConfigService.context.users.Where(x => x.Login == txtLogin.Text && x.Password == txtPassword.Text)
                    .FirstOrDefault();
                if (user != null)
                {
                    this.DialogResult = DialogResult.OK;
                }
                else 
                {
                    this.DialogResult = DialogResult.No;
                }
            }
            else 
            {
                MessageBox.Show("Заповніть усі поля!");
            }
        }
    }
}
