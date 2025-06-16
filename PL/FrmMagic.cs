using Erp_V1.CodeGenerator; // Assuming you put CRUDGenerator in this namespace
using System;
using System.Windows.Forms;

namespace Erp_V1
{
    public partial class FrmMagic : Form
    {
        public FrmMagic()
        {
            InitializeComponent();
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            string entityJson = txtEntityDefinition.Text;
            CRUDGenerator generator = new CRUDGenerator();
            generator.GenerateCRUDClasses(
                entityJson,
                out string dtoCode,
                out string daoInterfaceCode,
                out string daoImplementationCode,
                out string bllInterfaceCode,
                out string bllImplementationCode
            );

            txtDTOCode.Text = dtoCode;
            txtDAOInterfaceCode.Text = daoInterfaceCode;
            txtDAOImplementationCode.Text = daoImplementationCode;
            txtBLLInterfaceCode.Text = bllInterfaceCode;
            txtBLLImplementationCode.Text = bllImplementationCode;
        }
    }
}