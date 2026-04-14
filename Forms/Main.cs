using Histogram_Contrast_Corrector;
using OSGeo.GDAL;
using System.ComponentModel;
using System.Resources;
using vegetation_analyzer.DataClasses;

namespace vegetation_analyzer.Forms
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();

            Gdal.SetCacheMax(128 * 1024 * 1024);

            openFileDialog1.Filter = "All files|*.tif;*.img;*.png;*.jpg;*.gif|TIFF|*.tif|IMG|*.img|PNG|*.png|JPEG|*.jpg|GIF|*.gif";
        }

        private void Main_Load(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Visible = false;
            toolStripProgressBar1.Visible = false;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox aboutBox = new AboutBox();

            if (aboutBox.ShowDialog() == DialogResult.OK)
                return;
        }

        private void openFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openBackgroundWorker.IsBusy) return;

            if (openFileDialog1.ShowDialog(this) == DialogResult.OK)
            {
                toolStripStatusLabel1.Text = string.Format("Opening: {0}", openFileDialog1.SafeFileName);

                toolStripProgressBar1.Visible = true;
                toolStripStatusLabel1.Visible = true;

                openBackgroundWorker.RunWorkerAsync(Tuple.Create(true, openFileDialog1.FileName));
            }
        }

        private void openFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openBackgroundWorker.IsBusy) return;

            if (folderBrowserDialog1.ShowDialog(this) == DialogResult.OK)
            {
                toolStripStatusLabel1.Text = string.Format("Analyzing folder: {0}", Path.GetFileName(folderBrowserDialog1.SelectedPath));

                toolStripProgressBar1.Visible = true;
                toolStripStatusLabel1.Visible = true;

                openBackgroundWorker.RunWorkerAsync(Tuple.Create(false, folderBrowserDialog1.SelectedPath));
            }
        }

        private void BackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            toolStripProgressBar1.Value = e.ProgressPercentage;
            toolStripStatusLabel1.Text = e.UserState?.ToString();
        }

        private void openBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            if (sender is not BackgroundWorker worker || e.Argument is not Tuple<bool, string> args) return;

            if (args.Item1)
            {
                // Открытие одного файла
                string safeName = Path.GetFileName(args.Item2);

                FileOpenParamForm openParamForm = new FileOpenParamForm(safeName, Path.GetDirectoryName(args.Item2) ?? "");
                openParamForm.Location = Point.Subtract(Point.Add(Location, Size / 2), openParamForm.Size / 2);

                if (openParamForm.ShowDialog() == DialogResult.OK)
                {
                    worker.ReportProgress(50, string.Format("Opening: {0}", safeName));

                    bool ignoreZero = openParamForm.IgnoreZero;

                    RasterData raster = RasterData.LoadFile(args.Item2, safeName, ignoreZero);
                    e.Result = raster;

                    worker.ReportProgress(100);
                }
            }
            else
            {
                // Открытие папки с каналами
                string folderPath = args.Item2;
                string folderName = Path.GetFileName(folderPath);

                FolderOpenParamForm folderParamForm = new FolderOpenParamForm(folderPath);
                folderParamForm.Location = Point.Subtract(Point.Add(Location, Size / 2), folderParamForm.Size / 2);

                if (folderParamForm.ShowDialog() == DialogResult.OK)
                {
                    worker.ReportProgress(50, string.Format("Opening: {0}", folderName));

                    bool ignoreZero = folderParamForm.IgnoreZero;
                    List<BandFileInfo> selectedBands = folderParamForm.SelectedBands;

                    RasterData raster = RasterData.LoadFolder(selectedBands, folderName, folderPath, ignoreZero);
                    e.Result = raster;

                    worker.ReportProgress(100);
                }
            }
        }

        private void openBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error == null && e.Result != null)
                AddTreeNode(e.Result);

            toolStripProgressBar1.Value = 0;
            toolStripProgressBar1.Visible = false;
            toolStripStatusLabel1.Visible = false;
        }

        private void AddTreeNode(object obj)
        {
            if (obj == null) return;

            TreeNode node = new TreeNode(obj.ToString());
            node.Tag = obj;

            if (obj is RasterData rasterData)
            {
                node.ToolTipText = string.Format("{0}\\{1}", rasterData.Path, rasterData.Name);

                for (int i = 0; i < rasterData.BandsCount; i++)
                {
                    BandData? band = rasterData.GetBand(i);
                    if (band == null) continue;

                    TreeNode bandNode = new TreeNode(band.Name);
                    bandNode.Tag = band;
                    node.Nodes.Add(bandNode);
                }
            }

            treeView1.Nodes.Add(node);
            treeView1.SelectedNode = node;
        }

        private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (sender is not TreeView treeView) return;

            if (treeView.SelectedNode is TreeNode node)
            {
                switch (node.Tag)
                {
                    case RasterData rasterData:
                        viewport.UpdateImage(rasterData.GetBitmap(), rasterData.InterpolationMode);
                        break;
                    case BandData _:
                        if (node.Parent?.Tag is RasterData rData)
                            viewport.UpdateImage(rData.GetBitmap(), rData.InterpolationMode);
                        break;
                    default:
                        viewport.UpdateImage(null);
                        break;
                }
            }
            else
                viewport.UpdateImage(null);
        }

        private void contextMenuStrip1_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Показываем контекстное меню только если выбран RasterData или его дочерний узел
            if (treeView1.SelectedNode?.Tag is RasterData)
            {
                propertiesToolStripMenuItem.Visible = true;
                toolStripSeparator2.Visible = true;
                removeToolStripMenuItem.Visible = true;
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void propertiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode?.Tag is not RasterData raster) return;

            RasterProperties propsForm = new RasterProperties(raster);
            propsForm.Location = Point.Subtract(Point.Add(Location, Size / 2), propsForm.Size / 2);

            if (propsForm.ShowDialog() == DialogResult.OK)
            {
                // Обновляем viewport с новыми настройками
                viewport.UpdateImage(raster.GetBitmap(), raster.InterpolationMode);

                // Обновляем имена узлов в дереве, если каналы изменились
                RefreshTreeNodeNames(treeView1.SelectedNode);
            }
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode?.Tag is not RasterData raster) return;

            var result = MessageBox.Show(this, $"Удалить растр \"{raster.Name}\"?", "Подтверждение",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result != DialogResult.Yes) return;

            // Если удаляемый растр сейчас отображается — очищаем viewport
            viewport.UpdateImage(null);

            // Освобождаем ресурсы
            raster.Dispose();

            // Удаляем узел из дерева
            TreeNode node = treeView1.SelectedNode;
            treeView1.Nodes.Remove(node);

            // Сбрасываем выделение
            if (treeView1.Nodes.Count > 0)
                treeView1.SelectedNode = treeView1.Nodes[0];
            else
                viewport.UpdateImage(null);
        }

        private void RefreshTreeNodeNames(TreeNode node)
        {
            if (node.Tag is not RasterData raster) return;

            // Обновляем текст родительского узла
            node.Text = raster.ToString();

            // Обновляем имена дочерних узлов (каналов)
            for (int i = 0; i < raster.BandsCount && i < node.Nodes.Count; i++)
            {
                BandData? band = raster.GetBand(i);
                if (band != null)
                    node.Nodes[i].Text = band.Name;
            }
        }
    }
}
