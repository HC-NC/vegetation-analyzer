namespace vegetation_analyzer.Forms
{
    partial class FolderOpenParamForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            acceptButton = new Button();
            cancelButton = new Button();
            ignoreZeroCheckBox = new CheckBox();
            folderPathTextBox = new TextBox();
            filesDataGridView = new DataGridView();
            selectAllCheckBox = new CheckBox();
            label1 = new Label();
            label2 = new Label();
            colSelected = new DataGridViewCheckBoxColumn();
            colBandName = new DataGridViewTextBoxColumn();
            colFileName = new DataGridViewTextBoxColumn();
            colWidth = new DataGridViewTextBoxColumn();
            colHeight = new DataGridViewTextBoxColumn();
            colMin = new DataGridViewTextBoxColumn();
            colMax = new DataGridViewTextBoxColumn();
            colDescription = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)filesDataGridView).BeginInit();
            SuspendLayout();
            // 
            // acceptButton
            // 
            acceptButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            acceptButton.DialogResult = DialogResult.OK;
            acceptButton.Location = new Point(859, 701);
            acceptButton.Margin = new Padding(4, 5, 4, 5);
            acceptButton.Name = "acceptButton";
            acceptButton.Size = new Size(107, 38);
            acceptButton.TabIndex = 0;
            acceptButton.Text = "Открыть";
            acceptButton.UseVisualStyleBackColor = true;
            acceptButton.Click += acceptButton_Click;
            // 
            // cancelButton
            // 
            cancelButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            cancelButton.DialogResult = DialogResult.Cancel;
            cancelButton.Location = new Point(744, 701);
            cancelButton.Margin = new Padding(4, 5, 4, 5);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(107, 38);
            cancelButton.TabIndex = 1;
            cancelButton.Text = "Отмена";
            cancelButton.UseVisualStyleBackColor = true;
            // 
            // ignoreZeroCheckBox
            // 
            ignoreZeroCheckBox.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            ignoreZeroCheckBox.AutoSize = true;
            ignoreZeroCheckBox.Checked = true;
            ignoreZeroCheckBox.CheckState = CheckState.Checked;
            ignoreZeroCheckBox.Location = new Point(20, 707);
            ignoreZeroCheckBox.Margin = new Padding(4, 5, 4, 5);
            ignoreZeroCheckBox.Name = "ignoreZeroCheckBox";
            ignoreZeroCheckBox.Size = new Size(172, 29);
            ignoreZeroCheckBox.TabIndex = 2;
            ignoreZeroCheckBox.Text = "Игнорировать 0";
            ignoreZeroCheckBox.UseVisualStyleBackColor = true;
            // 
            // folderPathTextBox
            // 
            folderPathTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            folderPathTextBox.Location = new Point(110, 20);
            folderPathTextBox.Margin = new Padding(4, 5, 4, 5);
            folderPathTextBox.Name = "folderPathTextBox";
            folderPathTextBox.ReadOnly = true;
            folderPathTextBox.Size = new Size(854, 31);
            folderPathTextBox.TabIndex = 3;
            // 
            // filesDataGridView
            // 
            filesDataGridView.AllowUserToAddRows = false;
            filesDataGridView.AllowUserToDeleteRows = false;
            filesDataGridView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            filesDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            filesDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            filesDataGridView.Columns.AddRange(new DataGridViewColumn[] { colSelected, colBandName, colFileName, colWidth, colHeight, colMin, colMax, colDescription });
            filesDataGridView.Location = new Point(20, 110);
            filesDataGridView.Margin = new Padding(4, 5, 4, 5);
            filesDataGridView.Name = "filesDataGridView";
            filesDataGridView.RowHeadersVisible = false;
            filesDataGridView.RowHeadersWidth = 62;
            filesDataGridView.RowTemplate.Height = 25;
            filesDataGridView.Size = new Size(946, 575);
            filesDataGridView.TabIndex = 4;
            // 
            // selectAllCheckBox
            // 
            selectAllCheckBox.AutoSize = true;
            selectAllCheckBox.Location = new Point(20, 68);
            selectAllCheckBox.Margin = new Padding(4, 5, 4, 5);
            selectAllCheckBox.Name = "selectAllCheckBox";
            selectAllCheckBox.Size = new Size(197, 29);
            selectAllCheckBox.TabIndex = 5;
            selectAllCheckBox.Text = "Выбрать все файлы";
            selectAllCheckBox.UseVisualStyleBackColor = true;
            selectAllCheckBox.CheckedChanged += selectAllCheckBox_CheckedChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(17, 25);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(66, 25);
            label1.TabIndex = 6;
            label1.Text = "Папка:";
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label2.Location = new Point(225, 69);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(741, 32);
            label2.TabIndex = 7;
            label2.Text = "(Выберите файлы для загрузки как один растр с несколькими каналами)";
            // 
            // colSelected
            // 
            colSelected.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            colSelected.HeaderText = "Выбрать";
            colSelected.MinimumWidth = 60;
            colSelected.Name = "colSelected";
            colSelected.Width = 87;
            // 
            // colBandName
            // 
            colBandName.HeaderText = "Канал";
            colBandName.MinimumWidth = 80;
            colBandName.Name = "colBandName";
            colBandName.ReadOnly = true;
            // 
            // colFileName
            // 
            colFileName.HeaderText = "Файл";
            colFileName.MinimumWidth = 150;
            colFileName.Name = "colFileName";
            colFileName.ReadOnly = true;
            // 
            // colWidth
            // 
            colWidth.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            colWidth.HeaderText = "Ширина";
            colWidth.MinimumWidth = 70;
            colWidth.Name = "colWidth";
            colWidth.ReadOnly = true;
            colWidth.Width = 115;
            // 
            // colHeight
            // 
            colHeight.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            colHeight.HeaderText = "Высота";
            colHeight.MinimumWidth = 70;
            colHeight.Name = "colHeight";
            colHeight.ReadOnly = true;
            colHeight.Width = 106;
            // 
            // colMin
            // 
            colMin.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            colMin.HeaderText = "Min";
            colMin.MinimumWidth = 70;
            colMin.Name = "colMin";
            colMin.ReadOnly = true;
            colMin.Width = 78;
            // 
            // colMax
            // 
            colMax.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            colMax.HeaderText = "Max";
            colMax.MinimumWidth = 70;
            colMax.Name = "colMax";
            colMax.ReadOnly = true;
            colMax.Width = 81;
            // 
            // colDescription
            // 
            colDescription.HeaderText = "Описание";
            colDescription.MinimumWidth = 120;
            colDescription.Name = "colDescription";
            colDescription.ReadOnly = true;
            // 
            // FolderOpenParamForm
            // 
            AcceptButton = acceptButton;
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = cancelButton;
            ClientSize = new Size(986, 758);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(selectAllCheckBox);
            Controls.Add(filesDataGridView);
            Controls.Add(folderPathTextBox);
            Controls.Add(ignoreZeroCheckBox);
            Controls.Add(cancelButton);
            Controls.Add(acceptButton);
            Margin = new Padding(4, 5, 4, 5);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FolderOpenParamForm";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Открытие папки с каналами";
            Load += FolderOpenParamForm_Load;
            ((System.ComponentModel.ISupportInitialize)filesDataGridView).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button acceptButton;
        private Button cancelButton;
        private CheckBox ignoreZeroCheckBox;
        private TextBox folderPathTextBox;
        private DataGridView filesDataGridView;
        private CheckBox selectAllCheckBox;
        private Label label1;
        private Label label2;
        private DataGridViewCheckBoxColumn colSelected;
        private DataGridViewTextBoxColumn colBandName;
        private DataGridViewTextBoxColumn colFileName;
        private DataGridViewTextBoxColumn colWidth;
        private DataGridViewTextBoxColumn colHeight;
        private DataGridViewTextBoxColumn colMin;
        private DataGridViewTextBoxColumn colMax;
        private DataGridViewTextBoxColumn colDescription;
    }
}
