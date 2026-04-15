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
            infoTextBox = new TextBox();
            label1 = new Label();
            label2 = new Label();
            interpolationComboBox = new ComboBox();
            applyButton = new Button();
            cancelButton = new Button();
            SuspendLayout();
            // 
            // infoTextBox
            // 
            infoTextBox.Location = new Point(14, 41);
            infoTextBox.Multiline = true;
            infoTextBox.Name = "infoTextBox";
            infoTextBox.ReadOnly = true;
            infoTextBox.ScrollBars = ScrollBars.Vertical;
            infoTextBox.Size = new Size(350, 200);
            infoTextBox.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(14, 15);
            label1.Name = "label1";
            label1.Size = new Size(130, 15);
            label1.TabIndex = 1;
            label1.Text = "Информация о классиф.";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(14, 255);
            label2.Name = "label2";
            label2.Size = new Size(76, 15);
            label2.TabIndex = 2;
            label2.Text = "Интерполяция";
            // 
            // interpolationComboBox
            // 
            interpolationComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            interpolationComboBox.FormattingEnabled = true;
            interpolationComboBox.Location = new Point(96, 252);
            interpolationComboBox.Name = "interpolationComboBox";
            interpolationComboBox.Size = new Size(180, 23);
            interpolationComboBox.TabIndex = 3;
            // 
            // applyButton
            // 
            applyButton.Location = new Point(208, 285);
            applyButton.Name = "applyButton";
            applyButton.Size = new Size(75, 23);
            applyButton.TabIndex = 4;
            applyButton.Text = "Применить";
            applyButton.UseVisualStyleBackColor = true;
            applyButton.Click += applyButton_Click;
            // 
            // cancelButton
            // 
            cancelButton.DialogResult = DialogResult.Cancel;
            cancelButton.Location = new Point(289, 285);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(75, 23);
            cancelButton.TabIndex = 5;
            cancelButton.Text = "Отмена";
            cancelButton.UseVisualStyleBackColor = true;
            // 
            // ClassifiedRasterProperties
            // 
            AcceptButton = applyButton;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = cancelButton;
            ClientSize = new Size(378, 320);
            Controls.Add(cancelButton);
            Controls.Add(applyButton);
            Controls.Add(interpolationComboBox);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(infoTextBox);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "ClassifiedRasterProperties";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Свойства классификации";
            Load += ClassifiedRasterProperties_Load;
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
    }
}
