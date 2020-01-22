using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace M_2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        
        void button_click(Button buttons,string name)
        {
            pcnumber.Text = "";
            pcnumbers.Text += name;
        }

        void table()
        {

        }


        private void table_button_Click(object sender, EventArgs e) //테이블 묶기
        {   

            MessageBox.Show("좌석을 클릭해주세요.", "좌석선택");

        }

        private void room1_Click(object sender, EventArgs e)
        {
            button_click(room1, "1");
        }
    }
}
