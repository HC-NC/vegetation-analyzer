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
            infoTextBox = new TextBox();
            label1 = new Label();
            label2 = new Label();
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
            flowLayoutPanel1.SuspendLayout();
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
            infoTextBox.Size = new Size(498, 331);
            infoTextBox.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(20, 25);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(197, 25);
            label1.TabIndex = 1;
            label1.Text = "Информация о растре";
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            label2.AutoSize = true;
            label2.Location = new Point(20, 425);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(187, 25);
            label2.TabIndex = 2;
            label2.Text = "Каналы отображения";
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            label3.AutoSize = true;
            label3.Location = new Point(20, 472);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(46, 25);
            label3.TabIndex = 3;
            label3.Text = "Red:";
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            label4.AutoSize = true;
            label4.Location = new Point(19, 518);
            label4.Margin = new Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new Size(62, 25);
            label4.TabIndex = 4;
            label4.Text = "Green:";
            // 
            // redComboBox
            // 
            redComboBox.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            redComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            redComboBox.FormattingEnabled = true;
            redComboBox.Location = new Point(89, 467);
            redComboBox.Margin = new Padding(4, 5, 4, 5);
            redComboBox.Name = "redComboBox";
            redComboBox.Size = new Size(246, 33);
            redComboBox.TabIndex = 5;
            // 
            // greenComboBox
            // 
            greenComboBox.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            greenComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            greenComboBox.FormattingEnabled = true;
            greenComboBox.Location = new Point(89, 515);
            greenComboBox.Margin = new Padding(4, 5, 4, 5);
            greenComboBox.Name = "greenComboBox";
            greenComboBox.Size = new Size(246, 33);
            greenComboBox.TabIndex = 6;
            // 
            // blueComboBox
            // 
            blueComboBox.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            blueComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            blueComboBox.FormattingEnabled = true;
            blueComboBox.Location = new Point(89, 563);
            blueComboBox.Margin = new Padding(4, 5, 4, 5);
            blueComboBox.Name = "blueComboBox";
            blueComboBox.Size = new Size(246, 33);
            blueComboBox.TabIndex = 7;
            // 
            // interpolationComboBox
            // 
            interpolationComboBox.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            interpolationComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            interpolationComboBox.FormattingEnabled = true;
            interpolationComboBox.Items.AddRange(new object[] { "NearestNeighbour" });
            interpolationComboBox.Location = new Point(343, 464);
            interpolationComboBox.Margin = new Padding(4, 5, 4, 5);
            interpolationComboBox.Name = "interpolationComboBox";
            interpolationComboBox.Size = new Size(175, 33);
            interpolationComboBox.TabIndex = 8;
            // 
            // applyButton
            // 
            applyButton.Location = new Point(400, 9);
            applyButton.Margin = new Padding(4, 5, 4, 5);
            applyButton.Name = "applyButton";
            applyButton.Size = new Size(125, 38);
            applyButton.TabIndex = 9;
            applyButton.Text = "Применить";
            applyButton.UseVisualStyleBackColor = true;
            applyButton.Click += applyButton_Click;
            // 
            // cancelButton
            // 
            cancelButton.DialogResult = DialogResult.Cancel;
            cancelButton.Location = new Point(267, 9);
            cancelButton.Margin = new Padding(4, 5, 4, 5);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(125, 38);
            cancelButton.TabIndex = 10;
            cancelButton.Text = "Отмена";
            cancelButton.UseVisualStyleBackColor = true;
            cancelButton.Click += cancelButton_Click;
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            label5.AutoSize = true;
            label5.Location = new Point(343, 425);
            label5.Margin = new Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new Size(131, 25);
            label5.TabIndex = 11;
            label5.Text = "Интерполяция";
            // 
            // label6
            // 
            label6.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            label6.AutoSize = true;
            label6.Location = new Point(20, 566);
            label6.Margin = new Padding(4, 0, 4, 0);
            label6.Name = "label6";
            label6.Size = new Size(49, 25);
            label6.TabIndex = 12;
            label6.Text = "Blue:";
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.AutoSize = true;
            flowLayoutPanel1.Controls.Add(applyButton);
            flowLayoutPanel1.Controls.Add(cancelButton);
            flowLayoutPanel1.Dock = DockStyle.Bottom;
            flowLayoutPanel1.Location = new Point(0, 627);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Padding = new Padding(4);
            flowLayoutPanel1.RightToLeft = RightToLeft.Yes;
            flowLayoutPanel1.Size = new Size(537, 56);
            flowLayoutPanel1.TabIndex = 13;
            // 
            // RasterProperties
            // 
            AcceptButton = applyButton;
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = cancelButton;
            ClientSize = new Size(537, 683);
            Controls.Add(flowLayoutPanel1);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(interpolationComboBox);
            Controls.Add(blueComboBox);
            Controls.Add(greenComboBox);
            Controls.Add(redComboBox);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(infoTextBox);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Margin = new Padding(4, 5, 4, 5);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "RasterProperties";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Свойства растра";
            Load += RasterProperties_Load;
            flowLayoutPanel1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox infoTextBox;
        private Label label1;
        private Label label2;
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
    }
}
