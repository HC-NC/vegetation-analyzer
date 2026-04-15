namespace vegetation_analyzer.Forms
{
    partial class ClassifyForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            presetComboBox = new ComboBox();
            label1 = new Label();
            classTable = new DataGridView();
            colColor = new DataGridViewImageColumn();
            colClassName = new DataGridViewTextBoxColumn();
            colMin = new DataGridViewTextBoxColumn();
            colMax = new DataGridViewTextBoxColumn();
            previewPictureBox = new PictureBox();
            previewButton = new Button();
            applyButton = new Button();
            cancelButton = new Button();
            addClassButton = new Button();
            removeClassButton = new Button();
            moveUpButton = new Button();
            moveDownButton = new Button();
            flowLayoutPanel1 = new FlowLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)classTable).BeginInit();
            ((System.ComponentModel.ISupportInitialize)previewPictureBox).BeginInit();
            flowLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // presetComboBox
            // 
            presetComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            presetComboBox.FormattingEnabled = true;
            presetComboBox.Location = new Point(20, 68);
            presetComboBox.Margin = new Padding(4, 5, 4, 5);
            presetComboBox.Name = "presetComboBox";
            presetComboBox.Size = new Size(380, 33);
            presetComboBox.TabIndex = 0;
            presetComboBox.SelectedIndexChanged += presetComboBox_SelectedIndexChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(20, 25);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(73, 25);
            label1.TabIndex = 1;
            label1.Text = "Пресет:";
            // 
            // classTable
            // 
            classTable.AllowUserToAddRows = false;
            classTable.AllowUserToDeleteRows = false;
            classTable.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            classTable.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            classTable.Columns.AddRange(new DataGridViewColumn[] { colColor, colClassName, colMin, colMax });
            classTable.Location = new Point(20, 133);
            classTable.Margin = new Padding(4, 5, 4, 5);
            classTable.Name = "classTable";
            classTable.RowHeadersVisible = false;
            classTable.RowHeadersWidth = 62;
            classTable.RowTemplate.Height = 25;
            classTable.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            classTable.Size = new Size(380, 420);
            classTable.TabIndex = 2;
            classTable.CellContentClick += classTable_CellContentClick;
            classTable.CellEndEdit += classTable_CellEndEdit;
            // 
            // colColor
            // 
            colColor.HeaderText = "Цвет";
            colColor.ImageLayout = DataGridViewImageCellLayout.Stretch;
            colColor.MinimumWidth = 50;
            colColor.Name = "colColor";
            colColor.ReadOnly = true;
            // 
            // colClassName
            // 
            colClassName.HeaderText = "Название";
            colClassName.MinimumWidth = 120;
            colClassName.Name = "colClassName";
            // 
            // colMin
            // 
            colMin.HeaderText = "Min";
            colMin.MinimumWidth = 70;
            colMin.Name = "colMin";
            // 
            // colMax
            // 
            colMax.HeaderText = "Max";
            colMax.MinimumWidth = 70;
            colMax.Name = "colMax";
            // 
            // previewPictureBox
            // 
            previewPictureBox.BackColor = Color.White;
            previewPictureBox.BorderStyle = BorderStyle.FixedSingle;
            previewPictureBox.Location = new Point(408, 133);
            previewPictureBox.Margin = new Padding(4, 5, 4, 5);
            previewPictureBox.Name = "previewPictureBox";
            previewPictureBox.Size = new Size(420, 420);
            previewPictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            previewPictureBox.TabIndex = 3;
            previewPictureBox.TabStop = false;
            // 
            // previewButton
            // 
            previewButton.Location = new Point(540, 567);
            previewButton.Margin = new Padding(4, 5, 4, 5);
            previewButton.Name = "previewButton";
            previewButton.Size = new Size(143, 38);
            previewButton.TabIndex = 4;
            previewButton.Text = "Preview";
            previewButton.UseVisualStyleBackColor = true;
            previewButton.Click += previewButton_Click;
            // 
            // applyButton
            // 
            applyButton.Location = new Point(737, 9);
            applyButton.Margin = new Padding(4, 5, 4, 5);
            applyButton.Name = "applyButton";
            applyButton.Size = new Size(107, 38);
            applyButton.TabIndex = 5;
            applyButton.Text = "Применить";
            applyButton.UseVisualStyleBackColor = true;
            applyButton.Click += applyButton_Click;
            // 
            // cancelButton
            // 
            cancelButton.DialogResult = DialogResult.Cancel;
            cancelButton.Location = new Point(622, 9);
            cancelButton.Margin = new Padding(4, 5, 4, 5);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(107, 38);
            cancelButton.TabIndex = 6;
            cancelButton.Text = "Отмена";
            cancelButton.UseVisualStyleBackColor = true;
            // 
            // addClassButton
            // 
            addClassButton.Location = new Point(20, 567);
            addClassButton.Margin = new Padding(4, 5, 4, 5);
            addClassButton.Name = "addClassButton";
            addClassButton.Size = new Size(71, 38);
            addClassButton.TabIndex = 7;
            addClassButton.Text = "+";
            addClassButton.UseVisualStyleBackColor = true;
            addClassButton.Click += addClassButton_Click;
            // 
            // removeClassButton
            // 
            removeClassButton.Location = new Point(100, 567);
            removeClassButton.Margin = new Padding(4, 5, 4, 5);
            removeClassButton.Name = "removeClassButton";
            removeClassButton.Size = new Size(71, 38);
            removeClassButton.TabIndex = 8;
            removeClassButton.Text = "−";
            removeClassButton.UseVisualStyleBackColor = true;
            removeClassButton.Click += removeClassButton_Click;
            // 
            // moveUpButton
            // 
            moveUpButton.Location = new Point(200, 567);
            moveUpButton.Margin = new Padding(4, 5, 4, 5);
            moveUpButton.Name = "moveUpButton";
            moveUpButton.Size = new Size(71, 38);
            moveUpButton.TabIndex = 9;
            moveUpButton.Text = "↑";
            moveUpButton.UseVisualStyleBackColor = true;
            moveUpButton.Click += moveUpButton_Click;
            // 
            // moveDownButton
            // 
            moveDownButton.Location = new Point(280, 567);
            moveDownButton.Margin = new Padding(4, 5, 4, 5);
            moveDownButton.Name = "moveDownButton";
            moveDownButton.Size = new Size(71, 38);
            moveDownButton.TabIndex = 10;
            moveDownButton.Text = "↓";
            moveDownButton.UseVisualStyleBackColor = true;
            moveDownButton.Click += moveDownButton_Click;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.AutoSize = true;
            flowLayoutPanel1.Controls.Add(applyButton);
            flowLayoutPanel1.Controls.Add(cancelButton);
            flowLayoutPanel1.Dock = DockStyle.Bottom;
            flowLayoutPanel1.FlowDirection = FlowDirection.RightToLeft;
            flowLayoutPanel1.Location = new Point(0, 637);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Padding = new Padding(4);
            flowLayoutPanel1.Size = new Size(856, 56);
            flowLayoutPanel1.TabIndex = 11;
            // 
            // ClassifyForm
            // 
            AcceptButton = applyButton;
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = cancelButton;
            ClientSize = new Size(856, 693);
            Controls.Add(flowLayoutPanel1);
            Controls.Add(moveDownButton);
            Controls.Add(moveUpButton);
            Controls.Add(removeClassButton);
            Controls.Add(addClassButton);
            Controls.Add(previewButton);
            Controls.Add(previewPictureBox);
            Controls.Add(classTable);
            Controls.Add(label1);
            Controls.Add(presetComboBox);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Margin = new Padding(4, 5, 4, 5);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "ClassifyForm";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Классификация";
            Load += ClassifyForm_Load;
            ((System.ComponentModel.ISupportInitialize)classTable).EndInit();
            ((System.ComponentModel.ISupportInitialize)previewPictureBox).EndInit();
            flowLayoutPanel1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox presetComboBox;
        private Label label1;
        private DataGridView classTable;
        private DataGridViewImageColumn colColor;
        private DataGridViewTextBoxColumn colClassName;
        private DataGridViewTextBoxColumn colMin;
        private DataGridViewTextBoxColumn colMax;
        private PictureBox previewPictureBox;
        private Button previewButton;
        private Button applyButton;
        private Button cancelButton;
        private Button addClassButton;
        private Button removeClassButton;
        private Button moveUpButton;
        private Button moveDownButton;
        private FlowLayoutPanel flowLayoutPanel1;
    }
}
