namespace vegetation_analyzer.Forms
{
    partial class RasterProperties
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RasterProperties));
            infoTextBox = new TextBox();
            label1 = new Label();
            label3 = new Label();
            label4 = new Label();
            redComboBox = new ComboBox();
            greenComboBox = new ComboBox();
            blueComboBox = new ComboBox();
            interpolationComboBox = new ComboBox();
            applyButton = new Button();
            cancelButton = new Button();
            label5 = new Label();
            label6 = new Label();
            flowLayoutPanel1 = new FlowLayoutPanel();
            groupBox1 = new GroupBox();
            tableLayoutPanel1 = new TableLayoutPanel();
            tableLayoutPanel2 = new TableLayoutPanel();
            flowLayoutPanel1.SuspendLayout();
            groupBox1.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
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
            // redComboBox
            // 
            resources.ApplyResources(redComboBox, "redComboBox");
            redComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            redComboBox.FormattingEnabled = true;
            redComboBox.Name = "redComboBox";
            // 
            // greenComboBox
            // 
            resources.ApplyResources(greenComboBox, "greenComboBox");
            greenComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            greenComboBox.FormattingEnabled = true;
            greenComboBox.Name = "greenComboBox";
            // 
            // blueComboBox
            // 
            resources.ApplyResources(blueComboBox, "blueComboBox");
            blueComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            blueComboBox.FormattingEnabled = true;
            blueComboBox.Name = "blueComboBox";
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
            cancelButton.DialogResult = DialogResult.Cancel;
            resources.ApplyResources(cancelButton, "cancelButton");
            cancelButton.Name = "cancelButton";
            cancelButton.UseVisualStyleBackColor = true;
            cancelButton.Click += cancelButton_Click;
            // 
            // label5
            // 
            resources.ApplyResources(label5, "label5");
            label5.Name = "label5";
            // 
            // label6
            // 
            resources.ApplyResources(label6, "label6");
            label6.Name = "label6";
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
            groupBox1.Controls.Add(tableLayoutPanel1);
            resources.ApplyResources(groupBox1, "groupBox1");
            groupBox1.Name = "groupBox1";
            groupBox1.TabStop = false;
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(tableLayoutPanel1, "tableLayoutPanel1");
            tableLayoutPanel1.Controls.Add(label3, 0, 0);
            tableLayoutPanel1.Controls.Add(redComboBox, 1, 0);
            tableLayoutPanel1.Controls.Add(label6, 0, 2);
            tableLayoutPanel1.Controls.Add(label4, 0, 1);
            tableLayoutPanel1.Controls.Add(blueComboBox, 1, 2);
            tableLayoutPanel1.Controls.Add(greenComboBox, 1, 1);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // tableLayoutPanel2
            // 
            resources.ApplyResources(tableLayoutPanel2, "tableLayoutPanel2");
            tableLayoutPanel2.Controls.Add(label5, 0, 0);
            tableLayoutPanel2.Controls.Add(interpolationComboBox, 1, 0);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            // 
            // RasterProperties
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
            Name = "RasterProperties";
            ShowIcon = false;
            ShowInTaskbar = false;
            Load += RasterProperties_Load;
            flowLayoutPanel1.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
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
        private ComboBox redComboBox;
        private ComboBox greenComboBox;
        private ComboBox blueComboBox;
        private ComboBox interpolationComboBox;
        private Button applyButton;
        private Button cancelButton;
        private Label label5;
        private Label label6;
        private FlowLayoutPanel flowLayoutPanel1;
        private GroupBox groupBox1;
        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel tableLayoutPanel2;
    }
}
