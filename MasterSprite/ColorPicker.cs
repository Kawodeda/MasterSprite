using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace MasterSprite
{
    class ColorPicker
    {
        int xPos, yPos, cpXPos, cpYPos, xSize, ySize, mouseX, mouseY, arrowRX, arrowRY, arrowGX, arrowGY, arrowBX, arrowBY, arrowHX, arrowHY, arrowSX, arrowSY, arrowVX, arrowVY, Length, colSize = 16, selectedColor = 0;
        bool colorPickerOpen = false, arrowRCatched = false, arrowGCatched = false, arrowBCatched = false, arrowHCatched = false, arrowSCatched = false, arrowVCatched = false, mouseOnOkButt = false, mouseOnRGBButt = false, mouseOnHSVButt = false;
        int currentHue, currentSat, currentVal;
        int H, S, V;
        string colPickFormat = "RGB";
        PictureBox mainPb;
        Color[] colors;
        Color newColor;
        Bitmap bmp1, bmp2;

        public ColorPicker(PictureBox pictureBox, int xPos, int yPos, int xSize, int ySize, int maxColorCount)
        {
            this.xPos = xPos;
            this.yPos = yPos;
            this.xSize = xSize;
            this.ySize = ySize;
            mainPb = pictureBox;
            bmp1 = new Bitmap(xSize, ySize);
            colors = new Color[maxColorCount];
            for (int i = 0; i < colors.Length; i++)
            {
                colors[i] = Color.Empty;
            }
            newColor = Color.FromArgb(255, 204, 2, 123);
            RGBtoHSV(newColor.R, newColor.G, newColor.B, out currentHue, out currentSat, out currentVal, newColor);
            S = currentSat;
            V = currentVal;
            H = currentHue;
        }

        public void Draw()
        {
            int size = colSize;
            int filledSpace = 0;
            int row = 0;
            int row2 = 0;
            int filledSpace2 = 0;
            int limit2 = 0;
            Length = 0;
            int limit;
            Graphics g = Graphics.FromImage(bmp1);
            g.FillRectangle(new SolidBrush(Color.FromArgb(255, 255, 238, 217)), 0, 0, xSize, ySize);

            for (int i = 0; i < colors.Length; i++)
            {
                if (colors[i] != Color.Empty)
                    Length++;
            }

            for (int i = 0; i < colors.Length; i++)
            {
                limit = (filledSpace + size);
                if (!(limit > xSize))
                {
                    g.FillRectangle(new SolidBrush(colors[i]), filledSpace, row * size, size, size);
                    if ((i < colors.Length) && colors[i] != Color.Empty)
                    {
                        g.DrawRectangle(new Pen(Color.Gray), filledSpace, row * size, size, size);


                    }
                    filledSpace += size;

                    limit2 = filledSpace2 + size * 2;

                    if (i == Length - 1)
                    {
                        if (filledSpace < xSize)
                        {
                            g.DrawLine(new Pen(Color.Red), filledSpace + 4, row * size + 8, filledSpace + 12, row * size + 8);
                            g.DrawLine(new Pen(Color.Red), filledSpace + 8, row * size + 4, filledSpace + 8, row * size + 12);
                        }
                        else
                        {
                            g.DrawLine(new Pen(Color.Red), 4, (row + 1) * size + 8, 12, (row + 1) * size + 8);
                            g.DrawLine(new Pen(Color.Red), 8, (row + 1) * size + 4, 8, (row + 1) * size + 12);
                        }
                    }


                }
                else
                {
                    row++;
                    filledSpace = 0;
                    i--;
                    continue;
                }
            }

            //selected color
            filledSpace = 0;
            row = 0;

            for (int i = 0; i < colors.Length; i++)
            {
                limit = (filledSpace + size);
                if (!(limit > xSize))
                {
                    if (i == selectedColor)
                    {
                        g.DrawRectangle(new Pen(Color.Red), filledSpace, row * size, size, size);


                    }
                    filledSpace += size;
                }
                else
                {
                    row++;
                    filledSpace = 0;
                    i--;
                    continue;
                }
            }
            //

            // The true ColorPicker
            if (colorPickerOpen)
            {
                cpXPos = xPos + 100;
                cpYPos = yPos;
                bmp2 = new Bitmap(412, 128);
                Graphics g2 = Graphics.FromImage(bmp2);

                g2.FillRectangle(new SolidBrush(Color.FromArgb(255, 255, 238, 217)), 0, 0, 412, 128);
                if (mouseOnHSVButt || colPickFormat == "HSV")
                {
                    g2.DrawImageUnscaled(Properties.Resources.button_HSV_1, 64, 4);
                }
                else
                {
                    g2.DrawImageUnscaled(Properties.Resources.button_HSV_0, 64, 4);
                }

                if (mouseOnRGBButt || colPickFormat == "RGB")
                {
                    g2.DrawImageUnscaled(Properties.Resources.button_RGB_1, 8, 4);
                }
                else
                {
                    g2.DrawImageUnscaled(Properties.Resources.button_RGB_0, 8, 4);
                }

                if (!mouseOnOkButt)
                {
                    g2.DrawImageUnscaled(Properties.Resources.button_OK_0, 232, 76);
                }
                else
                {
                    g2.DrawImageUnscaled(Properties.Resources.button_OK_1, 232, 76);
                }

                g2.DrawString(ColorTranslator.ToHtml(newColor), new Font(new FontFamily("Arial"), 12, FontStyle.Regular), new SolidBrush(Color.Black), 196, 4);
                g2.FillRectangle(new SolidBrush(newColor), 136, 4, 48, 16);
                if (colPickFormat == "RGB")
                {
                    if (!arrowRCatched)
                        arrowRX = (int)Math.Round(21 + newColor.R * 1.4f);
                    if (!arrowGCatched)
                        arrowGX = (int)Math.Round(21 + newColor.G * 1.4f);
                    if (!arrowBCatched)
                        arrowBX = (int)Math.Round(21 + newColor.B * 1.4f);
                    arrowRY = 27;
                    arrowGY = 43;
                    arrowBY = 59;

                    g2.DrawString("R", new Font(new FontFamily("Arial"), 10, FontStyle.Regular), new SolidBrush(Color.Black), 8, 28);
                    g2.DrawString("G", new Font(new FontFamily("Arial"), 10, FontStyle.Regular), new SolidBrush(Color.Black), 8, 44);
                    g2.DrawString("B", new Font(new FontFamily("Arial"), 10, FontStyle.Regular), new SolidBrush(Color.Black), 8, 60);

                    g2.DrawString(newColor.R.ToString(), new Font(new FontFamily("Arial"), 8, FontStyle.Regular), new SolidBrush(Color.Black), 388, 28);
                    g2.DrawString(newColor.G.ToString(), new Font(new FontFamily("Arial"), 8, FontStyle.Regular), new SolidBrush(Color.Black), 388, 44);
                    g2.DrawString(newColor.B.ToString(), new Font(new FontFamily("Arial"), 8, FontStyle.Regular), new SolidBrush(Color.Black), 388, 60);


                    g2.DrawImageUnscaled(Properties.Resources.arrow, arrowRX, arrowRY);
                    g2.DrawImageUnscaled(Properties.Resources.arrow, arrowGX, arrowGY);
                    g2.DrawImageUnscaled(Properties.Resources.arrow, arrowBX, arrowBY);

                    for (int i = 0; i < 256; i++)
                    {
                        g2.FillRectangle(new SolidBrush(Color.FromArgb(255, i, newColor.G, newColor.B)), 24 + i * 1.4f, 32, 1.4f, 8);
                    }
                    for (int i = 0; i < 256; i++)
                    {
                        g2.FillRectangle(new SolidBrush(Color.FromArgb(255, newColor.R, i, newColor.B)), 24 + i * 1.4f, 48, 1.4f, 8);
                    }
                    for (int i = 0; i < 256; i++)
                    {
                        g2.FillRectangle(new SolidBrush(Color.FromArgb(255, newColor.R, newColor.G, i)), 24 + i * 1.4f, 64, 1.4f, 8);
                    }
                }
                else if (colPickFormat == "HSV")
                {
                    H = currentHue;
                    S = currentSat;
                    V = currentVal;

                    if (!arrowHCatched)
                        arrowHX = 21 + H;
                    if (!arrowSCatched)
                        arrowSX = (int)Math.Round(21 + S * 3.6f);
                    if (!arrowVCatched)
                        arrowVX = (int)Math.Round(21 + V * 3.6f);
                    arrowHY = 27;
                    arrowSY = 43;
                    arrowVY = 59;

                    g2.DrawString("H", new Font(new FontFamily("Arial"), 10, FontStyle.Regular), new SolidBrush(Color.Black), 8, 28);
                    g2.DrawString("S", new Font(new FontFamily("Arial"), 10, FontStyle.Regular), new SolidBrush(Color.Black), 8, 44);
                    g2.DrawString("V", new Font(new FontFamily("Arial"), 10, FontStyle.Regular), new SolidBrush(Color.Black), 8, 60);


                    g2.DrawString(H.ToString(), new Font(new FontFamily("Arial"), 8, FontStyle.Regular), new SolidBrush(Color.Black), 388, 28);
                    g2.DrawString(S.ToString(), new Font(new FontFamily("Arial"), 8, FontStyle.Regular), new SolidBrush(Color.Black), 388, 44);
                    g2.DrawString(V.ToString(), new Font(new FontFamily("Arial"), 8, FontStyle.Regular), new SolidBrush(Color.Black), 388, 60);

                    g2.DrawImageUnscaled(Properties.Resources.arrow, arrowHX, arrowHY);
                    g2.DrawImageUnscaled(Properties.Resources.arrow, arrowSX, arrowSY);
                    g2.DrawImageUnscaled(Properties.Resources.arrow, arrowVX, arrowVY);

                    for (int i = 0; i < 360; i++)
                    {
                        int[] rgb = new int[3];

                        HsvToRgb(i, currentSat, currentVal, out rgb[0], out rgb[1], out rgb[2]);
                        g2.FillRectangle(new SolidBrush(Color.FromArgb(255, rgb[0], rgb[1], rgb[2])), 24 + i, 32, 1, 8);
                    }
                    for (int i = 0; i < 100; i++)
                    {
                        int[] rgb = new int[3];

                        HsvToRgb(currentHue, i, currentVal, out rgb[0], out rgb[1], out rgb[2]);
                        g2.FillRectangle(new SolidBrush(Color.FromArgb(255, rgb[0], rgb[1], rgb[2])), 24 + i * 3.6f, 48, 3.6f, 8);
                    }
                    for (int i = 0; i < 100; i++)
                    {
                        int[] rgb = new int[3];

                        HsvToRgb(currentHue, currentSat, i, out rgb[0], out rgb[1], out rgb[2]);
                        g2.FillRectangle(new SolidBrush(Color.FromArgb(255, rgb[0], rgb[1], rgb[2])), 24 + i * 3.6f, 64, 3.6f, 8);
                    }

                }

                if (mouseX > 232 + cpXPos && mouseX < 280 + cpXPos && mouseY > 76 + cpYPos && mouseY < 96 + cpYPos)
                {
                    mouseOnOkButt = true;
                }
                else
                {
                    mouseOnOkButt = false;
                }

                if (mouseX > 8 + cpXPos && mouseX < 56 + cpXPos && mouseY > 4 + cpYPos && mouseY < 20 + cpYPos)
                {
                    mouseOnRGBButt = true;
                }
                else
                {
                    mouseOnRGBButt = false;
                }

                if (mouseX > 64 + cpXPos && mouseX < 102 + cpXPos && mouseY > 4 + cpYPos && mouseY < 20 + cpYPos)
                {
                    mouseOnHSVButt = true;
                }
                else
                {
                    mouseOnHSVButt = false;
                }

            }
            else
            {
                bmp2 = null;
            }



            //
        }

        public Bitmap Getbmp()
        {
            return bmp1;

        }

        public Bitmap Getbmp2()
        {
            return bmp2;

        }
        public Point GetDrawingPoint()
        {
            Point point = new Point(xPos, yPos);
            return point;
        }

        public Point GetDrawingPoint2()
        {
            Point point = new Point(cpXPos, cpYPos);
            return point;
        }
        public Color GetSelectedColor()
        {
            Color color = colors[selectedColor];
            return color;
        }

        public void MousePos(int x, int y)
        {
            mouseX = x;
            mouseY = y;

            switch (colPickFormat)
            {
                case "RGB":
                    if (arrowRCatched && mouseX - cpXPos >= 21 && mouseX - cpXPos <= 276 * 1.4f - 7)
                    {
                        arrowRX = mouseX - cpXPos;
                        if (Math.Round(arrowRX / 1.4f) % 1 == 0 && Math.Round((arrowRX - 21) / 1.4f) < 256)
                            newColor = Color.FromArgb(255, (int)Math.Round((arrowRX - 21) / 1.4f), newColor.G, newColor.B);
                    }

                    if (arrowGCatched && mouseX - cpXPos >= 21 && mouseX - cpXPos <= 276 * 1.4f - 7)
                    {
                        arrowGX = mouseX - cpXPos;
                        if (Math.Round(arrowGX / 1.4f) % 1 == 0 && Math.Round((arrowGX - 21) / 1.4f) < 256)
                            newColor = Color.FromArgb(255, newColor.R, (int)Math.Round((arrowGX - 21) / 1.4f), newColor.B);
                    }

                    if (arrowBCatched && mouseX - cpXPos >= 21 && mouseX - cpXPos <= 276 * 1.4f - 7)
                    {
                        arrowBX = mouseX - cpXPos;
                        if (Math.Round(arrowBX / 1.4f) % 1 == 0 && Math.Round((arrowBX - 21) / 1.4f) < 256)
                            newColor = Color.FromArgb(255, newColor.R, newColor.G, (int)Math.Round((arrowBX - 21) / 1.4f));
                    }

                    break;

                case "HSV":
                    int r, g, b;

                    if (arrowHCatched && mouseX - cpXPos >= 21 && mouseX - cpXPos <= 384 - 5)
                    {
                        arrowHX = mouseX - cpXPos;
                        if (arrowHX % 1 == 0 && arrowHX - 21 < 360)
                        {
                            HsvToRgb(arrowHX - 21, currentSat, currentVal, out r, out g, out b);
                            currentHue = arrowHX - 21;
                            newColor = Color.FromArgb(255, r, g, b);
                        }
                    }

                    if (arrowSCatched && mouseX - cpXPos >= 21 && mouseX - cpXPos <= 384 - 5)
                    {
                        arrowSX = mouseX - cpXPos;
                        if (Math.Round(arrowSX / 3.6f) % 1 == 0 && Math.Round((arrowSX - 21) / 3.6f) < 360)
                        {
                            HsvToRgb(currentHue, (int)Math.Round((arrowSX - 21) / 3.6f), currentVal, out r, out g, out b);
                            currentSat = (int)Math.Round((arrowSX - 21) / 3.6f);
                            newColor = Color.FromArgb(255, r, g, b);
                        }
                    }

                    if (arrowVCatched && mouseX - cpXPos >= 21 && mouseX - cpXPos <= 384 - 5)
                    {
                        arrowVX = mouseX - cpXPos;
                        if (Math.Round(arrowVX / 3.6f) % 1 == 0 && Math.Round((arrowVX - 21) / 3.6f) < 360)
                        {
                            HsvToRgb(currentHue, currentSat, (int)Math.Round((arrowVX - 21) / 3.6f), out r, out g, out b);
                            currentVal = (int)Math.Round((arrowVX - 21) / 3.6f);
                            newColor = Color.FromArgb(255, r, g, b);
                        }
                    }

                    break;
            }

        }

        public void SelectColor(bool mouseDown)
        {
            if (mouseDown)
            {
                if (mouseX > xPos && mouseX < xPos + xSize && mouseY > yPos && mouseY < yPos + ySize)
                {
                    int X = mouseX - xPos;
                    int Y = mouseY - yPos;
                    int number;
                    int row = Y / colSize;

                    number = row * (xSize / colSize) + X / colSize;
                    if (number < Length)
                    {
                        selectedColor = number;
                    }
                    else if (number == Length)
                    {
                        colorPickerOpen = true;
                    }
                }
            }
            if (colorPickerOpen && mouseDown)
            {
                switch (colPickFormat)
                {
                    case "RGB":
                        if (mouseX > arrowRX + cpXPos && mouseX < arrowRX + cpXPos + 7 && mouseY > arrowRY + cpYPos && mouseY < arrowRY + cpYPos + 13)
                            arrowRCatched = true;

                        if (mouseX > arrowGX + cpXPos && mouseX < arrowGX + cpXPos + 7 && mouseY > arrowGY + cpYPos && mouseY < arrowGY + cpYPos + 13)
                            arrowGCatched = true;

                        if (mouseX > arrowBX + cpXPos && mouseX < arrowBX + cpXPos + 7 && mouseY > arrowBY + cpYPos && mouseY < arrowBY + cpYPos + 13)
                            arrowBCatched = true;

                        break;

                    case "HSV":
                        if (mouseX > arrowHX + cpXPos && mouseX < arrowHX + cpXPos + 7 && mouseY > arrowHY + cpYPos && mouseY < arrowHY + cpYPos + 13)
                            arrowHCatched = true;

                        if (mouseX > arrowSX + cpXPos && mouseX < arrowSX + cpXPos + 7 && mouseY > arrowSY + cpYPos && mouseY < arrowSY + cpYPos + 13)
                            arrowSCatched = true;

                        if (mouseX > arrowVX + cpXPos && mouseX < arrowVX + cpXPos + 7 && mouseY > arrowVY + cpYPos && mouseY < arrowVY + cpYPos + 13)
                            arrowVCatched = true;

                        break;
                }

            }
            else
            {
                arrowRCatched = false;
                arrowGCatched = false;
                arrowBCatched = false;
                arrowHCatched = false;
                arrowSCatched = false;
                arrowVCatched = false;
            }

            if (mouseDown && mouseX > 232 + cpXPos && mouseX < 280 + cpXPos && mouseY > 76 + cpYPos && mouseY < 96 + cpYPos)
            {
                if (colors.Length == Length)
                {
                    colors[Length - 1] = newColor;
                }
                else
                {
                    colors[Length] = newColor;
                }
                colorPickerOpen = false;
            }

            if (mouseDown && mouseX > 8 + cpXPos && mouseX < 56 + cpXPos && mouseY > 4 + cpYPos && mouseY < 20 + cpYPos)
            {
                colPickFormat = "RGB";
            }

            if (mouseDown && mouseX > 64 + cpXPos && mouseX < 102 + cpXPos && mouseY > 4 + cpYPos && mouseY < 20 + cpYPos)
            {
                colPickFormat = "HSV";
            }
        }

        public void AddColor(Color color)
        {
            for (int i = 0; i < colors.Length; i++)
            {
                if (colors[i] == null || colors[i] == Color.Empty)
                {
                    colors[i] = color;
                    break;
                }
            }
        }

        void HsvToRgb(double h, double S, double V, out int r, out int g, out int b)
        {
            // ######################################################################
            // T. Nathan Mundhenk
            // mundhenk@usc.edu
            // C/C++ Macro HSV to RGB
            S /= 100;
            V /= 100;
            double H = h;
            while (H < 0) { H += 360; };
            while (H >= 360) { H -= 360; };
            double R, G, B;
            if (V <= 0)
            { R = G = B = 0; }
            else if (S <= 0)
            {
                R = G = B = V;
            }
            else
            {
                double hf = H / 60.0;
                int i = (int)Math.Floor(hf);
                double f = hf - i;
                double pv = V * (1 - S);
                double qv = V * (1 - S * f);
                double tv = V * (1 - S * (1 - f));
                switch (i)
                {

                    // Red is the dominant color

                    case 0:
                        R = V;
                        G = tv;
                        B = pv;
                        break;

                    // Green is the dominant color

                    case 1:
                        R = qv;
                        G = V;
                        B = pv;
                        break;
                    case 2:
                        R = pv;
                        G = V;
                        B = tv;
                        break;

                    // Blue is the dominant color

                    case 3:
                        R = pv;
                        G = qv;
                        B = V;
                        break;
                    case 4:
                        R = tv;
                        G = pv;
                        B = V;
                        break;

                    // Red is the dominant color

                    case 5:
                        R = V;
                        G = pv;
                        B = qv;
                        break;

                    // Just in case we overshoot on our math by a little, we put these here. Since its a switch it won't slow us down at all to put these here.

                    case 6:
                        R = V;
                        G = tv;
                        B = pv;
                        break;
                    case -1:
                        R = V;
                        G = pv;
                        B = qv;
                        break;

                    // The color is not defined, we should throw an error.

                    default:
                        //LFATAL("i Value error in Pixel conversion, Value is %d", i);
                        R = G = B = V; // Just pretend its black/white
                        break;
                }
            }
            r = Clamp((int)(R * 255.0));
            g = Clamp((int)(G * 255.0));
            b = Clamp((int)(B * 255.0));
        }

        /// <summary>
        /// Clamp a value to 0-255
        /// </summary>
        int Clamp(int i)
        {
            if (i < 0) return 0;
            if (i > 255) return 255;
            return i;
        }

        private void RGBtoHSV(byte r, byte g, byte b, out int h, out int s, out int v, Color c)
        {
            float r1 = r / 255f;
            float g1 = g / 255f;
            float b1 = b / 255f;

            float Hue = 0;
            float Sat = 0;
            float Val = 0;

            float cMax = Math.Max(Math.Max(r1, g1), b1);
            float cMin = Math.Min(Math.Min(r1, g1), b1);
            float Δc = cMax - cMin;

            // Hue calculation
            Hue = c.GetHue();

            // Saturation calculation
            Sat = (cMax == 0) ? 0 : 1f - (1f * cMin / cMax);

            //Value calculation
            Val = cMax;

            h = (int)Hue;
            s = (int)(Math.Floor(Sat * 100));
            v = (int)(Math.Floor(Val * 100));
        }

        private void RGBtoHSV(byte r, byte g, byte b, out int s, out int v, Color c)
        {
            float r1 = r / 255f;
            float g1 = g / 255f;
            float b1 = b / 255f;

            float Sat = 0;
            float Val = 0;

            float cMax = Math.Max(Math.Max(r1, g1), b1);
            float cMin = Math.Min(Math.Min(r1, g1), b1);
            float Δc = cMax - cMin;

            // Saturation calculation
            Sat = (cMax == 0) ? 0 : 1f - (1f * cMin / cMax);

            //Value calculation
            Val = cMax;

            s = (int)(Math.Floor(Sat * 100));
            v = (int)(Math.Floor(Val * 100));
        }
    } 
}



