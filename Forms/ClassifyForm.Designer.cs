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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ClassifyForm));
            classTable = new DataGridView();
            colColor = new DataGridViewImageColumn();
            colClassName = new DataGridViewTextBoxColumn();
            colMin = new DataGridViewTextBoxColumn();
            colMax = new DataGridViewTextBoxColumn();
            previewButton = new Button();
            applyButton = new Button();
            cancelButton = new Button();
            flowLayoutPanel1 = new FlowLayoutPanel();
            groupBox1 = new GroupBox();
            tableLayoutPanel1 = new TableLayoutPanel();
            presetComboBox = new ComboBox();
            createEmptyButton = new Button();
            savePresetButton = new Button();
            toolStrip2 = new ToolStrip();
            addClassToolStripButton = new ToolStripButton();
            removeClassToolStripButton = new ToolStripButton();
            moveDownToolStripButton = new ToolStripButton();
            moveUpToolStripButton = new ToolStripButton();
            splitContainer1 = new SplitContainer();
            previewPictureBox = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)classTable).BeginInit();
            flowLayoutPanel1.SuspendLayout();
            groupBox1.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            toolStrip2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)previewPictureBox).BeginInit();
            SuspendLayout();
            // 
            // classTable
            // 
            classTable.AllowUserToAddRows = false;
            classTable.AllowUserToDeleteRows = false;
            classTable.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            classTable.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            classTable.Columns.AddRange(new DataGridViewColumn[] { colColor, colClassName, colMin, colMax });
            resources.ApplyResources(classTable, "classTable");
            classTable.Name = "classTable";
            classTable.RowHeadersVisible = false;
            classTable.RowTemplate.Height = 25;
            classTable.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            classTable.CellContentClick += classTable_CellContentClick;
            classTable.CellEndEdit += classTable_CellEndEdit;
            // 
            // colColor
            // 
            resources.ApplyResources(colColor, "colColor");
            colColor.ImageLayout = DataGridViewImageCellLayout.Stretch;
            colColor.Name = "colColor";
            colColor.ReadOnly = true;
            // 
            // colClassName
            // 
            resources.ApplyResources(colClassName, "colClassName");
            colClassName.Name = "colClassName";
            // 
            // colMin
            // 
            resources.ApplyResources(colMin, "colMin");
            colMin.Name = "colMin";
            // 
            // colMax
            // 
            resources.ApplyResources(colMax, "colMax");
            colMax.Name = "colMax";
            // 
            // previewButton
            // 
            resources.ApplyResources(previewButton, "previewButton");
            previewButton.Name = "previewButton";
            previewButton.UseVisualStyleBackColor = true;
            previewButton.Click += previewButton_Click;
            // 
            // applyButton
            // 
            resources.ApplyResources(applyButton, "applyButton");
            applyButton.Name = "applyButton";
            applyButton.UseVisualStyleBackColor = true;
            applyButton.Click += applyButton_Click;
            // 
            // cancelButton
            // 
            cancelButton.DialogResult = DialogResult.Cancel;
            resources.ApplyResources(cancelButton, "cancelButton");
            cancelButton.Name = "cancelButton";
            cancelButton.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanel1
            // 
            resources.ApplyResources(flowLayoutPanel1, "flowLayoutPanel1");
            flowLayoutPanel1.Controls.Add(cancelButton);
            flowLayoutPanel1.Controls.Add(applyButton);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(classTable);
            groupBox1.Controls.Add(tableLayoutPanel1);
            groupBox1.Controls.Add(toolStrip2);
            resources.ApplyResources(groupBox1, "groupBox1");
            groupBox1.Name = "groupBox1";
            groupBox1.TabStop = false;
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(tableLayoutPanel1, "tableLayoutPanel1");
            tableLayoutPanel1.Controls.Add(presetComboBox, 0, 0);
            tableLayoutPanel1.Controls.Add(createEmptyButton, 1, 0);
            tableLayoutPanel1.Controls.Add(savePresetButton, 2, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // presetComboBox
            // 
            resources.ApplyResources(presetComboBox, "presetComboBox");
            presetComboBox.FormattingEnabled = true;
            presetComboBox.Name = "presetComboBox";
            presetComboBox.SelectedIndexChanged += presetComboBox_SelectedIndexChanged;
            // 
            // createEmptyButton
            // 
            resources.ApplyResources(createEmptyButton, "createEmptyButton");
            createEmptyButton.Name = "createEmptyButton";
            createEmptyButton.UseVisualStyleBackColor = true;
            createEmptyButton.Click += createEmptyButton_Click;
            // 
            // savePresetButton
            // 
            resources.ApplyResources(savePresetButton, "savePresetButton");
            savePresetButton.Name = "savePresetButton";
            savePresetButton.UseVisualStyleBackColor = true;
            savePresetButton.Click += savePresetButton_Click;
            // 
            // toolStrip2
            // 
            resources.ApplyResources(toolStrip2, "toolStrip2");
            toolStrip2.GripStyle = ToolStripGripStyle.Hidden;
            toolStrip2.ImageScalingSize = new Size(24, 24);
            toolStrip2.Items.AddRange(new ToolStripItem[] { addClassToolStripButton, removeClassToolStripButton, moveDownToolStripButton, moveUpToolStripButton });
            toolStrip2.Name = "toolStrip2";
            // 
            // addClassToolStripButton
            // 
            addClassToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
            resources.ApplyResources(addClassToolStripButton, "addClassToolStripButton");
            addClassToolStripButton.Name = "addClassToolStripButton";
            addClassToolStripButton.Click += addClassButton_Click;
            // 
            // removeClassToolStripButton
            // 
            removeClassToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
            resources.ApplyResources(removeClassToolStripButton, "removeClassToolStripButton");
            removeClassToolStripButton.Name = "removeClassToolStripButton";
            removeClassToolStripButton.Click += removeClassButton_Click;
            // 
            // moveDownToolStripButton
            // 
            moveDownToolStripButton.Alignment = ToolStripItemAlignment.Right;
            moveDownToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
            resources.ApplyResources(moveDownToolStripButton, "moveDownToolStripButton");
            moveDownToolStripButton.Name = "moveDownToolStripButton";
            moveDownToolStripButton.Click += moveDownButton_Click;
            // 
            // moveUpToolStripButton
            // 
            moveUpToolStripButton.Alignment = ToolStripItemAlignment.Right;
            moveUpToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
            resources.ApplyResources(moveUpToolStripButton, "moveUpToolStripButton");
            moveUpToolStripButton.Name = "moveUpToolStripButton";
            moveUpToolStripButton.Click += moveUpButton_Click;
            // 
            // splitContainer1
            // 
            resources.ApplyResources(splitContainer1, "splitContainer1");
            splitContainer1.FixedPanel = FixedPanel.Panel1;
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(groupBox1);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(previewPictureBox);
            splitContainer1.Panel2.Controls.Add(previewButton);
            // 
            // previewPictureBox
            // 
            resources.ApplyResources(previewPictureBox, "previewPictureBox");
            previewPictureBox.Name = "previewPictureBox";
            previewPictureBox.TabStop = false;
            // 
            // ClassifyForm
            // 
            AcceptButton = applyButton;
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = cancelButton;
            Controls.Add(splitContainer1);
            Controls.Add(flowLayoutPanel1);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "ClassifyForm";
            ShowIcon = false;
            ShowInTaskbar = false;
            Load += ClassifyForm_Load;
            ((System.ComponentModel.ISupportInitialize)classTable).EndInit();
            flowLayoutPanel1.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            tableLayoutPanel1.ResumeLayout(false);
            toolStrip2.ResumeLayout(false);
            toolStrip2.PerformLayout();
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)previewPictureBox).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private DataGridView classTable;
        private DataGridViewImageColumn colColor;
        private DataGridViewTextBoxColumn colClassName;
        private DataGridViewTextBoxColumn colMin;
        private DataGridViewTextBoxColumn colMax;
        private Button previewButton;
        private Button applyButton;
        private Button cancelButton;
        private FlowLayoutPanel flowLayoutPanel1;
        private GroupBox groupBox1;
        private ToolStrip toolStrip2;
        private ToolStripButton addClassToolStripButton;
        private ToolStrip toolStrip1;
        private ToolStripButton removeClassToolStripButton;
        private ToolStripButton moveDownToolStripButton;
        private ToolStripButton moveUpToolStripButton;
        private TableLayoutPanel tableLayoutPanel1;
        private ComboBox presetComboBox;
        private Button createEmptyButton;
        private Button savePresetButton;
        private SplitContainer splitContainer1;
        private PictureBox previewPictureBox;
    }
}
