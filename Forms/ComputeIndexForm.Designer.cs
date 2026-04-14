namespace vegetation_analyzer.Forms
{
    partial class ComputeIndexForm
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
            indexListBox = new ListBox();
            descriptionTextBox = new TextBox();
            bandMappingPanel = new Panel();
            acceptButton = new Button();
            cancelButton = new Button();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            SuspendLayout();
            // 
            // indexListBox
            // 
            indexListBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            indexListBox.FormattingEnabled = true;
            indexListBox.Location = new Point(20, 68);
            indexListBox.Margin = new Padding(4, 5, 4, 5);
            indexListBox.Name = "indexListBox";
            indexListBox.Size = new Size(227, 454);
            indexListBox.TabIndex = 0;
            indexListBox.SelectedIndexChanged += indexListBox_SelectedIndexChanged;
            // 
            // descriptionTextBox
            // 
            descriptionTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            descriptionTextBox.Location = new Point(271, 68);
            descriptionTextBox.Margin = new Padding(4, 5, 4, 5);
            descriptionTextBox.Multiline = true;
            descriptionTextBox.Name = "descriptionTextBox";
            descriptionTextBox.ReadOnly = true;
            descriptionTextBox.ScrollBars = ScrollBars.Vertical;
            descriptionTextBox.Size = new Size(498, 164);
            descriptionTextBox.TabIndex = 1;
            // 
            // bandMappingPanel
            // 
            bandMappingPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            bandMappingPanel.AutoScroll = true;
            bandMappingPanel.BorderStyle = BorderStyle.FixedSingle;
            bandMappingPanel.Location = new Point(271, 267);
            bandMappingPanel.Margin = new Padding(4, 5, 4, 5);
            bandMappingPanel.Name = "bandMappingPanel";
            bandMappingPanel.Size = new Size(499, 258);
            bandMappingPanel.TabIndex = 2;
            // 
            // acceptButton
            // 
            acceptButton.Location = new Point(663, 542);
            acceptButton.Margin = new Padding(4, 5, 4, 5);
            acceptButton.Name = "acceptButton";
            acceptButton.Size = new Size(107, 38);
            acceptButton.TabIndex = 3;
            acceptButton.Text = "Вычислить";
            acceptButton.UseVisualStyleBackColor = true;
            acceptButton.Click += acceptButton_Click;
            // 
            // cancelButton
            // 
            cancelButton.DialogResult = DialogResult.Cancel;
            cancelButton.Location = new Point(548, 542);
            cancelButton.Margin = new Padding(4, 5, 4, 5);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(107, 38);
            cancelButton.TabIndex = 4;
            cancelButton.Text = "Отмена";
            cancelButton.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(20, 25);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(199, 25);
            label1.TabIndex = 5;
            label1.Text = "Вегетационный индекс";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(271, 25);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(92, 25);
            label2.TabIndex = 6;
            label2.Text = "Описание";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(271, 237);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(194, 25);
            label3.TabIndex = 7;
            label3.Text = "Соответствие каналов";
            // 
            // ComputeIndexForm
            // 
            AcceptButton = acceptButton;
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = cancelButton;
            ClientSize = new Size(791, 600);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(cancelButton);
            Controls.Add(acceptButton);
            Controls.Add(bandMappingPanel);
            Controls.Add(descriptionTextBox);
            Controls.Add(indexListBox);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Margin = new Padding(4, 5, 4, 5);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "ComputeIndexForm";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Вычисление вегетационного индекса";
            Load += ComputeIndexForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListBox indexListBox;
        private TextBox descriptionTextBox;
        private Panel bandMappingPanel;
        private Button acceptButton;
        private Button cancelButton;
        private Label label1;
        private Label label2;
        private Label label3;
    }
}
