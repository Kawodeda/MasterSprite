using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace MasterSprite
{
    class PaintControl
    {
        #region Variabels
        PictureBox mainPb;
        public Color currentColor;
        LayerSwitch layerSwitch;
        Toolbar toolbar;
        Color[,,] layers;
        public int xPos, yPos, layerCount = 3, currentLayer, cursorLayer, brushWide = 8, currentTool;
        string brushShape = "square", background = "trans";
        public Bitmap bmp;
        public Point mousePos = new Point();
        public int? mousePreX = null, mousePreY = null;
        public bool drawing = false, active = true;
        
        #endregion
        #region Constructor
        public PaintControl(PictureBox mainPb, LayerSwitch layerSwitch, Toolbar toolbar, Color color, int fileXSize, int fileYsize, int xPos, int yPos)
        {
            currentColor = color;
            this.mainPb = mainPb;
            this.xPos = xPos;
            this.yPos = yPos;
            bmp = new Bitmap(fileXSize, fileYsize);
            layers = new Color [layerCount + 1, fileXSize, fileYsize];
            cursorLayer = layers.GetLength(0) - 1;
            this.layerSwitch = layerSwitch;
            this.toolbar = toolbar;
            for (byte y = 0; y < layerCount; y++)
            {
                for (int x = 0; x < fileXSize; x++)
                {
                    for (int z = 0; z < fileYsize; z++)
                    {
                        switch (background)
                        {
                            case "trans":
                                if (x % 16 == 0 && z % 16 == 0 && y == 0)
                                    layers[y, x, z] = Color.FromArgb(255, 191, 191, 191);
                                if (x % 16 != 0 && z % 16 != 0 && y == 0)
                                    layers[y, x, z] = Color.FromArgb(255, 128, 128, 128);
                                break;
                        }
                    }
                }
            }
        }

        public PaintControl(PictureBox mainPb, LayerSwitch layerSwitch, Toolbar toolbar, Color color, int fileXSize, int fileYsize, int currentLayer, int xPos, int yPos)
        {
            this.xPos = xPos;
            this.yPos = yPos;
            currentColor = color;
            this.mainPb = mainPb;
            bmp = new Bitmap(fileXSize, fileYsize);
            layers = new Color[layerCount + 1, fileXSize, fileYsize];
            this.currentLayer = currentLayer;
            cursorLayer = layers.GetLength(0) - 1;
            this.layerSwitch = layerSwitch;
            this.toolbar = toolbar;
            for (byte y = 0; y < layerCount; y++)
            {
                for (int x = 0; x < fileXSize; x++)
                {
                    for (int z = 0; z < fileYsize; z++)
                    {
                        switch (background)
                        {
                            case "trans":
                                if (x % 16 == 0 && z % 16 == 0 && y == 0)
                                    layers[y, x, z] = Color.FromArgb(255, 191, 191, 191);
                                if (x % 16 != 0 && z % 16 != 0 && y == 0)
                                    layers[y, x, z] = Color.FromArgb(255, 128, 128, 128);
                                break;
                        }
                    }
                }
            }
        }

        #endregion
        #region Functions
        public void PrimaryDraw()
        {
            currentLayer = layerSwitch.GetCurrentLayer();
            if (layers.GetLength(1) * layers.GetLength(2) >= 39000)
            {
                MessageBox.Show("АХТУНГ!!!!! \nYour image size is too big. Lags are possible!!", "АХТУНГ!!!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            //Graphics g = Graphics.FromImage(bmp);
            //Draw all layers successively
            for(byte y = 0; y < layerCount; y++)
            {
                for (int x = 0; x < layers.GetLength(1); x++)
                {
                    for (int z = 0; z < layers.GetLength(2); z++)
                    {
                        //SolidBrush b = new SolidBrush(layers[y, x, z]);
                        //g.FillRectangle(b, x, z, 1, 1);
                        bmp.SetPixel(x, z, layers[currentLayer, x, z]);
                    }
                }
            }

           
        }

        private Color recurseFunc(int y, int x, int z)
        {
            if (layers[y, x, z] == Color.Empty && y != 0)
            {
                return recurseFunc(y - 1, x, z);
            }
            else
            {
                return layers[y, x, z];
            }

        }

        public void layersComputation()
        {
            int visibleLayer = layers.GetLength(0) - 1;
            int layerBelowVisible = layerCount - 1;

            for(int x = 0; x < layers.GetLength(1); x++)
            {
                for (int z = 0; z < layers.GetLength(2); z++)
                {
                        layers[visibleLayer, x, z] = recurseFunc(layerCount - 1, x, z); 
                }
            }

        }

        public void Draw()
        {
            int visibleLayer = layers.GetLength(0) - 1;
            for (int x = 0; x < layers.GetLength(1); x++)
            {
                for (int z = 0; z < layers.GetLength(2); z++)
                {
                    bmp.SetPixel(x, z, layers[visibleLayer, x, z]);
                }
            }
        }

        public Bitmap Getbmp()
        {
            return bmp;
        }
        public Point GetDrawingPoint()
        {
            Point point = new Point(xPos, yPos);
            return point;
        }

        public void DrawCursuor()
        {
            
        }

        public void DrawingCheck()
        {
            currentTool = toolbar.GetCurrentTool();
            currentLayer = layerSwitch.GetCurrentLayer();

            if(active)
                if(drawing)
                {
                    switch(currentTool)
                {
                    case 0:
                        line(mousePreX ?? mousePos.X, mousePreY ?? mousePos.Y, mousePos.X, mousePos.Y, currentColor, brushWide);
                        break;
                    case 1:
                        line(mousePreX ?? mousePos.X, mousePreY ?? mousePos.Y, mousePos.X, mousePos.Y, Color.Empty, brushWide);
                        break;
                }
                mousePreX = mousePos.X;
                mousePreY = mousePos.Y;
                layersComputation();
                Draw();
            }
                else
                {
                    mousePreX = null;
                    mousePreY = null;
                }
        }

        public void MousePos(int X, int Y)
        {
            mousePos.X = X;
            mousePos.Y = Y;
        }

        public void NewFile(int width, int height, string background)
        {
            bmp = new Bitmap(width, height);
            layers = new Color[layerCount + 1, width, height];
            for (byte y = 0; y < layerCount; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    for (int z = 0; z < height; z++)
                    {
                        switch(background)
                        {
                            case "trans":
                                if (x % 16 == 0 && z % 16 == 0 && y == 0)
                                    layers[y, x, z] = Color.FromArgb(255, 191, 191, 191);
                                if (x % 16 != 0 && z % 16 != 0 && y == 0)
                                    layers[y, x, z] = Color.FromArgb(255, 128, 128, 128);
                                break;
                        }
                    }
                }
            }
        }

        public void OpenFile(Bitmap file)
        {
            bmp = new Bitmap(file.Width, file.Height);
            layers = new Color[layerCount + 1, file.Width, file.Height];
            for (byte y = 0; y < 2; y++)
            {
                for (int x = 0; x < file.Width; x++)
                {
                    for (int z = 0; z < file.Height; z++)
                    {
                        switch (background)
                        {
                            case "trans":
                                if (x % 16 == 0 && z % 16 == 0 && y == 0)
                                    layers[y, x, z] = Color.FromArgb(255, 191, 191, 191);
                                if (x % 16 != 0 && z % 16 != 0 && y == 0)
                                    layers[y, x, z] = Color.FromArgb(255, 128, 128, 128);
                                break;
                        }
                        if(y != 0)
                        layers[y, x, z] = file.GetPixel(x, z);
                    }
                }
            }
        }

        public void SaveFile(Bitmap file)
        {
            file = new Bitmap(layers.GetLength(1), layers.GetLength(2));
            if(background == "trans")
            for(int x = 0; x < layers.GetLength(1); x++)
                {
                    for(int z = 0; z < layers.GetLength(2); z++)
                    {
                        file.SetPixel(x, z, layers[layers.GetLength(0) - 1, x, z]);
                    }
                }
        }

        private void line(int x, int y, int x2, int y2, Color color, int wide)
        {
            int w = x2 - x;
            int h = y2 - y;
            int dx1 = 0, dy1 = 0, dx2 = 0, dy2 = 0;
            if (w < 0) dx1 = -1; else if (w > 0) dx1 = 1;
            if (h < 0) dy1 = -1; else if (h > 0) dy1 = 1;
            if (w < 0) dx2 = -1; else if (w > 0) dx2 = 1;
            int longest = Math.Abs(w);
            int shortest = Math.Abs(h);
            if (!(longest > shortest))
            {
                longest = Math.Abs(h);
                shortest = Math.Abs(w);
                if (h < 0) dy2 = -1; else if (h > 0) dy2 = 1;
                dx2 = 0;
            }
            int numerator = longest >> 1;
            for (int i = 0; i <= longest; i++)
            {
                if(brushShape == "square")
                {
                    for (int j = 0; j < wide; j++)
                    {
                        for (int k = 0; k < wide; k++)
                        {
                            if(x + j < layers.GetLength(1) + xPos && y + k < layers.GetLength(2) + yPos && x+j > xPos && y + k > yPos)
                            layers[currentLayer, x + j - xPos, y + k - yPos] = color;
                        }
                    }
                }
                else if (brushShape == "round")
                {
                    if (x + wide < layers.GetLength(1) && y + wide < layers.GetLength(2) && x + wide >= 0 && y + wide >= 0)
                        FilledCircle(x, y, color, wide / 2);
                }
               
                numerator += shortest;
                if (!(numerator < longest))
                {
                    numerator -= longest;
                    x += dx1;
                    y += dy1;
                }
                else
                {
                    x += dx2;
                    y += dy2;
                }
            }
        }

        private void FilledCircle(int x1, int y1, Color color, int radius)
        {
            int _x = x1, _y = y1;
            bool[,] ar = new bool[radius * 2 + 4, radius * 2 + 4];
            int x = 0, y = radius, gap = 0, delta = (2 - 2 * radius);
            while (y >= 0)
            {
                layers[currentLayer, _x + x, _y + y] = color;
                ar[x + radius + 2, y + radius + 2] = true;
                layers[currentLayer, _x + x, _y - y] = color;
                ar[x + radius + 2, -y + radius + 2] = true;
                layers[currentLayer, _x - x, _y - y] = color;
                ar[-x + radius + 2, -y + radius + 2] = true;
                layers[currentLayer, _x - x, _y + y] = color;
                ar[-x + radius + 2, y + radius + 2] = true;
                gap = 2 * (delta + y) - 1;
                if (delta < 0 && gap <= 0)
                {
                    x++;
                    delta += 2 * x + 1;
                    continue;
                }
                if (delta > 0 && gap > 0)
                {
                    y--;
                    delta -= 2 * y + 1;
                    continue;
                }
                x++;
                delta += 2 * (x - y);
                y--;
            }
            for (int i = 0; i < ar.GetLength(0); i++)
            {
                bool flag = false;
                for (int j = 0; j < ar.GetLength(1); j++)
                {
                    if (ar[i, j] == true)
                    {
                        //bmp.SetPixel(80 + i, 80 + j, Color.Purple);
                        if (j + 1 < ar.GetLength(1))
                        {
                            if (ar[i, j + 1] == false)
                            {
                                ar[i, j + 1] = true;
                            }
                            else if (ar[i, j + 2] == false)
                            {
                                if (flag)
                                {
                                    flag = false;
                                    break;
                                }
                                else
                                {
                                    flag = true;
                                }
                            }
                        }
                    }
                }
            }
        }
        #endregion


    }
}
