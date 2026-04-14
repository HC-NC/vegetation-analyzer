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
            infoTextBox = new TextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            minNumericUpDown = new NumericUpDown();
            maxNumericUpDown = new NumericUpDown();
            interpolationComboBox = new ComboBox();
            applyButton = new Button();
            cancelButton = new Button();
            resetButton = new Button();
            label5 = new Label();
            ((System.ComponentModel.ISupportInitialize)minNumericUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)maxNumericUpDown).BeginInit();
            SuspendLayout();
            // 
            // infoTextBox
            // 
            infoTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            infoTextBox.Location = new Point(20, 68);
            infoTextBox.Margin = new Padding(4, 5, 4, 5);
            infoTextBox.Multiline = true;
            infoTextBox.Name = "infoTextBox";
            infoTextBox.ReadOnly = true;
            infoTextBox.ScrollBars = ScrollBars.Vertical;
            infoTextBox.Size = new Size(455, 247);
            infoTextBox.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(20, 25);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(217, 25);
            label1.TabIndex = 1;
            label1.Text = "Информация об индексе";
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            label2.AutoSize = true;
            label2.Location = new Point(20, 342);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(206, 25);
            label2.TabIndex = 2;
            label2.Text = "Диапазон отображения";
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            label3.AutoSize = true;
            label3.Location = new Point(20, 388);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(46, 25);
            label3.TabIndex = 3;
            label3.Text = "Min:";
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            label4.AutoSize = true;
            label4.Location = new Point(20, 437);
            label4.Margin = new Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new Size(49, 25);
            label4.TabIndex = 4;
            label4.Text = "Max:";
            // 
            // minNumericUpDown
            //
            minNumericUpDown.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            minNumericUpDown.DecimalPlaces = 4;
            minNumericUpDown.Increment = 0.01m;
            minNumericUpDown.Location = new Point(79, 385);
            minNumericUpDown.Margin = new Padding(4, 5, 4, 5);
            minNumericUpDown.Maximum = 2m;
            minNumericUpDown.Minimum = -2m;
            minNumericUpDown.Name = "minNumericUpDown";
            minNumericUpDown.Size = new Size(171, 31);
            minNumericUpDown.TabIndex = 5;
            //
            // maxNumericUpDown
            //
            maxNumericUpDown.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            maxNumericUpDown.DecimalPlaces = 4;
            maxNumericUpDown.Increment = 0.01m;
            maxNumericUpDown.Location = new Point(79, 433);
            maxNumericUpDown.Margin = new Padding(4, 5, 4, 5);
            maxNumericUpDown.Maximum = 2m;
            maxNumericUpDown.Minimum = -2m;
            maxNumericUpDown.Name = "maxNumericUpDown";
            maxNumericUpDown.Size = new Size(171, 31);
            maxNumericUpDown.TabIndex = 6;
            // 
            // interpolationComboBox
            // 
            interpolationComboBox.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            interpolationComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            interpolationComboBox.FormattingEnabled = true;
            interpolationComboBox.Location = new Point(286, 385);
            interpolationComboBox.Margin = new Padding(4, 5, 4, 5);
            interpolationComboBox.Name = "interpolationComboBox";
            interpolationComboBox.Size = new Size(190, 33);
            interpolationComboBox.TabIndex = 7;
            // 
            // applyButton
            // 
            applyButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            applyButton.Location = new Point(350, 492);
            applyButton.Margin = new Padding(4, 5, 4, 5);
            applyButton.Name = "applyButton";
            applyButton.Size = new Size(125, 38);
            applyButton.TabIndex = 8;
            applyButton.Text = "Применить";
            applyButton.UseVisualStyleBackColor = true;
            applyButton.Click += applyButton_Click;
            // 
            // cancelButton
            // 
            cancelButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            cancelButton.DialogResult = DialogResult.Cancel;
            cancelButton.Location = new Point(217, 492);
            cancelButton.Margin = new Padding(4, 5, 4, 5);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(125, 38);
            cancelButton.TabIndex = 9;
            cancelButton.Text = "Отмена";
            cancelButton.UseVisualStyleBackColor = true;
            // 
            // resetButton
            // 
            resetButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            resetButton.Location = new Point(79, 492);
            resetButton.Margin = new Padding(4, 5, 4, 5);
            resetButton.Name = "resetButton";
            resetButton.Size = new Size(107, 38);
            resetButton.TabIndex = 10;
            resetButton.Text = "Сброс";
            resetButton.UseVisualStyleBackColor = true;
            resetButton.Click += resetButton_Click;
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            label5.AutoSize = true;
            label5.Location = new Point(286, 342);
            label5.Margin = new Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new Size(131, 25);
            label5.TabIndex = 11;
            label5.Text = "Интерполяция";
            // 
            // IndexRasterProperties
            // 
            AcceptButton = applyButton;
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = cancelButton;
            ClientSize = new Size(497, 550);
            Controls.Add(label5);
            Controls.Add(resetButton);
            Controls.Add(cancelButton);
            Controls.Add(applyButton);
            Controls.Add(interpolationComboBox);
            Controls.Add(maxNumericUpDown);
            Controls.Add(minNumericUpDown);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(infoTextBox);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Margin = new Padding(4, 5, 4, 5);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "IndexRasterProperties";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Свойства индекса";
            Load += IndexRasterProperties_Load;
            ((System.ComponentModel.ISupportInitialize)minNumericUpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)maxNumericUpDown).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox infoTextBox;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private NumericUpDown minNumericUpDown;
        private NumericUpDown maxNumericUpDown;
        private ComboBox interpolationComboBox;
        private Button applyButton;
        private Button cancelButton;
        private Button resetButton;
        private Label label5;
    }
}
