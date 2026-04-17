namespace vegetation_analyzer.Forms
{
    partial class Main
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            splitContainer1 = new SplitContainer();
            treeView1 = new TreeView();
            contextMenuStrip1 = new ContextMenuStrip(components);
            computeIndexToolStripMenuItem = new ToolStripMenuItem();
            classifyToolStripMenuItem = new ToolStripMenuItem();
            exportToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator2 = new ToolStripSeparator();
            propertiesToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator3 = new ToolStripSeparator();
            removeToolStripMenuItem = new ToolStripMenuItem();
            viewport = new Viewport();
            menuStrip1 = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            openFileToolStripMenuItem = new ToolStripMenuItem();
            openFolderToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            exitToolStripMenuItem = new ToolStripMenuItem();
            aboutToolStripMenuItem = new ToolStripMenuItem();
            statusStrip1 = new StatusStrip();
            toolStripStatusLabel1 = new ToolStripStatusLabel();
            toolStripProgressBar1 = new ToolStripProgressBar();
            openFileDialog1 = new OpenFileDialog();
            folderBrowserDialog1 = new FolderBrowserDialog();
            openBackgroundWorker = new System.ComponentModel.BackgroundWorker();
            computeIndexBackgroundWorker = new System.ComponentModel.BackgroundWorker();
            classifyBackgroundWorker = new System.ComponentModel.BackgroundWorker();
            exportBackgroundWorker = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            contextMenuStrip1.SuspendLayout();
            menuStrip1.SuspendLayout();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // splitContainer1
            // 
            resources.ApplyResources(splitContainer1, "splitContainer1");
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(treeView1);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(viewport);
            // 
            // treeView1
            // 
            treeView1.ContextMenuStrip = contextMenuStrip1;
            resources.ApplyResources(treeView1, "treeView1");
            treeView1.Name = "treeView1";
            treeView1.AfterSelect += treeView_AfterSelect;
            treeView1.NodeMouseClick += treeView1_NodeMouseClick;
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.ImageScalingSize = new Size(24, 24);
            contextMenuStrip1.Items.AddRange(new ToolStripItem[] { computeIndexToolStripMenuItem, classifyToolStripMenuItem, exportToolStripMenuItem, toolStripSeparator2, propertiesToolStripMenuItem, toolStripSeparator3, removeToolStripMenuItem });
            contextMenuStrip1.Name = "contextMenuStrip1";
            resources.ApplyResources(contextMenuStrip1, "contextMenuStrip1");
            contextMenuStrip1.Opening += contextMenuStrip1_Opening;
            // 
            // computeIndexToolStripMenuItem
            // 
            computeIndexToolStripMenuItem.Image = Properties.Resources.icon_trees;
            computeIndexToolStripMenuItem.Name = "computeIndexToolStripMenuItem";
            resources.ApplyResources(computeIndexToolStripMenuItem, "computeIndexToolStripMenuItem");
            computeIndexToolStripMenuItem.Click += computeIndexToolStripMenuItem_Click;
            // 
            // classifyToolStripMenuItem
            // 
            classifyToolStripMenuItem.Image = Properties.Resources.icon_classification;
            classifyToolStripMenuItem.Name = "classifyToolStripMenuItem";
            resources.ApplyResources(classifyToolStripMenuItem, "classifyToolStripMenuItem");
            classifyToolStripMenuItem.Click += classifyToolStripMenuItem_Click;
            // 
            // exportToolStripMenuItem
            // 
            exportToolStripMenuItem.Image = Properties.Resources.icon_disk;
            exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            resources.ApplyResources(exportToolStripMenuItem, "exportToolStripMenuItem");
            exportToolStripMenuItem.Click += exportToolStripMenuItem_Click;
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            resources.ApplyResources(toolStripSeparator2, "toolStripSeparator2");
            // 
            // propertiesToolStripMenuItem
            // 
            propertiesToolStripMenuItem.Image = Properties.Resources.icon_info;
            propertiesToolStripMenuItem.Name = "propertiesToolStripMenuItem";
            resources.ApplyResources(propertiesToolStripMenuItem, "propertiesToolStripMenuItem");
            propertiesToolStripMenuItem.Click += propertiesToolStripMenuItem_Click;
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.Name = "toolStripSeparator3";
            resources.ApplyResources(toolStripSeparator3, "toolStripSeparator3");
            // 
            // removeToolStripMenuItem
            // 
            removeToolStripMenuItem.Image = Properties.Resources.icon_trash;
            removeToolStripMenuItem.Name = "removeToolStripMenuItem";
            resources.ApplyResources(removeToolStripMenuItem, "removeToolStripMenuItem");
            removeToolStripMenuItem.Click += removeToolStripMenuItem_Click;
            // 
            // viewport
            // 
            resources.ApplyResources(viewport, "viewport");
            viewport.Name = "viewport";
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(24, 24);
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, aboutToolStripMenuItem });
            resources.ApplyResources(menuStrip1, "menuStrip1");
            menuStrip1.Name = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { openFileToolStripMenuItem, openFolderToolStripMenuItem, toolStripSeparator1, exitToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            resources.ApplyResources(fileToolStripMenuItem, "fileToolStripMenuItem");
            // 
            // openFileToolStripMenuItem
            // 
            openFileToolStripMenuItem.Image = Properties.Resources.icon_add_image;
            openFileToolStripMenuItem.Name = "openFileToolStripMenuItem";
            resources.ApplyResources(openFileToolStripMenuItem, "openFileToolStripMenuItem");
            openFileToolStripMenuItem.Click += openFileToolStripMenuItem_Click;
            // 
            // openFolderToolStripMenuItem
            // 
            openFolderToolStripMenuItem.Image = Properties.Resources.icon_add_folder;
            openFolderToolStripMenuItem.Name = "openFolderToolStripMenuItem";
            resources.ApplyResources(openFolderToolStripMenuItem, "openFolderToolStripMenuItem");
            openFolderToolStripMenuItem.Click += openFolderToolStripMenuItem_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(toolStripSeparator1, "toolStripSeparator1");
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Image = Properties.Resources.icon_power;
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            resources.ApplyResources(exitToolStripMenuItem, "exitToolStripMenuItem");
            exitToolStripMenuItem.Click += exitToolStripMenuItem_Click;
            // 
            // aboutToolStripMenuItem
            // 
            aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            resources.ApplyResources(aboutToolStripMenuItem, "aboutToolStripMenuItem");
            aboutToolStripMenuItem.Click += aboutToolStripMenuItem_Click;
            // 
            // statusStrip1
            // 
            statusStrip1.ImageScalingSize = new Size(24, 24);
            statusStrip1.Items.AddRange(new ToolStripItem[] { toolStripStatusLabel1, toolStripProgressBar1 });
            resources.ApplyResources(statusStrip1, "statusStrip1");
            statusStrip1.Name = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            resources.ApplyResources(toolStripStatusLabel1, "toolStripStatusLabel1");
            // 
            // toolStripProgressBar1
            // 
            toolStripProgressBar1.Name = "toolStripProgressBar1";
            resources.ApplyResources(toolStripProgressBar1, "toolStripProgressBar1");
            // 
            // openBackgroundWorker
            // 
            openBackgroundWorker.WorkerReportsProgress = true;
            openBackgroundWorker.DoWork += openBackgroundWorker_DoWork;
            openBackgroundWorker.ProgressChanged += BackgroundWorker_ProgressChanged;
            openBackgroundWorker.RunWorkerCompleted += openBackgroundWorker_RunWorkerCompleted;
            // 
            // computeIndexBackgroundWorker
            // 
            computeIndexBackgroundWorker.DoWork += computeIndexBackgroundWorker_DoWork;
            computeIndexBackgroundWorker.RunWorkerCompleted += computeIndexBackgroundWorker_RunWorkerCompleted;
            // 
            // classifyBackgroundWorker
            // 
            classifyBackgroundWorker.DoWork += classifyBackgroundWorker_DoWork;
            classifyBackgroundWorker.RunWorkerCompleted += classifyBackgroundWorker_RunWorkerCompleted;
            // 
            // exportBackgroundWorker
            // 
            exportBackgroundWorker.DoWork += exportBackgroundWorker_DoWork;
            exportBackgroundWorker.RunWorkerCompleted += exportBackgroundWorker_RunWorkerCompleted;
            // 
            // Main
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(splitContainer1);
            Controls.Add(statusStrip1);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "Main";
            Load += Main_Load;
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            contextMenuStrip1.ResumeLayout(false);
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem exitToolStripMenuItem;
        private StatusStrip statusStrip1;
        private SplitContainer splitContainer1;
        private Viewport viewport;
        private TreeView treeView1;
        private ToolStripMenuItem aboutToolStripMenuItem;
        private ToolStripMenuItem openFileToolStripMenuItem;
        private ToolStripMenuItem openFolderToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private ToolStripProgressBar toolStripProgressBar1;
        private OpenFileDialog openFileDialog1;
        private FolderBrowserDialog folderBrowserDialog1;
        private System.ComponentModel.BackgroundWorker openBackgroundWorker;
        private System.ComponentModel.BackgroundWorker computeIndexBackgroundWorker;
        private System.ComponentModel.BackgroundWorker classifyBackgroundWorker;
        private System.ComponentModel.BackgroundWorker exportBackgroundWorker;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem computeIndexToolStripMenuItem;
        private ToolStripMenuItem classifyToolStripMenuItem;
        private ToolStripMenuItem exportToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripMenuItem propertiesToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripMenuItem removeToolStripMenuItem;
    }
}
