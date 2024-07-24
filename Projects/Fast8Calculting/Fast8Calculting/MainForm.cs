using Fast8Calculting.Forms;

namespace Fast8Calculting
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MenuDropDownItemsClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            ssbMessage.Text = $"{e.ClickedItem.AccessibleName} start processing...";

            switch (e.ClickedItem.AccessibleName)
            {
                case "EasyTestTool":
                    var form = new EasyTestToolForm();
                    form.Show();
                    ssbMessage.Text = $"Open {e.ClickedItem.AccessibleName} - done";
                    break;
                default:
                    break;
            }
        }
    }
}