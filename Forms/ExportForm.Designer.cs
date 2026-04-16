namespace vegetation_analyzer.Forms
{
    partial class ExportForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExportForm));
            infoLabel = new Label();
            label2 = new Label();
            pathTextBox = new TextBox();
            browseButton = new Button();
            label4 = new Label();
            compressionComboBox = new ComboBox();
            includePaletteCheckBox = new CheckBox();
            exportAsByteCheckBox = new CheckBox();
            exportButton = new Button();
            cancelButton = new Button();
            saveFileDialog1 = new SaveFileDialog();
            flowLayoutPanel1 = new FlowLayoutPanel();
            flowLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // infoLabel
            // 
            resources.ApplyResources(infoLabel, "infoLabel");
            infoLabel.Name = "infoLabel";
            // 
            // label2
            // 
            resources.ApplyResources(label2, "label2");
            label2.Name = "label2";
            // 
            // pathTextBox
            // 
            resources.ApplyResources(pathTextBox, "pathTextBox");
            pathTextBox.Name = "pathTextBox";
            pathTextBox.ReadOnly = true;
            // 
            // browseButton
            // 
            resources.ApplyResources(browseButton, "browseButton");
            browseButton.Name = "browseButton";
            browseButton.UseVisualStyleBackColor = true;
            browseButton.Click += browseButton_Click;
            // 
            // label4
            // 
            resources.ApplyResources(label4, "label4");
            label4.Name = "label4";
            // 
            // compressionComboBox
            // 
            compressionComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            compressionComboBox.FormattingEnabled = true;
            resources.ApplyResources(compressionComboBox, "compressionComboBox");
            compressionComboBox.Name = "compressionComboBox";
            // 
            // includePaletteCheckBox
            // 
            resources.ApplyResources(includePaletteCheckBox, "includePaletteCheckBox");
            includePaletteCheckBox.Name = "includePaletteCheckBox";
            includePaletteCheckBox.UseVisualStyleBackColor = true;
            // 
            // exportAsByteCheckBox
            // 
            resources.ApplyResources(exportAsByteCheckBox, "exportAsByteCheckBox");
            exportAsByteCheckBox.Name = "exportAsByteCheckBox";
            exportAsByteCheckBox.UseVisualStyleBackColor = true;
            // 
            // exportButton
            // 
            resources.ApplyResources(exportButton, "exportButton");
            exportButton.Name = "exportButton";
            exportButton.UseVisualStyleBackColor = true;
            exportButton.Click += exportButton_Click;
            // 
            // cancelButton
            // 
            resources.ApplyResources(cancelButton, "cancelButton");
            cancelButton.DialogResult = DialogResult.Cancel;
            cancelButton.Name = "cancelButton";
            cancelButton.UseVisualStyleBackColor = true;
            // 
            // saveFileDialog1
            // 
            saveFileDialog1.DefaultExt = "tif";
            // 
            // flowLayoutPanel1
            // 
            resources.ApplyResources(flowLayoutPanel1, "flowLayoutPanel1");
            flowLayoutPanel1.Controls.Add(cancelButton);
            flowLayoutPanel1.Controls.Add(exportButton);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            // 
            // ExportForm
            // 
            AcceptButton = exportButton;
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = cancelButton;
            Controls.Add(flowLayoutPanel1);
            Controls.Add(exportAsByteCheckBox);
            Controls.Add(includePaletteCheckBox);
            Controls.Add(compressionComboBox);
            Controls.Add(label4);
            Controls.Add(browseButton);
            Controls.Add(pathTextBox);
            Controls.Add(label2);
            Controls.Add(infoLabel);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "ExportForm";
            ShowIcon = false;
            ShowInTaskbar = false;
            Load += ExportForm_Load;
            flowLayoutPanel1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label infoLabel;
        private Label label2;
        private TextBox pathTextBox;
        private Button browseButton;
        private Label label4;
        private ComboBox compressionComboBox;
        private CheckBox includePaletteCheckBox;
        private CheckBox exportAsByteCheckBox;
        private Button exportButton;
        private Button cancelButton;
        private SaveFileDialog saveFileDialog1;
        private FlowLayoutPanel flowLayoutPanel1;
    }
}
