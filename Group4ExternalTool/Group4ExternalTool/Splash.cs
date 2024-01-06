using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Group4ExternalTool
{
    public partial class Splash : Form
    {
        public int width;
        public int height;
        public Splash()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.ShowDialog();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Checking to make sure that the height and width are within specified range
            width = int.Parse(textBox1.Text);
            if (width > 30 || width < 10)
            {
                System.Windows.Forms.MessageBox.Show("Please keep tile count between 10 and 30", "Error! Out of Range",
                    System.Windows.Forms.MessageBoxButtons.OK,
                    System.Windows.Forms.MessageBoxIcon.Error);
            }

            height = int.Parse(textBox2.Text);
            if (height > 30 || height < 10)
            {
                System.Windows.Forms.MessageBox.Show("Please keep tile count between 10 and 30", "Error! Out of Range",
                    System.Windows.Forms.MessageBoxButtons.OK,
                    System.Windows.Forms.MessageBoxIcon.Error);
            }

            Editor editor = new Editor();
            editor.Wid = width;
            editor.Hei = height;
            editor.Height = 680;

            //Adjusting the width of the new form depending on user input
            if (width < 11)
            {
                editor.Width = 430;
            }
            else if (width < 21)
            {
                editor.Width = 630;
            }
            else
            {
                editor.Width = 830;
            }

            //Directing the user the the new form while closing the current one
            editor.ShowDialog();
            this.Close();
        
        }
    }
}
