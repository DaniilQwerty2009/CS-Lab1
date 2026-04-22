namespace LabApp
{
    partial class ProductEditForm
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
            labelComponents = new Label();
            btnOK = new Button();
            btnCancel = new Button();
            listBoxComponents = new ListBox();
            btnDelete = new Button();
            textBoxAddComponent = new TextBox();
            linkLabelAddComponent = new LinkLabel();
            SuspendLayout();
            // 
            // textBoxName
            // 
            textBoxName.Location = new Point(12, 31);
            textBoxName.Name = "textBoxName";
            textBoxName.Size = new Size(286, 27);
            textBoxName.TabIndex = 0;
            // 
            // labelName
            // 
            labelName.AutoSize = true;
            labelName.Location = new Point(12, 8);
            labelName.Name = "labelName";
            labelName.Size = new Size(77, 20);
            labelName.TabIndex = 1;
            labelName.Text = "Название";
            // 
            // labelComponents
            // 
            labelComponents.AutoSize = true;
            labelComponents.Location = new Point(12, 77);
            labelComponents.Name = "labelComponents";
            labelComponents.Size = new Size(99, 20);
            labelComponents.TabIndex = 2;
            labelComponents.Text = "Компоненты";
            // 
            // btnOK
            // 
            btnOK.Location = new Point(79, 302);
            btnOK.Name = "btnOK";
            btnOK.Size = new Size(94, 29);
            btnOK.TabIndex = 3;
            btnOK.Text = "Сохранить";
            btnOK.UseVisualStyleBackColor = true;
            btnOK.Click += BtnOK_Click;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(262, 302);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(94, 29);
            btnCancel.TabIndex = 4;
            btnCancel.Text = "Отмена";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += BtnCancel_Click;
            // 
            // listBoxComponents
            // 
            listBoxComponents.FormattingEnabled = true;
            listBoxComponents.Location = new Point(12, 100);
            listBoxComponents.Name = "listBoxComponents";
            listBoxComponents.Size = new Size(286, 144);
            listBoxComponents.TabIndex = 5;
            listBoxComponents.SelectedIndexChanged += listBoxComponents_SelectedIndexChanged;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(304, 100);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(132, 29);
            btnDelete.TabIndex = 7;
            btnDelete.Text = "Удалить";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += BtnDelete_Click;
            // 
            // textBoxAddComponent
            // 
            textBoxAddComponent.Location = new Point(12, 254);
            textBoxAddComponent.Name = "textBoxAddComponent";
            textBoxAddComponent.Size = new Size(206, 27);
            textBoxAddComponent.TabIndex = 9;
            // 
            // linkLabelAddComponent
            // 
            linkLabelAddComponent.ActiveLinkColor = Color.Blue;
            linkLabelAddComponent.AutoSize = true;
            linkLabelAddComponent.LinkBehavior = LinkBehavior.NeverUnderline;
            linkLabelAddComponent.LinkColor = Color.Black;
            linkLabelAddComponent.Location = new Point(224, 254);
            linkLabelAddComponent.Name = "linkLabelAddComponent";
            linkLabelAddComponent.Size = new Size(74, 20);
            linkLabelAddComponent.TabIndex = 12;
            linkLabelAddComponent.TabStop = true;
            linkLabelAddComponent.Text = "добавить";
            linkLabelAddComponent.Click += linkLabelAddComponent_Click;
            // 
            // ProductEditForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(442, 364);
            Controls.Add(linkLabelAddComponent);
            Controls.Add(textBoxAddComponent);
            Controls.Add(btnDelete);
            Controls.Add(listBoxComponents);
            Controls.Add(btnCancel);
            Controls.Add(btnOK);
            Controls.Add(labelComponents);
            Controls.Add(labelName);
            Controls.Add(textBoxName);
            Name = "ProductEditForm";
            Text = "ProductEditForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBoxName;
        private Label labelName;
        private Label labelComponents;
        private Button btnOK;
        private Button btnCancel;
        private ListBox listBoxComponents;
        private Button btnDelete;
        private TextBox textBoxAddComponent;
        private LinkLabel linkLabelAddComponent;
    }
}