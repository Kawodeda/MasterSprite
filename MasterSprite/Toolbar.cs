using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace MasterSprite
{
    class Toolbar
    {
        int xPos,
           yPos,
           xSize,
           ySize,
           mouseX,
           mouseY,
           currentTool = 0; // 0 - pencil ; 1 - earser ; 2 - marquee
        bool mouseOnButtonPencil = false, mouseOnButtonEarser = false, mouseOnButtonMarquee = false;
        Bitmap bmp;

        public Toolbar(int xPos, int yPos, int xSize, int ySize)
        {
            this.xPos = xPos;
            this.yPos = yPos;
            this.xSize = xSize;
            this.ySize = ySize;
            bmp = new Bitmap(xSize, ySize);
        }

        public void Draw()
        {
            bmp = new Bitmap(xSize, ySize);
            Graphics g = Graphics.FromImage(bmp);

            g.FillRectangle(new SolidBrush(Color.FromArgb(255, 255, 238, 217)), 0, 0, xSize, ySize);
            if(mouseOnButtonPencil || currentTool == 0)
            {
                g.DrawImageUnscaled(Properties.Resources.button_pencil_1, 0, 0);
            }
            else
            {
                g.DrawImageUnscaled(Properties.Resources.button_pencil_0, 0, 0);
            }

            if (mouseOnButtonEarser || currentTool == 1)
            {
                g.DrawImageUnscaled(Properties.Resources.button_earser_1, 0, 25);
            }
            else
            {
                g.DrawImageUnscaled(Properties.Resources.button_earser_0, 0, 25);
            }

            if (mouseOnButtonMarquee || currentTool == 2)
            {
                g.DrawImageUnscaled(Properties.Resources.button_marquee_1, 0, 50);
            }
            else
            {
                g.DrawImageUnscaled(Properties.Resources.button_marquee_0, 0, 50);
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

        public int GetCurrentTool()
        {
            return currentTool;
        }

        public void MouseClick(bool mouseDown)
        {
            if(mouseDown)
            {
                if (mouseX - xPos > 0 && mouseX - xPos < 24 && mouseY - yPos > 0 && mouseY - yPos < 24)
                {
                    currentTool = 0;
                }
                else if (mouseX - xPos > 0 && mouseX - xPos < 24 && mouseY - yPos > 25 && mouseY - yPos < 49)
                {
                    currentTool = 1;
                }
            }
        }
        public void MousePos(int x, int y)
        {
            mouseX = x;
            mouseY = y;

            if(mouseX - xPos > 0 && mouseX - xPos < 24 && mouseY - yPos > 0 && mouseY - yPos < 24)
            {
                mouseOnButtonPencil = true;
            }
            else
            {
                mouseOnButtonPencil = false;
            }

            if (mouseX - xPos > 0 && mouseX - xPos < 24 && mouseY - yPos > 25 && mouseY - yPos < 49)
            {
                mouseOnButtonEarser = true;
            }
            else
            {
                mouseOnButtonEarser = false;
            }

            if (mouseX - xPos > 0 && mouseX - xPos < 24 && mouseY - yPos > 50 && mouseY - yPos < 74)
            {
                mouseOnButtonMarquee = true;
            }
            else
            {
                mouseOnButtonMarquee = false;
            }
        }
    }
}
