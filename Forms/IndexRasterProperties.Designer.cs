namespace vegetation_analyzer.Forms
{
    partial class IndexRasterProperties
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IndexRasterProperties));
            infoTextBox = new TextBox();
            label1 = new Label();
            label3 = new Label();
            label4 = new Label();
            minNumericUpDown = new NumericUpDown();
            maxNumericUpDown = new NumericUpDown();
            interpolationComboBox = new ComboBox();
            applyButton = new Button();
            cancelButton = new Button();
            resetButton = new Button();
            label5 = new Label();
            groupBox1 = new GroupBox();
            tableLayoutPanel1 = new TableLayoutPanel();
            flowLayoutPanel1 = new FlowLayoutPanel();
            tableLayoutPanel2 = new TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)minNumericUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)maxNumericUpDown).BeginInit();
            groupBox1.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            flowLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            SuspendLayout();
            // 
            // infoTextBox
            // 
            resources.ApplyResources(infoTextBox, "infoTextBox");
            infoTextBox.Name = "infoTextBox";
            infoTextBox.ReadOnly = true;
            // 
            // label1
            // 
            resources.ApplyResources(label1, "label1");
            label1.Name = "label1";
            // 
            // label3
            // 
            resources.ApplyResources(label3, "label3");
            label3.Name = "label3";
            // 
            // label4
            // 
            resources.ApplyResources(label4, "label4");
            label4.Name = "label4";
            // 
            // minNumericUpDown
            // 
            minNumericUpDown.DecimalPlaces = 4;
            resources.ApplyResources(minNumericUpDown, "minNumericUpDown");
            minNumericUpDown.Increment = new decimal(new int[] { 1, 0, 0, 131072 });
            minNumericUpDown.Maximum = new decimal(new int[] { 2, 0, 0, 0 });
            minNumericUpDown.Minimum = new decimal(new int[] { 2, 0, 0, int.MinValue });
            minNumericUpDown.Name = "minNumericUpDown";
            // 
            // maxNumericUpDown
            // 
            maxNumericUpDown.DecimalPlaces = 4;
            resources.ApplyResources(maxNumericUpDown, "maxNumericUpDown");
            maxNumericUpDown.Increment = new decimal(new int[] { 1, 0, 0, 131072 });
            maxNumericUpDown.Maximum = new decimal(new int[] { 2, 0, 0, 0 });
            maxNumericUpDown.Minimum = new decimal(new int[] { 2, 0, 0, int.MinValue });
            maxNumericUpDown.Name = "maxNumericUpDown";
            // 
            // interpolationComboBox
            // 
            resources.ApplyResources(interpolationComboBox, "interpolationComboBox");
            interpolationComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            interpolationComboBox.FormattingEnabled = true;
            interpolationComboBox.Name = "interpolationComboBox";
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
            resources.ApplyResources(cancelButton, "cancelButton");
            cancelButton.DialogResult = DialogResult.Cancel;
            cancelButton.Name = "cancelButton";
            cancelButton.UseVisualStyleBackColor = true;
            // 
            // resetButton
            // 
            resources.ApplyResources(resetButton, "resetButton");
            resetButton.Name = "resetButton";
            resetButton.UseVisualStyleBackColor = true;
            resetButton.Click += resetButton_Click;
            // 
            // label5
            // 
            resources.ApplyResources(label5, "label5");
            label5.Name = "label5";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(tableLayoutPanel1);
            groupBox1.Controls.Add(resetButton);
            resources.ApplyResources(groupBox1, "groupBox1");
            groupBox1.Name = "groupBox1";
            groupBox1.TabStop = false;
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(tableLayoutPanel1, "tableLayoutPanel1");
            tableLayoutPanel1.Controls.Add(label3, 0, 0);
            tableLayoutPanel1.Controls.Add(label4, 0, 1);
            tableLayoutPanel1.Controls.Add(minNumericUpDown, 1, 0);
            tableLayoutPanel1.Controls.Add(maxNumericUpDown, 1, 1);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // flowLayoutPanel1
            // 
            resources.ApplyResources(flowLayoutPanel1, "flowLayoutPanel1");
            flowLayoutPanel1.Controls.Add(cancelButton);
            flowLayoutPanel1.Controls.Add(applyButton);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            // 
            // tableLayoutPanel2
            // 
            resources.ApplyResources(tableLayoutPanel2, "tableLayoutPanel2");
            tableLayoutPanel2.Controls.Add(label5, 0, 0);
            tableLayoutPanel2.Controls.Add(interpolationComboBox, 1, 0);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            // 
            // IndexRasterProperties
            // 
            AcceptButton = applyButton;
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = cancelButton;
            Controls.Add(infoTextBox);
            Controls.Add(groupBox1);
            Controls.Add(tableLayoutPanel2);
            Controls.Add(flowLayoutPanel1);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "IndexRasterProperties";
            ShowIcon = false;
            ShowInTaskbar = false;
            Load += IndexRasterProperties_Load;
            ((System.ComponentModel.ISupportInitialize)minNumericUpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)maxNumericUpDown).EndInit();
            groupBox1.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            flowLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox infoTextBox;
        private Label label1;
        private Label label3;
        private Label label4;
        private NumericUpDown minNumericUpDown;
        private NumericUpDown maxNumericUpDown;
        private ComboBox interpolationComboBox;
        private Button applyButton;
        private Button cancelButton;
        private Button resetButton;
        private Label label5;
        private GroupBox groupBox1;
        private TableLayoutPanel tableLayoutPanel1;
        private FlowLayoutPanel flowLayoutPanel1;
        private TableLayoutPanel tableLayoutPanel2;
    }
}
