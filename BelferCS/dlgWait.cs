using System.Windows.Forms;

namespace BelferCS
{
    public partial class dlgWait : Form
    {
        public dlgWait()
        {
            InitializeComponent();
        }

        private void dlgWait_Load(object sender, System.EventArgs e)
        {
            lblInfo.Text = Tag.ToString();
        }
    }
}
