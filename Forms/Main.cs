namespace vegetation_analyzer.Forms
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();

            Image image = Image.FromFile("C:\\Users\\sanya\\OneDrive\\Pictures\\Wallpapers\\139240743_p0_master1200.jpg");
            viewport.UpdateImage(image);
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
    }
}
