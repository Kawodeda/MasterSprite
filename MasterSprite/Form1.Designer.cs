namespace MasterSprite
{
    partial class mainFrm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(mainFrm));
            this.mainGraphics = new System.Windows.Forms.PictureBox();
            this.mainTime = new System.Windows.Forms.Timer(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.width = new System.Windows.Forms.TextBox();
            this.height = new System.Windows.Forms.TextBox();
            this.openFile = new System.Windows.Forms.OpenFileDialog();
            this.saveFile = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.mainGraphics)).BeginInit();
            this.SuspendLayout();
            // 
            // mainGraphics
            // 
            this.mainGraphics.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainGraphics.Location = new System.Drawing.Point(0, 0);
            this.mainGraphics.Name = "mainGraphics";
            this.mainGraphics.Size = new System.Drawing.Size(1582, 853);
            this.mainGraphics.TabIndex = 0;
            this.mainGraphics.TabStop = false;
            this.mainGraphics.MouseDown += new System.Windows.Forms.MouseEventHandler(this.mainGraphics_MouseDown);
            this.mainGraphics.MouseMove += new System.Windows.Forms.MouseEventHandler(this.mainGraphics_MouseMove);
            this.mainGraphics.MouseUp += new System.Windows.Forms.MouseEventHandler(this.mainGraphics_MouseUp);
            // 
            // mainTime
            // 
            this.mainTime.Interval = 1;
            this.mainTime.Tick += new System.EventHandler(this.mainTime_Tick);
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // width
            // 
            this.width.Enabled = false;
            this.width.Location = new System.Drawing.Point(0, 0);
            this.width.Name = "width";
            this.width.Size = new System.Drawing.Size(48, 22);
            this.width.TabIndex = 1;
            this.width.Visible = false;
            // 
            // height
            // 
            this.height.Enabled = false;
            this.height.Location = new System.Drawing.Point(54, 0);
            this.height.Name = "height";
            this.height.Size = new System.Drawing.Size(48, 22);
            this.height.TabIndex = 2;
            this.height.Visible = false;
            // 
            // openFile
            // 
            this.openFile.Filter = "Image Files(*.BMP;*.JPG)|";
            this.openFile.Title = "Select file";
            // 
            // saveFile
            // 
            this.saveFile.Filter = "Image Files(*.BMP;*.JPG)|";
            // 
            // mainFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1582, 853);
            this.Controls.Add(this.height);
            this.Controls.Add(this.width);
            this.Controls.Add(this.mainGraphics);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "mainFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MasterSprite";
            this.Load += new System.EventHandler(this.mainFrm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.mainGraphics)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox mainGraphics;
        private System.Windows.Forms.Timer mainTime;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TextBox width;
        private System.Windows.Forms.TextBox height;
        private System.Windows.Forms.OpenFileDialog openFile;
        private System.Windows.Forms.SaveFileDialog saveFile;
    }
}

