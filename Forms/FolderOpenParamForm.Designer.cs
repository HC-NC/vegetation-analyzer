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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FolderOpenParamForm));
            acceptButton = new Button();
            cancelButton = new Button();
            ignoreZeroCheckBox = new CheckBox();
            folderPathTextBox = new TextBox();
            filesDataGridView = new DataGridView();
            colSelected = new DataGridViewCheckBoxColumn();
            colBandName = new DataGridViewTextBoxColumn();
            colFileName = new DataGridViewTextBoxColumn();
            colWidth = new DataGridViewTextBoxColumn();
            colHeight = new DataGridViewTextBoxColumn();
            colMin = new DataGridViewTextBoxColumn();
            colMax = new DataGridViewTextBoxColumn();
            colDescription = new DataGridViewTextBoxColumn();
            selectAllCheckBox = new CheckBox();
            label1 = new Label();
            label2 = new Label();
            flowLayoutPanel1 = new FlowLayoutPanel();
            panel1 = new Panel();
            ((System.ComponentModel.ISupportInitialize)filesDataGridView).BeginInit();
            flowLayoutPanel1.SuspendLayout();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // acceptButton
            // 
            resources.ApplyResources(acceptButton, "acceptButton");
            acceptButton.DialogResult = DialogResult.OK;
            acceptButton.Name = "acceptButton";
            acceptButton.UseVisualStyleBackColor = true;
            acceptButton.Click += acceptButton_Click;
            // 
            // cancelButton
            // 
            resources.ApplyResources(cancelButton, "cancelButton");
            cancelButton.DialogResult = DialogResult.Cancel;
            cancelButton.Name = "cancelButton";
            cancelButton.UseVisualStyleBackColor = true;
            // 
            // ignoreZeroCheckBox
            // 
            resources.ApplyResources(ignoreZeroCheckBox, "ignoreZeroCheckBox");
            ignoreZeroCheckBox.Checked = true;
            ignoreZeroCheckBox.CheckState = CheckState.Checked;
            ignoreZeroCheckBox.Name = "ignoreZeroCheckBox";
            ignoreZeroCheckBox.UseVisualStyleBackColor = true;
            // 
            // folderPathTextBox
            // 
            resources.ApplyResources(folderPathTextBox, "folderPathTextBox");
            folderPathTextBox.Name = "folderPathTextBox";
            folderPathTextBox.ReadOnly = true;
            // 
            // filesDataGridView
            // 
            filesDataGridView.AllowUserToAddRows = false;
            filesDataGridView.AllowUserToDeleteRows = false;
            filesDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            filesDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            filesDataGridView.Columns.AddRange(new DataGridViewColumn[] { colSelected, colBandName, colFileName, colWidth, colHeight, colMin, colMax, colDescription });
            resources.ApplyResources(filesDataGridView, "filesDataGridView");
            filesDataGridView.Name = "filesDataGridView";
            filesDataGridView.RowHeadersVisible = false;
            filesDataGridView.RowTemplate.Height = 25;
            // 
            // colSelected
            // 
            colSelected.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            resources.ApplyResources(colSelected, "colSelected");
            colSelected.Name = "colSelected";
            // 
            // colBandName
            // 
            resources.ApplyResources(colBandName, "colBandName");
            colBandName.Name = "colBandName";
            colBandName.ReadOnly = true;
            // 
            // colFileName
            // 
            resources.ApplyResources(colFileName, "colFileName");
            colFileName.Name = "colFileName";
            colFileName.ReadOnly = true;
            // 
            // colWidth
            // 
            colWidth.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            resources.ApplyResources(colWidth, "colWidth");
            colWidth.Name = "colWidth";
            colWidth.ReadOnly = true;
            // 
            // colHeight
            // 
            colHeight.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            resources.ApplyResources(colHeight, "colHeight");
            colHeight.Name = "colHeight";
            colHeight.ReadOnly = true;
            // 
            // colMin
            // 
            colMin.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            resources.ApplyResources(colMin, "colMin");
            colMin.Name = "colMin";
            colMin.ReadOnly = true;
            // 
            // colMax
            // 
            colMax.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            resources.ApplyResources(colMax, "colMax");
            colMax.Name = "colMax";
            colMax.ReadOnly = true;
            // 
            // colDescription
            // 
            resources.ApplyResources(colDescription, "colDescription");
            colDescription.Name = "colDescription";
            colDescription.ReadOnly = true;
            // 
            // selectAllCheckBox
            // 
            resources.ApplyResources(selectAllCheckBox, "selectAllCheckBox");
            selectAllCheckBox.Name = "selectAllCheckBox";
            selectAllCheckBox.UseVisualStyleBackColor = true;
            selectAllCheckBox.CheckedChanged += selectAllCheckBox_CheckedChanged;
            // 
            // label1
            // 
            resources.ApplyResources(label1, "label1");
            label1.Name = "label1";
            // 
            // label2
            // 
            resources.ApplyResources(label2, "label2");
            label2.Name = "label2";
            // 
            // flowLayoutPanel1
            // 
            resources.ApplyResources(flowLayoutPanel1, "flowLayoutPanel1");
            flowLayoutPanel1.Controls.Add(cancelButton);
            flowLayoutPanel1.Controls.Add(acceptButton);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            // 
            // panel1
            // 
            resources.ApplyResources(panel1, "panel1");
            panel1.Controls.Add(label1);
            panel1.Controls.Add(folderPathTextBox);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(ignoreZeroCheckBox);
            panel1.Controls.Add(selectAllCheckBox);
            panel1.Name = "panel1";
            // 
            // FolderOpenParamForm
            // 
            AcceptButton = acceptButton;
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = cancelButton;
            Controls.Add(filesDataGridView);
            Controls.Add(panel1);
            Controls.Add(flowLayoutPanel1);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FolderOpenParamForm";
            ShowIcon = false;
            ShowInTaskbar = false;
            Load += FolderOpenParamForm_Load;
            ((System.ComponentModel.ISupportInitialize)filesDataGridView).EndInit();
            flowLayoutPanel1.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
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
        private FlowLayoutPanel flowLayoutPanel1;
        private Panel panel1;
    }
}
