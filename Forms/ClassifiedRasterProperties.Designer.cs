namespace vegetation_analyzer.Forms
{
    partial class ClassifiedRasterProperties
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ClassifiedRasterProperties));
            infoTextBox = new TextBox();
            label1 = new Label();
            label2 = new Label();
            interpolationComboBox = new ComboBox();
            applyButton = new Button();
            cancelButton = new Button();
            flowLayoutPanel1 = new FlowLayoutPanel();
            tableLayoutPanel1 = new TableLayoutPanel();
            flowLayoutPanel1.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
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
            // label2
            // 
            resources.ApplyResources(label2, "label2");
            label2.Name = "label2";
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
            // 
            // flowLayoutPanel1
            // 
            resources.ApplyResources(flowLayoutPanel1, "flowLayoutPanel1");
            flowLayoutPanel1.Controls.Add(cancelButton);
            flowLayoutPanel1.Controls.Add(applyButton);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(tableLayoutPanel1, "tableLayoutPanel1");
            tableLayoutPanel1.Controls.Add(label2, 0, 0);
            tableLayoutPanel1.Controls.Add(interpolationComboBox, 1, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // ClassifiedRasterProperties
            // 
            AcceptButton = applyButton;
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = cancelButton;
            Controls.Add(infoTextBox);
            Controls.Add(tableLayoutPanel1);
            Controls.Add(flowLayoutPanel1);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "ClassifiedRasterProperties";
            ShowIcon = false;
            ShowInTaskbar = false;
            Load += ClassifiedRasterProperties_Load;
            flowLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox infoTextBox;
        private Label label1;
        private Label label2;
        private ComboBox interpolationComboBox;
        private Button applyButton;
        private Button cancelButton;
        private FlowLayoutPanel flowLayoutPanel1;
        private TableLayoutPanel tableLayoutPanel1;
    }
}
