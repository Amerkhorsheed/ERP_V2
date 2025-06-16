using DevExpress.XtraEditors;
using System.Windows.Forms;

namespace Erp_V1
{
    public partial class FrmLiveDashboard : XtraForm
    {
        public FrmLiveDashboard()
        {
            InitializeComponent();
            InitializeDashboard();
        }

        private void InitializeDashboard()
        {
            this.Size = new Size(800, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            
            // Add chart controls and real-time data implementation
        }
    }
} 