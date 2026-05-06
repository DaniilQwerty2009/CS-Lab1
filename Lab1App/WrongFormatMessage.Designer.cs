namespace LabApp
{
    partial class WrongFormatMessage
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            labelWrongFormat = new Label();
            BtnOK = new Button();
            SuspendLayout();
            // 
            // labelWrongFormat
            // 
            labelWrongFormat.AutoSize = true;
            labelWrongFormat.Location = new Point(37, 52);
            labelWrongFormat.Name = "labelWrongFormat";
            labelWrongFormat.Size = new Size(188, 20);
            labelWrongFormat.TabIndex = 0;
            labelWrongFormat.Text = "Неверный формат ввода!";
            // 
            // BtnOK
            // 
            BtnOK.Location = new Point(91, 114);
            BtnOK.Name = "BtnOK";
            BtnOK.Size = new Size(94, 29);
            BtnOK.TabIndex = 1;
            BtnOK.Text = "OK";
            BtnOK.UseVisualStyleBackColor = true;
            BtnOK.Click += BtnOK_Click;
            // 
            // WrongFormatMessage
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(265, 172);
            Controls.Add(BtnOK);
            Controls.Add(labelWrongFormat);
            Name = "WrongFormatMessage";
            Text = "WronfFormatMessage";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label labelWrongFormat;
        private Button BtnOK;
    }
}