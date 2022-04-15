using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace MasterSprite
{
    class MainControll
    {
        PictureBox pb;
        TextBox w, h;
        public Bitmap bmp;
        PaintControl paintControl;
        OpenFileDialog openFile;
        SaveFileDialog saveFile;
        string currentWin = "mainmenu", currentBack = "trans";
        bool mouseOnNew = false, mouseOnOpen = false, newFile = false, mouseOnTrans, mouseOnWhite, mouseOnBlack, mouseOnOk, mouseOnCancel, mouseOnNew1, mouseOnOpen1, mouseOnSave;
        int mouseX, mouseY;

        public MainControll(PictureBox pb, TextBox w, TextBox h, PaintControl paintControl, OpenFileDialog openFile, SaveFileDialog saveFile)
        {
            this.pb = pb;
            this.w = w;
            this.h = h;
            w.Text = "16";
            h.Text = "16";
            this.paintControl = paintControl;
            this.openFile = openFile;
            this.saveFile = saveFile;
        }

        public void Draw(Form frm, ColorPicker colorPicker, LayerSwitch layerSwitch, Toolbar toolbar)
        {
            if(frm.WindowState != FormWindowState.Minimized)
            bmp = new Bitmap(this.pb.Width, this.pb.Height);
            Graphics g = Graphics.FromImage(bmp);

            if(currentWin == "editor")
            {
                g.FillRectangle(new SolidBrush(Color.FromArgb(255, 214, 191, 212)), 0, 0, bmp.Width, bmp.Height);
                if (mouseOnNew1) { g.DrawImageUnscaled(Properties.Resources.button_new1_1, 0, 0); } else { g.DrawImageUnscaled(Properties.Resources.button_new1_0, 0, 0); }
                if (mouseOnOpen1) { g.DrawImageUnscaled(Properties.Resources.button_open1_1, 100, 0); } else { g.DrawImageUnscaled(Properties.Resources.button_open1_0, 100, 0); }
                if (mouseOnSave) { g.DrawImageUnscaled(Properties.Resources.button_save_1, 200, 0); } else { g.DrawImageUnscaled(Properties.Resources.button_save_0, 200, 0); }
                g.DrawImage(paintControl.Getbmp(), paintControl.GetDrawingPoint());
                g.DrawImage(colorPicker.Getbmp(), colorPicker.GetDrawingPoint());
                g.DrawImage(layerSwitch.GetBmp(), layerSwitch.GetDrawingPoint());
                g.DrawImage(toolbar.GetBmp(), toolbar.GetDrawingPoint());
                if (colorPicker.Getbmp2() != null)
                    g.DrawImage(colorPicker.Getbmp2(), colorPicker.GetDrawingPoint2());


            }
            else if(currentWin == "mainmenu")
            {
                if(paintControl != null)
                paintControl.active = false;
                g.FillRectangle(new SolidBrush(Color.FromArgb(255, 130, 111, 128)), 0, 0, bmp.Width, bmp.Height);
                if(mouseOnNew)
                {
                    g.DrawImageUnscaled(Properties.Resources.button_new_1, bmp.Width / 2 - 128, 96);
                }
                else
                {
                    g.DrawImageUnscaled(Properties.Resources.button_new_0, bmp.Width / 2 - 128, 96);
                }

                if (mouseOnOpen)
                {
                    g.DrawImageUnscaled(Properties.Resources.button_open_1, bmp.Width / 2 - 128, 192);
                }
                else
                {
                    g.DrawImageUnscaled(Properties.Resources.button_open_0, bmp.Width / 2 - 128, 192);
                }
                #region newfile
                if(newFile)
                {
                    g.FillRectangle(new SolidBrush(Color.FromArgb(255, 255, 238, 217)), bmp.Width / 2 - 112, 64, 224, 240);
                    g.DrawLine(new Pen(Color.Black, 2), bmp.Width / 2 - 113, 64, bmp.Width / 2 - 113, 304);
                    g.DrawLine(new Pen(Color.Black, 2), bmp.Width / 2 + 113, 64, bmp.Width / 2 + 113, 304);
                    g.DrawLine(new Pen(Color.Black, 2), bmp.Width / 2 - 113, 64, bmp.Width / 2 + 113, 64);
                    g.DrawLine(new Pen(Color.Black, 2), bmp.Width / 2 - 113, 304, bmp.Width / 2 + 113, 304);

                    w.Location = new Point(bmp.Width / 2 - 48, 106);
                    w.Enabled = true;
                    w.Visible = true;
                    h.Location = new Point(bmp.Width / 2 - 48, 132);
                    h.Enabled = true;
                    h.Visible = true;

                    g.DrawString("New file", new Font(new FontFamily("Calibri"), 18, FontStyle.Regular), new SolidBrush(Color.Black), bmp.Width / 2 - 44, 72);
                    g.DrawString("Width", new Font(new FontFamily("Calibri"), 14, FontStyle.Regular), new SolidBrush(Color.Black), bmp.Width / 2 - 108, 104);
                    g.DrawString("Height", new Font(new FontFamily("Calibri"), 14, FontStyle.Regular), new SolidBrush(Color.Black), bmp.Width / 2 - 108, 128);

                    g.DrawLine(new Pen(Color.FromArgb(255, Color.Black), 2), bmp.Width / 2 - 112, 164, bmp.Width / 2 + 112, 164);

                    g.DrawString("Background", new Font(new FontFamily("Calibri"), 14, FontStyle.Regular), new SolidBrush(Color.Black), bmp.Width / 2 - 48, 168);

                    if (mouseOnTrans || currentBack == "trans") { g.DrawImageUnscaled(Properties.Resources.button_trans_1, bmp.Width / 2 - 104, 200); } else { g.DrawImageUnscaled(Properties.Resources.button_trans_0, bmp.Width / 2 - 104, 200); }
                    if (mouseOnWhite || currentBack == "white") { g.DrawImageUnscaled(Properties.Resources.button_white_1, bmp.Width / 2 - 32, 200); } else { g.DrawImageUnscaled(Properties.Resources.button_white_0, bmp.Width / 2 - 32, 200); }
                    if (mouseOnBlack || currentBack == "black") { g.DrawImageUnscaled(Properties.Resources.button_black_1, bmp.Width / 2 + 40, 200); } else { g.DrawImageUnscaled(Properties.Resources.button_black_0, bmp.Width / 2 + 40, 200); }

                    g.DrawLine(new Pen(Color.FromArgb(255, Color.Black), 2), bmp.Width / 2 - 112, 240, bmp.Width / 2 + 112, 240);

                    if (mouseOnOk) { g.DrawImageUnscaled(Properties.Resources.button_OK_1, bmp.Width / 2 - 104, 264); } else { g.DrawImageUnscaled(Properties.Resources.button_OK_0, bmp.Width / 2 - 104, 264); }
                    if (mouseOnCancel) { g.DrawImageUnscaled(Properties.Resources.button_Cancel_1, bmp.Width / 2, 264); } else { g.DrawImageUnscaled(Properties.Resources.button_Cancel_0, bmp.Width / 2, 264); }
                    #endregion
                }
            }

            pb.Image = bmp;
        }

        public void MousePos(int x, int y)
        {
            mouseX = x;
            mouseY = y;

            if(currentWin == "editor")
            {
                if(mouseX > 0 && mouseX < 96 && mouseY > 0 && mouseY < 32)
                {
                    mouseOnNew1 = true;
                }
                else
                {
                    mouseOnNew1 = false;
                }

                if (mouseX > 100 && mouseX < 196 && mouseY > 0 && mouseY < 32)
                {
                    mouseOnOpen1 = true;
                }
                else
                {
                    mouseOnOpen1 = false;
                }

                if (mouseX > 200 && mouseX < 296 && mouseY > 0 && mouseY < 32)
                {
                    mouseOnSave = true;
                }
                else
                {
                    mouseOnSave = false;
                }

            }

            if(currentWin == "mainmenu")
            {
                if (mouseX > bmp.Width / 2 - 128 && mouseX < bmp.Width / 2 - 128 + 256 && mouseY > 96 && mouseY < 160 && !newFile)
                {
                    mouseOnNew = true;
                }
                else
                {
                    mouseOnNew = false;
                }

                if (mouseX > bmp.Width / 2 - 128 && mouseX < bmp.Width / 2 - 128 + 256 && mouseY > 192 && mouseY < 256 && !newFile)
                {
                    mouseOnOpen = true;
                }
                else
                {
                    mouseOnOpen = false;
                }
                if (newFile)
                {
                    if (mouseX > bmp.Width / 2 - 104 && mouseX < bmp.Width / 2 - 40 && mouseY > 200 && mouseY < 232)
                    {
                        mouseOnTrans = true;
                    }
                    else
                    {
                        mouseOnTrans = false;
                    }

                    if (mouseX > bmp.Width / 2 - 32 && mouseX < bmp.Width / 2 + 32 && mouseY > 200 && mouseY < 232)
                    {
                        mouseOnWhite = true;
                    }
                    else
                    {
                        mouseOnWhite = false;
                    }

                    if (mouseX > bmp.Width / 2 + 40 && mouseX < bmp.Width / 2 + 104 && mouseY > 200 && mouseY < 232)
                    {
                        mouseOnBlack = true;
                    }
                    else
                    {
                        mouseOnBlack = false;
                    }

                    if (mouseX > bmp.Width / 2 && mouseX < bmp.Width / 2 + 104 && mouseY > 264 && mouseY < 296)
                    {
                        mouseOnCancel = true;
                    }
                    else
                    {
                        mouseOnCancel = false;
                    }

                    if (mouseX > bmp.Width / 2 - 104 && mouseX < bmp.Width / 2 - 8 && mouseY > 264 && mouseY < 296)
                    {
                        mouseOnOk = true;
                    }
                    else
                    {
                        mouseOnOk = false;
                    }
                }
            }
            
        }

        public void MouseDown(bool down)
        {
            if(down)
            {
                if(currentWin == "editor")
                {
                    if (mouseX > 0 && mouseX < 96 && mouseY > 0 && mouseY < 32)
                    {
                        currentWin = "mainmenu";
                        newFile = true;
                        w.Enabled = false;
                        w.Visible = false;
                        h.Enabled = false;
                        h.Visible = false;
                    }

                    if (mouseX > 100 && mouseX < 196 && mouseY > 0 && mouseY < 32)
                    {
                        if (openFile.ShowDialog() == DialogResult.OK)
                        {
                            Bitmap file = new Bitmap(openFile.FileName);
                            paintControl.OpenFile(file);

                            w.Enabled = false;
                            w.Visible = false;
                            h.Enabled = false;
                            h.Visible = false;
                            currentWin = "editor";
                            paintControl.active = true;
                            paintControl.xPos = bmp.Width / 2 - file.Width / 2;
                            paintControl.yPos = bmp.Height / 2 - file.Height / 2;
                        }
                    }

                    if (mouseX > 200 && mouseX < 296 && mouseY > 0 && mouseY < 32)
                    {
                        Bitmap file = new Bitmap(0, 0);
                        if (saveFile.ShowDialog() == DialogResult.OK)
                        {
                            paintControl.SaveFile(file);
                            file.Save(saveFile.FileName);
                        }
                    }
                }

                    if (mouseX > bmp.Width / 2 - 128 && mouseX < bmp.Width / 2 - 128 + 256 && mouseY > 96 && mouseY < 160 && !newFile)
                    {
                        newFile = true;
                    }

                    if (newFile)
                    {
                        if (mouseX > bmp.Width / 2 - 104 && mouseX < bmp.Width / 2 - 40 && mouseY > 200 && mouseY < 232)
                        {
                            currentBack = "trans";
                        }

                        if (mouseX > bmp.Width / 2 - 32 && mouseX < bmp.Width / 2 + 32 && mouseY > 200 && mouseY < 232)
                        {
                            currentBack = "white";
                        }

                        if (mouseX > bmp.Width / 2 + 40 && mouseX < bmp.Width / 2 + 104 && mouseY > 200 && mouseY < 232)
                        {
                            currentBack = "black";
                        }

                        if (mouseX > bmp.Width / 2 && mouseX < bmp.Width / 2 + 104 && mouseY > 264 && mouseY < 296)
                        {
                            newFile = false;
                            w.Enabled = false;
                            w.Visible = false;
                            h.Enabled = false;
                            h.Visible = false;
                        }

                        if (mouseX > bmp.Width / 2 - 104 && mouseX < bmp.Width / 2 - 8 && mouseY > 264 && mouseY < 296)
                        {
                            newFile = false;
                            w.Enabled = false;
                            w.Visible = false;
                            h.Enabled = false;
                            h.Visible = false;
                            currentWin = "editor";
                            if (paintControl != null)
                            {
                                paintControl.NewFile(Convert.ToInt32(w.Text), Convert.ToInt32(h.Text), currentBack);
                                paintControl.active = true;
                                paintControl.xPos = bmp.Width / 2 - Convert.ToInt32(w.Text) / 2;
                                paintControl.yPos = bmp.Height / 2 - Convert.ToInt32(h.Text) / 2;
                            }
                        }
                    }
                    if (mouseX > bmp.Width / 2 - 128 && mouseX < bmp.Width / 2 - 128 + 256 && mouseY > 192 && mouseY < 256)
                    {
                        if (openFile.ShowDialog() == DialogResult.OK)
                        {
                            Bitmap file = new Bitmap(openFile.FileName);
                            paintControl.OpenFile(file);

                            w.Enabled = false;
                            w.Visible = false;
                            h.Enabled = false;
                            h.Visible = false;
                            currentWin = "editor";
                            paintControl.active = true;
                            paintControl.xPos = bmp.Width / 2 - file.Width / 2;
                            paintControl.yPos = bmp.Height / 2 - file.Height / 2;
                        }
                }
                
            }
        }
    }
}
