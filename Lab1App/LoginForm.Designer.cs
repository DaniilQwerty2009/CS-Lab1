namespace LabApp
{
    partial class LoginForm
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
            labelName = new Label();
            textBoxName = new TextBox();
            btnLogin = new Button();
            SuspendLayout();
            // 
            // labelName
            // 
            labelName.AutoSize = true;
            labelName.Location = new Point(12, 17);
            labelName.Name = "labelName";
            labelName.Size = new Size(31, 15);
            labelName.TabIndex = 0;
            labelName.Text = "Имя";
            // 
            // textBoxName
            // 
            textBoxName.Location = new Point(12, 35);
            textBoxName.Name = "textBoxName";
            textBoxName.Size = new Size(145, 23);
            textBoxName.TabIndex = 1;
            textBoxName.TextChanged += TextBoxNameTextChanged;
            // 
            // btnLogin
            // 
            btnLogin.Location = new Point(99, 208);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(75, 23);
            btnLogin.TabIndex = 2;
            btnLogin.Text = "Войти";
            btnLogin.UseVisualStyleBackColor = true;
            btnLogin.Click += BtnLogin_Click;
            // 
            // LoginForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(267, 256);
            Controls.Add(btnLogin);
            Controls.Add(textBoxName);
            Controls.Add(labelName);
            Name = "LoginForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Вход в систему";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label labelName;
        private TextBox textBoxName;
        private Button btnLogin;
    }
}