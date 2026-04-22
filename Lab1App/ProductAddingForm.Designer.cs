namespace LabApp
{
    partial class ProductAddingForm
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
            labelType = new Label();
            labelCode = new Label();
            textBoxCode = new TextBox();
            labelComponents = new Label();
            textBoxAddingComponent = new TextBox();
            BtnAdd = new Button();
            BtnCancel = new Button();
            listBoxComponents = new ListBox();
            linkLabelAddComponent = new LinkLabel();
            comboBoxTypes = new ComboBox();
            SuspendLayout();
            // 
            // labelName
            // 
            labelName.AutoSize = true;
            labelName.Location = new Point(12, 9);
            labelName.Name = "labelName";
            labelName.Size = new Size(77, 20);
            labelName.TabIndex = 0;
            labelName.Text = "Название";
            // 
            // textBoxName
            // 
            textBoxName.Location = new Point(12, 32);
            textBoxName.Name = "textBoxName";
            textBoxName.Size = new Size(335, 27);
            textBoxName.TabIndex = 1;
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
            textBoxCode.Size = new Size(335, 27);
            textBoxCode.TabIndex = 3;
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
            // textBoxAddingComponent
            // 
            textBoxAddingComponent.Location = new Point(12, 281);
            textBoxAddingComponent.Name = "textBoxAddingComponent";
            textBoxAddingComponent.Size = new Size(253, 27);
            textBoxAddingComponent.TabIndex = 4;
            // 
            // BtnAdd
            // 
            BtnAdd.Location = new Point(17, 331);
            BtnAdd.Name = "BtnAdd";
            BtnAdd.Size = new Size(94, 29);
            BtnAdd.TabIndex = 6;
            BtnAdd.Text = "Добавить";
            BtnAdd.UseVisualStyleBackColor = true;
            BtnAdd.Click += BtnAdd_Click;
            // 
            // BtnCancel
            // 
            BtnCancel.Location = new Point(253, 331);
            BtnCancel.Name = "BtnCancel";
            BtnCancel.Size = new Size(94, 29);
            BtnCancel.TabIndex = 7;
            BtnCancel.Text = "Отмена";
            BtnCancel.UseVisualStyleBackColor = true;
            BtnCancel.Click += BtnCancel_Click;
            // 
            // listBoxComponents
            // 
            listBoxComponents.FormattingEnabled = true;
            listBoxComponents.Location = new Point(12, 191);
            listBoxComponents.Name = "listBoxComponents";
            listBoxComponents.Size = new Size(335, 84);
            listBoxComponents.TabIndex = 10;
            // 
            // linkLabelAddComponent
            // 
            linkLabelAddComponent.ActiveLinkColor = Color.Blue;
            linkLabelAddComponent.AutoSize = true;
            linkLabelAddComponent.LinkBehavior = LinkBehavior.NeverUnderline;
            linkLabelAddComponent.LinkColor = Color.Black;
            linkLabelAddComponent.Location = new Point(271, 284);
            linkLabelAddComponent.Name = "linkLabelAddComponent";
            linkLabelAddComponent.Size = new Size(74, 20);
            linkLabelAddComponent.TabIndex = 5;
            linkLabelAddComponent.TabStop = true;
            linkLabelAddComponent.Text = "добавить";
            linkLabelAddComponent.LinkClicked += linkLabelAddComponent_LinkClicked;
            // 
            // comboBoxTypes
            // 
            comboBoxTypes.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            comboBoxTypes.AutoCompleteSource = AutoCompleteSource.ListItems;
            comboBoxTypes.Location = new Point(12, 84);
            comboBoxTypes.Name = "comboBoxTypes";
            comboBoxTypes.Size = new Size(333, 28);
            comboBoxTypes.TabIndex = 2;
            comboBoxTypes.SelectedIndexChanged += comboBoxTypes_SelectedIndexChanged;
            // 
            // ProductAddingForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(389, 372);
            Controls.Add(comboBoxTypes);
            Controls.Add(linkLabelAddComponent);
            Controls.Add(listBoxComponents);
            Controls.Add(BtnCancel);
            Controls.Add(BtnAdd);
            Controls.Add(textBoxAddingComponent);
            Controls.Add(labelComponents);
            Controls.Add(textBoxCode);
            Controls.Add(labelCode);
            Controls.Add(labelType);
            Controls.Add(textBoxName);
            Controls.Add(labelName);
            Name = "ProductAddingForm";
            Text = "ProductAddingForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label labelName;
        private TextBox textBoxName;
        private Label labelType;
        private Label labelCode;
        private TextBox textBoxCode;
        private Label labelComponents;
        private TextBox textBoxAddingComponent;
        private Button BtnAdd;
        private Button BtnCancel;
        private ListBox listBoxComponents;
        private LinkLabel linkLabelAddComponent;
        private ComboBox comboBoxTypes;
    }
}