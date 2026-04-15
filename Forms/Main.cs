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
                    case IndexRaster indexRaster:
                        viewport.UpdateImage(indexRaster.GetBitmap(), indexRaster.InterpolationMode);
                        break;
                    case ClassifiedRaster classified:
                        viewport.UpdateImage(classified.GetBitmap(), classified.InterpolationMode);
                        break;
                    case BandData _:
                        if (node.Parent?.Tag is RasterData rData)
                            viewport.UpdateImage(rData.GetBitmap(), rData.InterpolationMode);
                        else if (node.Parent?.Tag is IndexRaster idxRaster)
                            viewport.UpdateImage(idxRaster.GetBitmap(), idxRaster.InterpolationMode);
                        break;
                    default:
                        viewport.UpdateImage(null);
                        break;
                }
            }
            else
                viewport.UpdateImage(null);
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            if (treeView1.SelectedNode?.Tag is RasterData)
            {
                computeIndexToolStripMenuItem.Visible = true;
                classifyToolStripMenuItem.Visible = false;
                toolStripSeparator2.Visible = true;
                propertiesToolStripMenuItem.Visible = true;
                toolStripSeparator3.Visible = true;
                removeToolStripMenuItem.Visible = true;
            }
            else if (treeView1.SelectedNode?.Tag is IndexRaster)
            {
                computeIndexToolStripMenuItem.Visible = false;
                classifyToolStripMenuItem.Visible = true;
                toolStripSeparator2.Visible = true;
                propertiesToolStripMenuItem.Visible = true;
                toolStripSeparator3.Visible = true;
                removeToolStripMenuItem.Visible = true;
            }
            else if (treeView1.SelectedNode?.Tag is ClassifiedRaster)
            {
                computeIndexToolStripMenuItem.Visible = false;
                classifyToolStripMenuItem.Visible = false;
                toolStripSeparator2.Visible = false;
                propertiesToolStripMenuItem.Visible = true;
                toolStripSeparator3.Visible = true;
                removeToolStripMenuItem.Visible = true;
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void classifyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode?.Tag is not IndexRaster indexRaster) return;

            ClassifyForm classifyForm = new ClassifyForm(indexRaster);
            classifyForm.Location = Point.Subtract(Point.Add(Location, Size / 2), classifyForm.Size / 2);

            if (classifyForm.ShowDialog() == DialogResult.OK)
            {
                var scheme = classifyForm.GetScheme();

                toolStripStatusLabel1.Text = $"Classifying: {scheme.Name}";
                toolStripProgressBar1.Visible = true;
                toolStripStatusLabel1.Visible = true;

                classifyBackgroundWorker.RunWorkerAsync(Tuple.Create(indexRaster, scheme));
            }
        }

        private void classifyBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            if (e.Argument is not Tuple<IndexRaster, ClassificationScheme> args) return;

            var (indexRaster, scheme) = args;
            var classified = ClassifiedRaster.Classify(indexRaster, scheme);
            e.Result = Tuple.Create(classified, indexRaster, scheme);
        }

        private void classifyBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show(this, $"Ошибка классификации: {e.Error.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (e.Result is Tuple<ClassifiedRaster, IndexRaster, ClassificationScheme> result)
            {
                var (classified, sourceIndex, scheme) = result;
                AddClassifiedNode(classified, sourceIndex);
            }

            toolStripProgressBar1.Value = 0;
            toolStripProgressBar1.Visible = false;
            toolStripStatusLabel1.Visible = false;
        }

        private void AddClassifiedNode(ClassifiedRaster classified, IndexRaster sourceIndex)
        {
            // Ищем узел IndexRaster рекурсивно по всему дереву
            TreeNode? FindIndexNode(TreeNodeCollection nodes)
            {
                foreach (TreeNode n in nodes)
                {
                    if (n.Tag == sourceIndex) return n;
                    var found = FindIndexNode(n.Nodes);
                    if (found != null) return found;
                }
                return null;
            }

            var parentNode = FindIndexNode(treeView1.Nodes);
            if (parentNode == null) return;

            TreeNode? existingNode = null;
            foreach (TreeNode child in parentNode.Nodes)
            {
                if (child.Tag is ClassifiedRaster cr && cr.Scheme.Name == classified.Scheme.Name)
                {
                    existingNode = child;
                    break;
                }
            }

            if (existingNode != null)
            {
                ((ClassifiedRaster)existingNode.Tag).Dispose();
                existingNode.Tag = classified;
                existingNode.Text = classified.ToString();
                treeView1.SelectedNode = existingNode;
                return;
            }

            TreeNode classNode = new TreeNode(classified.ToString())
            {
                Tag = classified,
                ToolTipText = $"{classified.Scheme.Name} ({classified.Scheme.Classes.Count} classes)"
            };
            parentNode.Nodes.Add(classNode);
            treeView1.SelectedNode = classNode;
        }

        private void computeIndexToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode?.Tag is not RasterData raster) return;

            ComputeIndexForm computeForm = new ComputeIndexForm(raster);
            computeForm.Location = Point.Subtract(Point.Add(Location, Size / 2), computeForm.Size / 2);

            if (computeForm.ShowDialog() == DialogResult.OK)
            {
                VegetationIndex indexType = computeForm.SelectedIndex;
                Dictionary<SpectralBandRole, int> bandMapping = computeForm.GetBandMapping();

                toolStripStatusLabel1.Text = $"Computing: {IndexDefinition.GetName(indexType)}";
                toolStripProgressBar1.Visible = true;
                toolStripStatusLabel1.Visible = true;

                computeIndexBackgroundWorker.RunWorkerAsync(Tuple.Create(raster, indexType, bandMapping));
            }
        }

        private void computeIndexBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            if (e.Argument is not Tuple<RasterData, VegetationIndex, Dictionary<SpectralBandRole, int>> args) return;

            var (raster, indexType, bandMapping) = args;
            IndexRaster indexRaster = IndexRaster.Compute(raster, indexType, bandMapping);
            e.Result = Tuple.Create(indexRaster, raster);
        }

        private void computeIndexBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show(this, $"Ошибка вычисления индекса: {e.Error.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (e.Result is Tuple<IndexRaster, RasterData> result)
            {
                var (indexRaster, sourceRaster) = result;
                AddIndexNode(indexRaster, sourceRaster);
            }

            toolStripProgressBar1.Value = 0;
            toolStripProgressBar1.Visible = false;
            toolStripStatusLabel1.Visible = false;
        }

        private void AddIndexNode(IndexRaster indexRaster, RasterData sourceRaster)
        {
            foreach (TreeNode node in treeView1.Nodes)
            {
                if (node.Tag == sourceRaster)
                {
                    TreeNode? existingNode = null;
                    foreach (TreeNode child in node.Nodes)
                    {
                        if (child.Tag is IndexRaster ir && ir.IndexType == indexRaster.IndexType)
                        {
                            existingNode = child;
                            break;
                        }
                    }

                    // Если есть — заменяем
                    if (existingNode != null)
                    {
                        ((IndexRaster)existingNode.Tag).Dispose();
                        existingNode.Tag = indexRaster;
                        existingNode.Text = indexRaster.ToString();
                        treeView1.SelectedNode = existingNode;
                        return;
                    }

                    // Создаём новый узел
                    TreeNode indexNode = new TreeNode(indexRaster.ToString())
                    {
                        Tag = indexRaster,
                        ToolTipText = $"{IndexDefinition.GetName(indexRaster.IndexType)}: [{indexRaster.Minimum:F3} ... {indexRaster.Maximum:F3}]"
                    };
                    node.Nodes.Add(indexNode);
                    treeView1.SelectedNode = indexNode;
                    return;
                }
            }
        }

        private void propertiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode?.Tag is RasterData raster)
            {
                RasterProperties propsForm = new RasterProperties(raster);
                propsForm.Location = Point.Subtract(Point.Add(Location, Size / 2), propsForm.Size / 2);

                if (propsForm.ShowDialog() == DialogResult.OK)
                {
                    viewport.UpdateImage(raster.GetBitmap(), raster.InterpolationMode);
                    RefreshTreeNodeNames(treeView1.SelectedNode);
                }
            }
            else if (treeView1.SelectedNode?.Tag is IndexRaster indexRaster)
            {
                IndexRasterProperties propsForm = new IndexRasterProperties(indexRaster);
                propsForm.Location = Point.Subtract(Point.Add(Location, Size / 2), propsForm.Size / 2);

                if (propsForm.ShowDialog() == DialogResult.OK)
                {
                    viewport.UpdateImage(indexRaster.GetBitmap(), indexRaster.InterpolationMode);
                }
            }
            else if (treeView1.SelectedNode?.Tag is ClassifiedRaster classified)
            {
                ClassifiedRasterProperties propsForm = new ClassifiedRasterProperties(classified);
                propsForm.Location = Point.Subtract(Point.Add(Location, Size / 2), propsForm.Size / 2);

                if (propsForm.ShowDialog() == DialogResult.OK)
                {
                    viewport.UpdateImage(classified.GetBitmap(), classified.InterpolationMode);
                }
            }
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode? node = treeView1.SelectedNode;
            if (node?.Tag is RasterData raster)
            {
                var result = MessageBox.Show(this, $"Удалить растр \"{raster.Name}\"?", "Подтверждение",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result != DialogResult.Yes) return;

                viewport.UpdateImage(null);
                raster.Dispose();
                treeView1.Nodes.Remove(node);

                if (treeView1.Nodes.Count > 0)
                    treeView1.SelectedNode = treeView1.Nodes[0];
                else
                    viewport.UpdateImage(null);
            }
            else if (node?.Tag is IndexRaster indexRaster)
            {
                var result = MessageBox.Show(this, $"Удалить индекс \"{indexRaster.Name}\"?", "Подтверждение",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result != DialogResult.Yes) return;

                viewport.UpdateImage(null);
                indexRaster.Dispose();
                treeView1.Nodes.Remove(node);

                if (treeView1.Nodes.Count > 0)
                    treeView1.SelectedNode = treeView1.Nodes[0];
                else
                    viewport.UpdateImage(null);
            }
            else if (node?.Tag is ClassifiedRaster classifiedRaster)
            {
                var result = MessageBox.Show(this, $"Удалить классификацию \"{classifiedRaster.Name}\"?", "Подтверждение",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result != DialogResult.Yes) return;

                viewport.UpdateImage(null);
                classifiedRaster.Dispose();
                treeView1.Nodes.Remove(node);

                if (treeView1.Nodes.Count > 0)
                    treeView1.SelectedNode = treeView1.Nodes[0];
                else
                    viewport.UpdateImage(null);
            }
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
