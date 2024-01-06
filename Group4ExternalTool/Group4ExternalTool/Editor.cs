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
    public partial class Editor : Form
    {
        public int height;
        public int width;
        public List<PictureBox> list = new List<PictureBox>();
        Color color;

        //Creating set properties to use values from the first form
        public int Hei
        {
            get { return height; }
            set { height = value; }
        }

        public int Wid
        {
            get { return width; }
            set { width = value; }
        }

        public Editor()
        {
            InitializeComponent();
        }

        private void Editor_Load(object sender, EventArgs e)
        {
            for (int x = 0; x < (height * width); x++)
            {
                list.Add(new PictureBox());
            }

            int count = 0;

            for (int i = 0; i < height; i++)
            {
                list[count].Location = new System.Drawing.Point(176, (23 + 20 * i));

                for (int j = 0; j < width; j++)
                {
                    list[count].Size = new System.Drawing.Size(20, 20);
                    list[count].Location = new System.Drawing.Point((176 + 20 * j), (23 + 20 * i));

                    //Differenciating between load files with edited picture boxes and new files with default picture boxes
                    if (list[count].BackColor == DefaultBackColor)
                    {
                        list[count].BackColor = Color.Black;
                    }

                    list[count].Visible = true;
                    this.Controls.Add(this.list[count]);
                    list[count].Click += new EventHandler(PictureBox_Click);

                    count++;
                }
            }

            private void PictureBox_Click(Object sender, System.EventArgs e)
            {
                PictureBox p = (PictureBox)sender;
                p.BackColor = color;
            }
        }
    }
}
