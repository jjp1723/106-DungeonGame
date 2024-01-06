using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Group4ExternalTool
{
    public partial class MapReader : Form
    {

        private int height;
        private int width;

        //The list is a public variable as it needs to be 
        public List<PictureBox> list = new List<PictureBox>();

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

        public MapReader()
        {
            InitializeComponent();
        }

        private void Reader_Load(object sender, EventArgs e)
        {
            for (int x = 0; x < (height * width); x++)
            {
                list.Add(new PictureBox());
            }

            int count = 0;

            for (int i = 0; i < height; i++)
            {
                list[count].Location = new System.Drawing.Point(10, (10 + 10 * i));

                for (int j = 0; j < width; j++)
                {
                    list[count].Size = new System.Drawing.Size(20, 20);
                    list[count].Location = new System.Drawing.Point((10 + 10 * j), (10 + 10 * i));

                    //Differenciating between load files with edited picture boxes and new files with default picture boxes
                    if (list[count].BackColor == DefaultBackColor)
                    {
                        list[count].BackColor = Color.Black;
                    }

                    list[count].Visible = true;
                    this.Controls.Add(this.list[count]);

                    count++;
                }
            }
        }
    }
}
