namespace LabApp
{
    partial class ProductVewForm
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
            textBoxName = new TextBox();
            labelName = new Label();
            labelType = new Label();
            textBoxType = new TextBox();
            labelCode = new Label();
            textBoxCode = new TextBox();
            labelComponents = new Label();
            listBoxComponents = new ListBox();
            SuspendLayout();
            // 
            // textBoxName
            // 
            textBoxName.Location = new Point(12, 32);
            textBoxName.Name = "textBoxName";
            textBoxName.ReadOnly = true;
            textBoxName.Size = new Size(406, 27);
            textBoxName.TabIndex = 0;
            // 
            // labelName
            // 
            labelName.AutoSize = true;
            labelName.Location = new Point(12, 9);
            labelName.Name = "labelName";
            labelName.Size = new Size(77, 20);
            labelName.TabIndex = 1;
            labelName.Text = "Название";
            // 
            // labelType
            // 
            labelType.AutoSize = true;
            labelType.Location = new Point(12, 62);
            labelType.Name = "labelType";
            labelType.Size = new Size(35, 20);
            labelType.TabIndex = 2;
            labelType.Text = "Тип";
            // 
            // textBoxType
            // 
            textBoxType.Location = new Point(12, 85);
            textBoxType.Name = "textBoxType";
            textBoxType.ReadOnly = true;
            textBoxType.Size = new Size(406, 27);
            textBoxType.TabIndex = 3;
            // 
            // labelCode
            // 
            labelCode.AutoSize = true;
            labelCode.Location = new Point(12, 115);
            labelCode.Name = "labelCode";
            labelCode.Size = new Size(65, 20);
            labelCode.TabIndex = 4;
            labelCode.Text = "Артикул";
            // 
            // textBoxCode
            // 
            textBoxCode.Location = new Point(12, 138);
            textBoxCode.Name = "textBoxCode";
            textBoxCode.ReadOnly = true;
            textBoxCode.Size = new Size(406, 27);
            textBoxCode.TabIndex = 5;
            // 
            // labelComponents
            // 
            labelComponents.AutoSize = true;
            labelComponents.Location = new Point(12, 168);
            labelComponents.Name = "labelComponents";
            labelComponents.Size = new Size(110, 20);
            labelComponents.TabIndex = 6;
            labelComponents.Text = "Комплектация";
            // 
            // listBoxComponents
            // 
            listBoxComponents.FormattingEnabled = true;
            listBoxComponents.Location = new Point(12, 191);
            listBoxComponents.Name = "listBoxComponents";
            listBoxComponents.Size = new Size(406, 224);
            listBoxComponents.TabIndex = 7;
            // 
            // ProductVewForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(544, 438);
            Controls.Add(listBoxComponents);
            Controls.Add(labelComponents);
            Controls.Add(textBoxCode);
            Controls.Add(labelCode);
            Controls.Add(textBoxType);
            Controls.Add(labelType);
            Controls.Add(labelName);
            Controls.Add(textBoxName);
            Name = "ProductVewForm";
            Text = "ProductVewForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBoxName;
        private Label labelName;
        private Label labelType;
        private TextBox textBoxType;
        private Label labelCode;
        private TextBox textBoxCode;
        private Label labelComponents;
        private ListBox listBoxComponents;
    }
}