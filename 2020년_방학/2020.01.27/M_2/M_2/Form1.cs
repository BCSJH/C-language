using System;
using System.Collections;
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
        List<string> table_name = new List<string>();

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

            if (table_button.BackColor == SystemColors.Control) // 클릭이 안됐을 경우
            {
                table_button.BackColor = SystemColors.ControlDark;
                MessageBox.Show("좌석을 클릭해주세요.", "좌석선택");//메세지 출력
            }

            else //클릭을 했을 경우
            {
                table_button.BackColor = SystemColors.Control;
                // 색깔 바꾸고 테이블 테두리 만들기
                // 이미 테두리에 있는 테이블이면 포함 안되게 예외처리

            }
        }

        private void room1_Click(object sender, EventArgs e)
        {
            button_click(room1, "1");
        }
    }
}
