using Histogram_Contrast_Corrector;
using OSGeo.GDAL;
using System.ComponentModel;
using vegetation_analyzer.DataClasses;
using vegetation_analyzer.Properties;

namespace vegetation_analyzer.Forms
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();

            Gdal.SetCacheMax(128 * 1024 * 1024);

            openFileDialog1.Filter = "All files|*.tif;*.img;*.png;*.jpg;*.gif|TIFF|*.tif;*.tiff|IMG|*.img|PNG|*.png|JPEG|*.jpg|GIF|*.gif";
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
                toolStripStatusLabel1.Text = string.Format(Resources.Opening, openFileDialog1.SafeFileName);

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
                toolStripStatusLabel1.Text = string.Format(Resources.AnalyzingFolder, Path.GetFileName(folderBrowserDialog1.SelectedPath));

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
                    worker.ReportProgress(50, string.Format(Resources.Opening, safeName));

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
                    worker.ReportProgress(50, string.Format(Resources.Opening, folderName));

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
                exportToolStripMenuItem.Visible = true;
                toolStripSeparator2.Visible = true;
                propertiesToolStripMenuItem.Visible = true;
                toolStripSeparator3.Visible = true;
                removeToolStripMenuItem.Visible = true;
            }
            else if (treeView1.SelectedNode?.Tag is IndexRaster)
            {
                computeIndexToolStripMenuItem.Visible = false;
                classifyToolStripMenuItem.Visible = true;
                exportToolStripMenuItem.Visible = true;
                toolStripSeparator2.Visible = true;
                propertiesToolStripMenuItem.Visible = true;
                toolStripSeparator3.Visible = true;
                removeToolStripMenuItem.Visible = true;
            }
            else if (treeView1.SelectedNode?.Tag is ClassifiedRaster)
            {
                computeIndexToolStripMenuItem.Visible = false;
                classifyToolStripMenuItem.Visible = false;
                exportToolStripMenuItem.Visible = true;
                toolStripSeparator2.Visible = true;
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

                toolStripStatusLabel1.Text = string.Format(Resources.Classifying, scheme.Name);
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
                MessageBox.Show(this, string.Format(Resources.ErrorClassification, e.Error.Message), Resources.Error,
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
                ToolTipText = $"{classified.Scheme.Name} ({classified.Scheme.Classes.Count} {Resources.Classes})"
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

                toolStripStatusLabel1.Text = string.Format(Resources.Computing, IndexDefinition.GetName(indexType));
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
                MessageBox.Show(this, string.Format(Resources.ErrorCalculatingIndex, e.Error.Message), Resources.Error,
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
                var result = MessageBox.Show(this, string.Format(Resources.DeleteRaster, raster.Name), Resources.Confirmation,
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
                var result = MessageBox.Show(this, string.Format(Resources.DeleteIndex, indexRaster.Name), Resources.Confirmation,
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
                var result = MessageBox.Show(this, string.Format(Resources.DeleteClassification, classifiedRaster.Name), Resources.Confirmation,
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
        }

        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var selectedTag = treeView1.SelectedNode?.Tag;
            ExportForm.ExportTarget target;
            object data;

            if (selectedTag is RasterData raster)
            {
                target = ExportForm.ExportTarget.RasterData;
                data = raster;
            }
            else if (selectedTag is IndexRaster indexRaster)
            {
                target = ExportForm.ExportTarget.IndexRaster;
                data = indexRaster;
            }
            else if (selectedTag is ClassifiedRaster classified)
            {
                target = ExportForm.ExportTarget.ClassifiedRaster;
                data = classified;
            }
            else return;

            ExportForm exportForm = new ExportForm(data, target);
            exportForm.Location = Point.Subtract(Point.Add(Location, Size / 2), exportForm.Size / 2);

            if (exportForm.ShowDialog() == DialogResult.OK)
            {
                toolStripStatusLabel1.Text = string.Format(Resources.Exporting, Path.GetFileName(exportForm.FilePath));
                toolStripProgressBar1.Visible = true;
                toolStripStatusLabel1.Visible = true;

                exportBackgroundWorker.RunWorkerAsync(Tuple.Create(data, target, exportForm.FilePath, exportForm.Compression, exportForm.ExportAsByte));
            }
        }

        private void exportBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            if (e.Argument is not Tuple<object, ExportForm.ExportTarget, string, string, bool> args) return;

            var (data, target, filePath, compression, exportAsByte) = args;

            switch (target)
            {
                case ExportForm.ExportTarget.RasterData:
                    ExportService.ExportRasterData((RasterData)data, filePath, compression);
                    break;
                case ExportForm.ExportTarget.IndexRaster:
                    ExportService.ExportIndexRaster((IndexRaster)data, filePath, compression, exportAsByte);
                    break;
                case ExportForm.ExportTarget.ClassifiedRaster:
                    ExportService.ExportClassifiedRaster((ClassifiedRaster)data, filePath, compression);
                    break;
            }
        }

        private void exportBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show(this, string.Format(Resources.ErrorExport, e.Error.Message), Resources.Error,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show(this, Resources.ExportCompleted, Resources.Ready,
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            toolStripProgressBar1.Value = 0;
            toolStripProgressBar1.Visible = false;
            toolStripStatusLabel1.Visible = false;
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            treeView1.SelectedNode = e.Node;
        }
    }
}
