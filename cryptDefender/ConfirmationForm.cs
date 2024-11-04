using System;
using System.Windows.Forms;

namespace cryptDefender
{
    public partial class ConfirmationForm : Form
    {
        private string _confirmationCode;

        public ConfirmationForm(string confirmationCode)
        {
            InitializeComponent();
            _confirmationCode = confirmationCode;
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            string userInput = txtConfirmationCode.Text;
            if (userInput == _confirmationCode)
            {
                DialogResult = DialogResult.OK;
            }
            else
            {
                DialogResult = DialogResult.Cancel;
            }
        }
    }
}
