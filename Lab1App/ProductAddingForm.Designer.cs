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
            labelName.Location = new Point(10, 7);
            labelName.Name = "labelName";
            labelName.Size = new Size(59, 15);
            labelName.TabIndex = 0;
            labelName.Text = "Название";
            // 
            // textBoxName
            // 
            textBoxName.BorderStyle = BorderStyle.FixedSingle;
            textBoxName.Location = new Point(10, 24);
            textBoxName.Margin = new Padding(3, 2, 3, 2);
            textBoxName.Name = "textBoxName";
            textBoxName.Size = new Size(294, 23);
            textBoxName.TabIndex = 1;
            // 
            // labelType
            // 
            labelType.AutoSize = true;
            labelType.Location = new Point(10, 47);
            labelType.Name = "labelType";
            labelType.Size = new Size(28, 15);
            labelType.TabIndex = 2;
            labelType.Text = "Тип";
            // 
            // labelCode
            // 
            labelCode.AutoSize = true;
            labelCode.Location = new Point(10, 86);
            labelCode.Name = "labelCode";
            labelCode.Size = new Size(53, 15);
            labelCode.TabIndex = 4;
            labelCode.Text = "Артикул";
            // 
            // textBoxCode
            // 
            textBoxCode.Location = new Point(10, 104);
            textBoxCode.Margin = new Padding(3, 2, 3, 2);
            textBoxCode.Name = "textBoxCode";
            textBoxCode.Size = new Size(294, 23);
            textBoxCode.TabIndex = 3;
            // 
            // labelComponents
            // 
            labelComponents.AutoSize = true;
            labelComponents.Location = new Point(10, 126);
            labelComponents.Name = "labelComponents";
            labelComponents.Size = new Size(87, 15);
            labelComponents.TabIndex = 6;
            labelComponents.Text = "Комплектация";
            // 
            // textBoxAddingComponent
            // 
            textBoxAddingComponent.Location = new Point(10, 211);
            textBoxAddingComponent.Margin = new Padding(3, 2, 3, 2);
            textBoxAddingComponent.Name = "textBoxAddingComponent";
            textBoxAddingComponent.Size = new Size(222, 23);
            textBoxAddingComponent.TabIndex = 4;
            // 
            // BtnAdd
            // 
            BtnAdd.Location = new Point(15, 248);
            BtnAdd.Margin = new Padding(3, 2, 3, 2);
            BtnAdd.Name = "BtnAdd";
            BtnAdd.Size = new Size(82, 22);
            BtnAdd.TabIndex = 6;
            BtnAdd.Text = "Добавить";
            BtnAdd.UseVisualStyleBackColor = true;
            BtnAdd.Click += BtnAdd_Click;
            // 
            // BtnCancel
            // 
            BtnCancel.Location = new Point(221, 248);
            BtnCancel.Margin = new Padding(3, 2, 3, 2);
            BtnCancel.Name = "BtnCancel";
            BtnCancel.Size = new Size(82, 22);
            BtnCancel.TabIndex = 7;
            BtnCancel.Text = "Отмена";
            BtnCancel.UseVisualStyleBackColor = true;
            BtnCancel.Click += BtnCancel_Click;
            // 
            // listBoxComponents
            // 
            listBoxComponents.FormattingEnabled = true;
            listBoxComponents.Location = new Point(10, 143);
            listBoxComponents.Margin = new Padding(3, 2, 3, 2);
            listBoxComponents.Name = "listBoxComponents";
            listBoxComponents.Size = new Size(294, 64);
            listBoxComponents.TabIndex = 10;
            // 
            // linkLabelAddComponent
            // 
            linkLabelAddComponent.ActiveLinkColor = Color.Blue;
            linkLabelAddComponent.AutoSize = true;
            linkLabelAddComponent.LinkBehavior = LinkBehavior.NeverUnderline;
            linkLabelAddComponent.LinkColor = Color.Black;
            linkLabelAddComponent.Location = new Point(237, 213);
            linkLabelAddComponent.Name = "linkLabelAddComponent";
            linkLabelAddComponent.Size = new Size(57, 15);
            linkLabelAddComponent.TabIndex = 5;
            linkLabelAddComponent.TabStop = true;
            linkLabelAddComponent.Text = "добавить";
            linkLabelAddComponent.LinkClicked += linkLabelAddComponent_LinkClicked;
            // 
            // comboBoxTypes
            // 
            comboBoxTypes.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            comboBoxTypes.AutoCompleteSource = AutoCompleteSource.ListItems;
            comboBoxTypes.Location = new Point(10, 63);
            comboBoxTypes.Margin = new Padding(3, 2, 3, 2);
            comboBoxTypes.Name = "comboBoxTypes";
            comboBoxTypes.Size = new Size(292, 23);
            comboBoxTypes.TabIndex = 2;
            comboBoxTypes.DisplayMember = "Text";
            comboBoxTypes.SelectedIndexChanged += comboBoxTypes_SelectedIndexChanged;
            // 
            // ProductAddingForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(340, 279);
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
            Margin = new Padding(3, 2, 3, 2);
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