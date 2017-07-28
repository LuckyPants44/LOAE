namespace CAP
{
    partial class Form1
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
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.FillingFigureButton = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.InsertPointsButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox
            // 
            this.pictureBox.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.pictureBox.Location = new System.Drawing.Point(214, 12);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(843, 479);
            this.pictureBox.TabIndex = 0;
            this.pictureBox.TabStop = false;
            // 
            // FillingFigureButton
            // 
            this.FillingFigureButton.Location = new System.Drawing.Point(13, 416);
            this.FillingFigureButton.Name = "FillingFigureButton";
            this.FillingFigureButton.Size = new System.Drawing.Size(195, 87);
            this.FillingFigureButton.TabIndex = 1;
            this.FillingFigureButton.Text = "Filling figure";
            this.FillingFigureButton.UseVisualStyleBackColor = true;
            this.FillingFigureButton.Click += new System.EventHandler(this.FillingFigureButton_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(13, 323);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(195, 87);
            this.button2.TabIndex = 2;
            this.button2.UseVisualStyleBackColor = true;
            // 
            // InsertPointsButton
            // 
            this.InsertPointsButton.Location = new System.Drawing.Point(12, 230);
            this.InsertPointsButton.Name = "InsertPointsButton";
            this.InsertPointsButton.Size = new System.Drawing.Size(195, 87);
            this.InsertPointsButton.TabIndex = 3;
            this.InsertPointsButton.Text = "Insert points";
            this.InsertPointsButton.UseVisualStyleBackColor = true;
            this.InsertPointsButton.Click += new System.EventHandler(this.InsertPointsButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1069, 515);
            this.Controls.Add(this.InsertPointsButton);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.FillingFigureButton);
            this.Controls.Add(this.pictureBox);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.Button FillingFigureButton;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button InsertPointsButton;
    }
}

