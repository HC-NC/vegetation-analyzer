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
            infoLabel = new Label();
            label2 = new Label();
            pathTextBox = new TextBox();
            browseButton = new Button();
            label3 = new Label();
            label4 = new Label();
            formatComboBox = new ComboBox();
            compressionComboBox = new ComboBox();
            includePaletteCheckBox = new CheckBox();
            exportAsByteCheckBox = new CheckBox();
            exportButton = new Button();
            cancelButton = new Button();
            saveFileDialog1 = new SaveFileDialog();
            SuspendLayout();
            // 
            // infoLabel
            // 
            infoLabel.AutoSize = true;
            infoLabel.Location = new Point(14, 15);
            infoLabel.Name = "infoLabel";
            infoLabel.Size = new Size(0, 15);
            infoLabel.TabIndex = 0;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(14, 50);
            label2.Name = "label2";
            label2.Size = new Size(31, 15);
            label2.TabIndex = 1;
            label2.Text = "Путь:";
            // 
            // pathTextBox
            // 
            pathTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            pathTextBox.Location = new Point(55, 47);
            pathTextBox.Name = "pathTextBox";
            pathTextBox.ReadOnly = true;
            pathTextBox.Size = new Size(320, 23);
            pathTextBox.TabIndex = 2;
            // 
            // browseButton
            // 
            browseButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            browseButton.Location = new Point(381, 46);
            browseButton.Name = "browseButton";
            browseButton.Size = new Size(30, 23);
            browseButton.TabIndex = 3;
            browseButton.Text = "...";
            browseButton.UseVisualStyleBackColor = true;
            browseButton.Click += browseButton_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(14, 85);
            label3.Name = "label3";
            label3.Size = new Size(59, 15);
            label3.TabIndex = 4;
            label3.Text = "Формат:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(14, 115);
            label4.Name = "label4";
            label4.Size = new Size(74, 15);
            label4.TabIndex = 5;
            label4.Text = "Компрессия:";
            // 
            // formatComboBox
            // 
            formatComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            formatComboBox.FormattingEnabled = true;
            formatComboBox.Location = new Point(100, 82);
            formatComboBox.Name = "formatComboBox";
            formatComboBox.Size = new Size(120, 23);
            formatComboBox.TabIndex = 6;
            // 
            // compressionComboBox
            // 
            compressionComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            compressionComboBox.FormattingEnabled = true;
            compressionComboBox.Location = new Point(100, 112);
            compressionComboBox.Name = "compressionComboBox";
            compressionComboBox.Size = new Size(120, 23);
            compressionComboBox.TabIndex = 7;
            // 
            // includePaletteCheckBox
            // 
            includePaletteCheckBox.AutoSize = true;
            includePaletteCheckBox.Location = new Point(14, 150);
            includePaletteCheckBox.Name = "includePaletteCheckBox";
            includePaletteCheckBox.Size = new Size(143, 19);
            includePaletteCheckBox.TabIndex = 8;
            includePaletteCheckBox.Text = "Включить палитру";
            includePaletteCheckBox.UseVisualStyleBackColor = true;
            // 
            // exportAsByteCheckBox
            // 
            exportAsByteCheckBox.AutoSize = true;
            exportAsByteCheckBox.Location = new Point(14, 175);
            exportAsByteCheckBox.Name = "exportAsByteCheckBox";
            exportAsByteCheckBox.Size = new Size(189, 19);
            exportAsByteCheckBox.TabIndex = 9;
            exportAsByteCheckBox.Text = "Экспортировать как Byte (8-bit)";
            exportAsByteCheckBox.UseVisualStyleBackColor = true;
            // 
            // exportButton
            // 
            exportButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            exportButton.Location = new Point(245, 210);
            exportButton.Name = "exportButton";
            exportButton.Size = new Size(75, 23);
            exportButton.TabIndex = 10;
            exportButton.Text = "Экспорт";
            exportButton.UseVisualStyleBackColor = true;
            exportButton.Click += exportButton_Click;
            // 
            // cancelButton
            // 
            cancelButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            cancelButton.DialogResult = DialogResult.Cancel;
            cancelButton.Location = new Point(326, 210);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(75, 23);
            cancelButton.TabIndex = 11;
            cancelButton.Text = "Отмена";
            cancelButton.UseVisualStyleBackColor = true;
            // 
            // saveFileDialog1
            // 
            saveFileDialog1.AddExtension = true;
            saveFileDialog1.DefaultExt = "tif";
            // 
            // ExportForm
            // 
            AcceptButton = exportButton;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = cancelButton;
            ClientSize = new Size(423, 245);
            Controls.Add(cancelButton);
            Controls.Add(exportButton);
            Controls.Add(exportAsByteCheckBox);
            Controls.Add(includePaletteCheckBox);
            Controls.Add(compressionComboBox);
            Controls.Add(formatComboBox);
            Controls.Add(label4);
            Controls.Add(label3);
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
            StartPosition = FormStartPosition.CenterParent;
            Text = "Экспорт в файл";
            Load += ExportForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label infoLabel;
        private Label label2;
        private TextBox pathTextBox;
        private Button browseButton;
        private Label label3;
        private Label label4;
        private ComboBox formatComboBox;
        private ComboBox compressionComboBox;
        private CheckBox includePaletteCheckBox;
        private CheckBox exportAsByteCheckBox;
        private Button exportButton;
        private Button cancelButton;
        private SaveFileDialog saveFileDialog1;
    }
}
