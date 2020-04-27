using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.IO;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        Bin _bin = new Bin();
        bool loaded = false;
        View view = new View();
        int currentLayer;
        int func_min;
        int func_width;
        int FrameCount;
        DateTime NextFPSUpdate = DateTime.Now.AddSeconds(1);
        bool needReload = false;
        int check;

        void displayFPS()
        {
            if (DateTime.Now >= NextFPSUpdate)
            {
                this.Text = String.Format("CT Visualizer (fps={0})", FrameCount);
                NextFPSUpdate = DateTime.Now.AddSeconds(1);
                FrameCount = 0;
            }
            FrameCount++;
        }

        public Form1()
        {
            InitializeComponent();
        }

        void Application_Idle(object sender, EventArgs e)
        {
            while (glControl1.IsIdle)
            {
                displayFPS();
                glControl1.Invalidate();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Application.Idle += Application_Idle;
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string str = dialog.FileName;
                _bin.readBIN(str);
                view.SetupView(glControl1.Width, glControl1.Height);
                loaded = true;
                glControl1.Invalidate();
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            currentLayer = trackBar1.Value;
            needReload = true;
        }

        private void glControl1_Paint(object sender, PaintEventArgs e)
        {
            if (loaded)
            {
                if (check == 2)
                {
                    view.DrawQuads(currentLayer, func_min, func_width);
                    glControl1.SwapBuffers();
                }
                if (check == 3)
                {
                    view.DrawQuadStrip(currentLayer, func_min, func_width);
                    glControl1.SwapBuffers();
                }
                else
                {
                    if (needReload)
                    {
                        view.generateTextureImage(currentLayer, func_min, func_width);
                        view.Load2DTexture();
                        needReload = false;
                    }
                    //view.DrawQuads(currentLayer);
                    view.DrawTexture();
                    glControl1.SwapBuffers();
                }
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            check = 1;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            check = 2;
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            func_min = trackBar2.Value;
            needReload = true;
        }

        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            func_width = trackBar3.Value;
            needReload = true;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            check = 3;
        }
    }
}
