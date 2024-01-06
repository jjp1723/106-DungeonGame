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
    public partial class Form1 : Form
    {

        public Form1()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.InitialDirectory = @"C:\";
            dialog.RestoreDirectory = true;
            dialog.Title = "Open a Level File";
            dialog.DefaultExt = "Level Files| *.level";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                FileStream inStream = null;
                StreamReader input = null;

                inStream = File.OpenRead(dialog.FileName);
                input = new StreamReader(inStream);

                MapReader reader = new MapReader();

                reader.Wid = int.Parse(input.ReadLine());
                reader.Hei = int.Parse(input.ReadLine());

                //Readjusting the size of the form
                if (reader.Wid < 11)
                {
                    reader.Width = 430;
                }
                else if (reader.Wid < 21)
                {
                    reader.Width = 630;
                }
                else
                {
                    reader.Width = 830;
                }

                for (int x = 0; x < (reader.Hei * reader.Wid); x++)
                {
                    reader.list.Add(new PictureBox());
                }

                int count = 0;

                for (int i = 0; i < reader.Hei; i++)
                {
                    reader.list[count].Location = new System.Drawing.Point(20, (20 + 20 * i));

                    for (int j = 0; j < reader.Wid; j++)
                    {
                        reader.list[count].Size = new System.Drawing.Size(20, 20);
                        reader.list[count].Location = new System.Drawing.Point((20 + 20 * j), (20 + 20 * i));
                        reader.list[count].BackColor = ColorTranslator.FromHtml(input.ReadLine());
                        reader.list[count].Visible = true;
                        this.Controls.Add(reader.list[count]);

                        count++;
                    }
                }

                

               // reader.ShowDialog();
            }
        }
    }
}
