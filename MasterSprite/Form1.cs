using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MasterSprite
{
    public partial class mainFrm : Form
    {
        byte alpha = 255;
        public mainFrm()
        {
            InitializeComponent();
        }
        MainControll mainControll;
        PaintControl paintControl;
        ColorPicker colorPicker;
        LayerSwitch layerSwitch;
        Toolbar toolbar;

        private void mainFrm_Load(object sender, EventArgs e)
        {
            layerSwitch = new LayerSwitch(0, mainGraphics.Height - 32, 96, 32, 1);
            toolbar = new Toolbar(mainGraphics.Width - 24, 0, 24, 96);
            paintControl = new PaintControl(mainGraphics, layerSwitch, toolbar, Color.FromArgb(alpha, 100, 200, 100), 196, 196, 1, mainGraphics.Width / 2, mainGraphics.Height / 2);
            mainControll = new MainControll(mainGraphics, width, height, paintControl, openFile, saveFile);
            colorPicker = new ColorPicker(mainGraphics,0, 96, 96, 96, 31);
            colorPicker.AddColor(Color.Red);
            colorPicker.AddColor(Color.Orange);
            colorPicker.AddColor(Color.Yellow);
            colorPicker.AddColor(Color.Green);
            colorPicker.AddColor(Color.Blue);
            colorPicker.AddColor(Color.Purple);
            colorPicker.AddColor(Color.Black);
            colorPicker.AddColor(Color.White);
            paintControl.currentColor = colorPicker.GetSelectedColor();
            paintControl.PrimaryDraw();
            colorPicker.Draw();
            layerSwitch.Draw();
            toolbar.Draw();
            mainControll.Draw(this, colorPicker, layerSwitch, toolbar);
            mainTime.Enabled = true;
            timer1.Enabled = true;
        }

        private void mainGraphics_MouseMove(object sender, MouseEventArgs e)
        {
            mainControll.MousePos(e.X, e.Y);
            paintControl.MousePos(e.X, e.Y);
            colorPicker.MousePos(e.X, e.Y);
            layerSwitch.MousePos(e.X, e.Y);
            toolbar.MousePos(e.X, e.Y);
            paintControl.DrawCursuor();
        }

        private void mainTime_Tick(object sender, EventArgs e)
        {
            paintControl.DrawingCheck();
            paintControl.currentColor = colorPicker.GetSelectedColor();
            colorPicker.Draw();
            layerSwitch.Draw();
            toolbar.Draw();
            mainControll.Draw(this, colorPicker, layerSwitch, toolbar);
        }

        private void mainGraphics_MouseDown(object sender, MouseEventArgs e)
        {
            mainControll.MouseDown(true);
            paintControl.drawing = true;
            colorPicker.SelectColor(true);
            layerSwitch.mouseDown(true);
            toolbar.MouseClick(true);
        }

        private void mainGraphics_MouseUp(object sender, MouseEventArgs e)
        {
            mainControll.MouseDown(false);
            paintControl.drawing = false;
            colorPicker.SelectColor(false);
            layerSwitch.mouseDown(false);
            toolbar.MouseClick(false);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
           

            
        }
    }
}
