namespace LabApp
{
    partial class ApplicationForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            leftPanel = new Panel();
            listProduct = new ListBox();
            btnAdd = new Button();
            rightPanel = new Panel();
            btnEdit = new Button();
            btnDelete = new Button();
            btnInfo = new Button();
            btnView = new Button();
            toolStrip1 = new ToolStrip();
            toolStripBtnAdd = new ToolStripButton();
            toolStripBtnView = new ToolStripButton();
            toolStripBtnEdit = new ToolStripButton();
            toolStripBtnDelete = new ToolStripButton();
            toolStripBtnInfo = new ToolStripButton();
            panelVisualisation = new Panel();
            btnStart = new Button();
            btnFirtsThreadPause = new Button();
            btnFirstThreadContinue = new Button();
            btnSecondThreadContinue = new Button();
            btnSecondThreadPause = new Button();
            btnStop = new Button();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            comboBoxFirstThreadPriority = new ComboBox();
            comboBoxSecondThreadPriority = new ComboBox();
            textboxSecondThreadState = new TextBox();
            textboxFirstThreadState = new TextBox();
            leftPanel.SuspendLayout();
            rightPanel.SuspendLayout();
            toolStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // leftPanel
            // 
            leftPanel.Controls.Add(listProduct);
            leftPanel.Controls.Add(btnAdd);
            leftPanel.Location = new Point(0, 25);
            leftPanel.Margin = new Padding(3, 2, 3, 2);
            leftPanel.Name = "leftPanel";
            leftPanel.Size = new Size(306, 442);
            leftPanel.TabIndex = 0;
            // 
            // listProduct
            // 
            listProduct.BorderStyle = BorderStyle.None;
            listProduct.FormattingEnabled = true;
            listProduct.Location = new Point(10, 2);
            listProduct.Margin = new Padding(3, 2, 3, 2);
            listProduct.Name = "listProduct";
            listProduct.Size = new Size(293, 390);
            listProduct.TabIndex = 2;
            listProduct.SelectedIndexChanged += ListProduct_SelectedIndexChanged;
            // 
            // btnAdd
            // 
            btnAdd.Cursor = Cursors.Hand;
            btnAdd.Location = new Point(10, 412);
            btnAdd.Margin = new Padding(3, 2, 3, 2);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(120, 22);
            btnAdd.TabIndex = 1;
            btnAdd.Text = "Добавить...";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += BtnAdd_Click;
            // 
            // rightPanel
            // 
            rightPanel.Controls.Add(btnEdit);
            rightPanel.Controls.Add(btnDelete);
            rightPanel.Controls.Add(btnInfo);
            rightPanel.Controls.Add(btnView);
            rightPanel.Location = new Point(312, 25);
            rightPanel.Margin = new Padding(3, 2, 3, 2);
            rightPanel.Name = "rightPanel";
            rightPanel.Size = new Size(173, 442);
            rightPanel.TabIndex = 1;
            // 
            // btnEdit
            // 
            btnEdit.Cursor = Cursors.Hand;
            btnEdit.Location = new Point(10, 27);
            btnEdit.Margin = new Padding(3, 2, 3, 2);
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size(120, 22);
            btnEdit.TabIndex = 4;
            btnEdit.Text = "Редактирвать";
            btnEdit.UseVisualStyleBackColor = true;
            btnEdit.Click += BtnEdit_Click;
            // 
            // btnDelete
            // 
            btnDelete.Cursor = Cursors.Hand;
            btnDelete.Location = new Point(10, 53);
            btnDelete.Margin = new Padding(3, 2, 3, 2);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(120, 22);
            btnDelete.TabIndex = 3;
            btnDelete.Text = "Удалить";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += BtnDelete_Click;
            // 
            // btnInfo
            // 
            btnInfo.Cursor = Cursors.Hand;
            btnInfo.Location = new Point(10, 410);
            btnInfo.Margin = new Padding(3, 2, 3, 2);
            btnInfo.Name = "btnInfo";
            btnInfo.Size = new Size(120, 22);
            btnInfo.TabIndex = 2;
            btnInfo.Text = "Справка";
            btnInfo.UseVisualStyleBackColor = true;
            btnInfo.Click += BtnInfo_Click;
            // 
            // btnView
            // 
            btnView.Cursor = Cursors.Hand;
            btnView.Location = new Point(10, 1);
            btnView.Margin = new Padding(3, 2, 3, 2);
            btnView.Name = "btnView";
            btnView.Size = new Size(120, 22);
            btnView.TabIndex = 0;
            btnView.Text = "Просмотр";
            btnView.UseVisualStyleBackColor = true;
            btnView.Click += BtnView_Click;
            // 
            // toolStrip1
            // 
            toolStrip1.ImageScalingSize = new Size(20, 20);
            toolStrip1.Items.AddRange(new ToolStripItem[] { toolStripBtnAdd, toolStripBtnView, toolStripBtnEdit, toolStripBtnDelete, toolStripBtnInfo });
            toolStrip1.Location = new Point(0, 0);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(984, 27);
            toolStrip1.TabIndex = 2;
            toolStrip1.Text = "toolStrip1";
            // 
            // toolStripBtnAdd
            // 
            toolStripBtnAdd.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripBtnAdd.Image = Properties.Resources.add;
            toolStripBtnAdd.ImageTransparentColor = Color.Magenta;
            toolStripBtnAdd.Name = "toolStripBtnAdd";
            toolStripBtnAdd.Size = new Size(24, 24);
            toolStripBtnAdd.Text = "Добавить";
            toolStripBtnAdd.Click += BtnAdd_Click;
            // 
            // toolStripBtnView
            // 
            toolStripBtnView.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripBtnView.Enabled = false;
            toolStripBtnView.Image = Properties.Resources.watch;
            toolStripBtnView.ImageTransparentColor = Color.Magenta;
            toolStripBtnView.Name = "toolStripBtnView";
            toolStripBtnView.Size = new Size(24, 24);
            toolStripBtnView.Text = "Просмотр";
            toolStripBtnView.Click += BtnView_Click;
            // 
            // toolStripBtnEdit
            // 
            toolStripBtnEdit.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripBtnEdit.Enabled = false;
            toolStripBtnEdit.Image = Properties.Resources.edit;
            toolStripBtnEdit.ImageTransparentColor = Color.Magenta;
            toolStripBtnEdit.Name = "toolStripBtnEdit";
            toolStripBtnEdit.Size = new Size(24, 24);
            toolStripBtnEdit.Text = "Редактировать";
            toolStripBtnEdit.Click += BtnEdit_Click;
            // 
            // toolStripBtnDelete
            // 
            toolStripBtnDelete.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripBtnDelete.Enabled = false;
            toolStripBtnDelete.Image = Properties.Resources.delete;
            toolStripBtnDelete.ImageTransparentColor = Color.Magenta;
            toolStripBtnDelete.Name = "toolStripBtnDelete";
            toolStripBtnDelete.Size = new Size(24, 24);
            toolStripBtnDelete.Text = "Удалить";
            toolStripBtnDelete.Click += BtnDelete_Click;
            // 
            // toolStripBtnInfo
            // 
            toolStripBtnInfo.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripBtnInfo.Image = Properties.Resources.info;
            toolStripBtnInfo.ImageTransparentColor = Color.Magenta;
            toolStripBtnInfo.Name = "toolStripBtnInfo";
            toolStripBtnInfo.Size = new Size(24, 24);
            toolStripBtnInfo.Text = "Справка";
            toolStripBtnInfo.Click += BtnInfo_Click;
            // 
            // panelVisualisation
            // 
            panelVisualisation.BorderStyle = BorderStyle.FixedSingle;
            panelVisualisation.ForeColor = SystemColors.ActiveCaptionText;
            panelVisualisation.Location = new Point(490, 117);
            panelVisualisation.Margin = new Padding(3, 2, 3, 2);
            panelVisualisation.Name = "panelVisualisation";
            panelVisualisation.Size = new Size(483, 300);
            panelVisualisation.TabIndex = 3;
            panelVisualisation.Paint += PanelVisualisation_Paint;
            // 
            // btnStart
            // 
            btnStart.Location = new Point(786, 4);
            btnStart.Margin = new Padding(3, 2, 3, 2);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(82, 22);
            btnStart.TabIndex = 0;
            btnStart.Text = "Старт";
            btnStart.UseVisualStyleBackColor = true;
            btnStart.Click += BtnStart_Click;
            // 
            // btnFirtsThreadPause
            // 
            btnFirtsThreadPause.Location = new Point(873, 52);
            btnFirtsThreadPause.Margin = new Padding(3, 2, 3, 2);
            btnFirtsThreadPause.Name = "btnFirtsThreadPause";
            btnFirtsThreadPause.Size = new Size(82, 22);
            btnFirtsThreadPause.TabIndex = 5;
            btnFirtsThreadPause.Text = "Пауза";
            btnFirtsThreadPause.UseVisualStyleBackColor = true;
            btnFirtsThreadPause.Click += BtnFirstThreadPause_Click;
            // 
            // btnFirstThreadContinue
            // 
            btnFirstThreadContinue.Location = new Point(786, 52);
            btnFirstThreadContinue.Margin = new Padding(3, 2, 3, 2);
            btnFirstThreadContinue.Name = "btnFirstThreadContinue";
            btnFirstThreadContinue.Size = new Size(82, 22);
            btnFirstThreadContinue.TabIndex = 6;
            btnFirstThreadContinue.Text = "Продолж.";
            btnFirstThreadContinue.UseVisualStyleBackColor = true;
            btnFirstThreadContinue.Click += BtnFirstThreadContinue_Click;
            // 
            // btnSecondThreadContinue
            // 
            btnSecondThreadContinue.Location = new Point(786, 78);
            btnSecondThreadContinue.Margin = new Padding(3, 2, 3, 2);
            btnSecondThreadContinue.Name = "btnSecondThreadContinue";
            btnSecondThreadContinue.Size = new Size(82, 22);
            btnSecondThreadContinue.TabIndex = 8;
            btnSecondThreadContinue.Text = "Продолж.";
            btnSecondThreadContinue.UseVisualStyleBackColor = true;
            btnSecondThreadContinue.Click += BtnSecondThreadContinue_Click;
            // 
            // btnSecondThreadPause
            // 
            btnSecondThreadPause.Location = new Point(873, 78);
            btnSecondThreadPause.Margin = new Padding(3, 2, 3, 2);
            btnSecondThreadPause.Name = "btnSecondThreadPause";
            btnSecondThreadPause.Size = new Size(82, 22);
            btnSecondThreadPause.TabIndex = 6;
            btnSecondThreadPause.Text = "Пауза";
            btnSecondThreadPause.UseVisualStyleBackColor = true;
            btnSecondThreadPause.Click += BtnSecondThreadPause_Click;
            // 
            // btnStop
            // 
            btnStop.Location = new Point(873, 4);
            btnStop.Margin = new Padding(3, 2, 3, 2);
            btnStop.Name = "btnStop";
            btnStop.Size = new Size(82, 22);
            btnStop.TabIndex = 9;
            btnStop.Text = "Стоп";
            btnStop.UseVisualStyleBackColor = true;
            btnStop.Click += BtnStop_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(490, 8);
            label1.Name = "label1";
            label1.Size = new Size(130, 15);
            label1.TabIndex = 10;
            label1.Text = "Управление потоками";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(490, 55);
            label2.Name = "label2";
            label2.Size = new Size(89, 15);
            label2.TabIndex = 11;
            label2.Text = "Поток номер 1";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(490, 81);
            label3.Name = "label3";
            label3.Size = new Size(89, 15);
            label3.TabIndex = 12;
            label3.Text = "Поток номер 2";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(600, 31);
            label4.Name = "label4";
            label4.Size = new Size(43, 15);
            label4.TabIndex = 13;
            label4.Text = "Статус";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(694, 31);
            label5.Name = "label5";
            label5.Size = new Size(67, 15);
            label5.TabIndex = 16;
            label5.Text = "Приоритет";
            // 
            // comboBoxFirstThreadPriority
            // 
            comboBoxFirstThreadPriority.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            comboBoxFirstThreadPriority.AutoCompleteSource = AutoCompleteSource.ListItems;
            comboBoxFirstThreadPriority.Location = new Point(694, 52);
            comboBoxFirstThreadPriority.Margin = new Padding(3, 2, 3, 2);
            comboBoxFirstThreadPriority.Name = "comboBoxFirstThreadPriority";
            comboBoxFirstThreadPriority.Size = new Size(87, 23);
            comboBoxFirstThreadPriority.TabIndex = 19;
            comboBoxFirstThreadPriority.SelectedIndexChanged += ComboBoxFirstThreadPriority_SelectedIndexChanged;
            // 
            // comboBoxSecondThreadPriority
            // 
            comboBoxSecondThreadPriority.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            comboBoxSecondThreadPriority.AutoCompleteSource = AutoCompleteSource.ListItems;
            comboBoxSecondThreadPriority.Location = new Point(694, 78);
            comboBoxSecondThreadPriority.Margin = new Padding(3, 2, 3, 2);
            comboBoxSecondThreadPriority.Name = "comboBoxSecondThreadPriority";
            comboBoxSecondThreadPriority.Size = new Size(87, 23);
            comboBoxSecondThreadPriority.TabIndex = 20;
            comboBoxSecondThreadPriority.SelectedIndexChanged += ComboBoxSecondThreadPriority_SelectedIndexChanged;
            // 
            // textboxSecondThreadState
            // 
            textboxSecondThreadState.BorderStyle = BorderStyle.None;
            textboxSecondThreadState.Location = new Point(600, 81);
            textboxSecondThreadState.Margin = new Padding(3, 2, 3, 2);
            textboxSecondThreadState.Name = "textboxSecondThreadState";
            textboxSecondThreadState.ReadOnly = true;
            textboxSecondThreadState.Size = new Size(88, 16);
            textboxSecondThreadState.TabIndex = 15;
            // 
            // textboxFirstThreadState
            // 
            textboxFirstThreadState.BorderStyle = BorderStyle.None;
            textboxFirstThreadState.Location = new Point(600, 55);
            textboxFirstThreadState.Margin = new Padding(3, 2, 3, 2);
            textboxFirstThreadState.Name = "textboxFirstThreadState";
            textboxFirstThreadState.ReadOnly = true;
            textboxFirstThreadState.Size = new Size(88, 16);
            textboxFirstThreadState.TabIndex = 14;
            // 
            // ApplicationForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(984, 467);
            Controls.Add(comboBoxSecondThreadPriority);
            Controls.Add(comboBoxFirstThreadPriority);
            Controls.Add(label5);
            Controls.Add(textboxSecondThreadState);
            Controls.Add(textboxFirstThreadState);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(btnStop);
            Controls.Add(btnSecondThreadPause);
            Controls.Add(btnSecondThreadContinue);
            Controls.Add(btnFirstThreadContinue);
            Controls.Add(btnFirtsThreadPause);
            Controls.Add(btnStart);
            Controls.Add(panelVisualisation);
            Controls.Add(toolStrip1);
            Controls.Add(rightPanel);
            Controls.Add(leftPanel);
            Margin = new Padding(3, 2, 3, 2);
            Name = "ApplicationForm";
            leftPanel.ResumeLayout(false);
            rightPanel.ResumeLayout(false);
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel leftPanel;
        private Button btnAdd;
        private Panel rightPanel;
        private Button btnEdit;
        private Button btnDelete;
        private Button btnInfo;
        private Button btnView;
        private ListBox listProduct;
        private ToolStrip toolStrip1;
        private ToolStripButton toolStripBtnAdd;
        private ToolStripButton toolStripBtnView;
        private ToolStripButton toolStripBtnEdit;
        private ToolStripButton toolStripBtnDelete;
        private ToolStripButton toolStripBtnInfo;
        private Panel panelVisualisation;
        private Button btnStart;
        private Button btnFirtsThreadPause;
        private Button btnFirstThreadContinue;
        private Button btnSecondThreadContinue;
        private Button btnSecondThreadPause;
        private Button btnStop;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private ComboBox comboBoxFirstThreadPriority;
        private ComboBox comboBoxSecondThreadPriority;
        private TextBox textboxSecondThreadState;
        private TextBox textboxFirstThreadState;
    }
}
