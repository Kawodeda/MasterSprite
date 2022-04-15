using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace MasterSprite
{
    class LayerSwitch
    {
        int xPos,
            yPos,
            xSize,
            ySize,
            mouseX,
            mouseY,
            currentLayer;
        bool mouseOnLeftButton = false, mouseOnRightButton = false;
        Bitmap bmp;

        public LayerSwitch(int xPos, int yPos, int xSize, int ySize, int currentLayer)
        {
            this.xPos = xPos;
            this.yPos = yPos;
            this.xSize = xSize;
            this.ySize = ySize;
            this.currentLayer = currentLayer;
            bmp = new Bitmap(xSize, ySize);
        }

        public void Draw()
        {
            Graphics g = Graphics.FromImage(bmp);

            g.FillRectangle(new SolidBrush(Color.FromArgb(255, 255, 238, 217)), 0, 0, xSize, ySize);

            g.DrawString(currentLayer.ToString(), new Font(new FontFamily("Arial"), 16, FontStyle.Regular), new SolidBrush(Color.Black), 38, 4);
            if(mouseOnLeftButton)
            {
                g.DrawImageUnscaled(Properties.Resources.button_left_1, 8, 4);
            }
            else
            {
                g.DrawImageUnscaled(Properties.Resources.button_left_0, 8, 4);
            }

            if (mouseOnRightButton)
            {
                g.DrawImageUnscaled(Properties.Resources.button_right_1, 64, 4);
            }
            else
            {
                g.DrawImageUnscaled(Properties.Resources.button_right_0, 64, 4);
            }
        }

        public Bitmap GetBmp()
        {
            return bmp;
        }

        public Point GetDrawingPoint()
        {
            Point point = new Point(xPos, yPos);
            return point;
        }

        public int GetCurrentLayer()
        {
            return currentLayer;
        }

        public void MousePos(int x, int y)
        {
            mouseX = x;
            mouseY = y;

            if (mouseX - xPos > 8 && mouseX - xPos < 32 && mouseY - yPos > 4 && mouseY - yPos < 28)
            {
                mouseOnLeftButton = true;
            }
            else
            {
                mouseOnLeftButton = false;
            }

            if (mouseX - xPos > 64 && mouseX - xPos < 88 && mouseY - yPos > 4 && mouseY - yPos < 28)
            {
                mouseOnRightButton = true;
            }
            else
            {
                mouseOnRightButton = false;
            }
        }

        public void mouseDown(bool mouseDown)
        {
            if(mouseDown)
            {
                if (mouseX - xPos > 8 && mouseX - xPos < 32 && mouseY - yPos > 4 && mouseY - yPos < 28)
                {
                    currentLayer--;
                }

                if (mouseX - xPos > 64 && mouseX - xPos < 88 && mouseY - yPos > 4 && mouseY - yPos < 28)
                {
                    currentLayer++;
                }
            }
        }

    }
}
