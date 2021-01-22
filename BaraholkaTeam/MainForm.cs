using BaraholkaTeam.DAL.ContextData;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BaraholkaTeam
{
    public partial class MainForm : Form
    {
        MyContext context { set; get; }
        public MainForm()
        {
            InitializeComponent();
        }
    }
}
