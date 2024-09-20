using TimerNotificatoin.Core.Consts;
using TimerNotificatoin.Core.Enums;
using TimerNotificatoin.Core.Models;

namespace TimerNotificatoin.Forms
{
    public partial class AlertInput : Form
    {
        private NotificationModel notificate = null!;
        public AlertInput()
        {
            var dtn = DateTime.Now;
            InitializeComponent();
            ReSetCausesValidation(pnlBackGrd.Controls.OfType<Control>().ToArray(), false);
            dtPicker.MinDate = dtn.AddYears(-20);
            dtPicker.MaxDate = dtn.AddYears(20);

            InitControls();
        }

        private void InitControls()
        {
            var vals = HOSTServices.GetClassifications();
            cbNType.Items.AddRange(vals.ToArray());
            cbNType.SelectedIndex = 0;
            cbNType.DisplayMember = "Name";
            cbNType.ValueMember = "ID";
        }

        public void SetNotification(NotificationModel notification)
        {
            notificate = notification;

            txtTitle.Text = notification.Title;
            txtDescription.Text = notification.Description;
            dtPicker.Value = notification.AlertDateTime;
            cbAlert.Checked = !notification.IsAlerted;
            for (int i = 0; i < cbNType.Items.Count; i++)
            {
                var itmVal = (cbNType.Items[i] as ClassificationModel) ?? new ClassificationModel();
                if (itmVal.ID == notification.ClassificationID)
                {
                    cbNType.SelectedIndex = i;
                    break;
                }
            }
        }
        public NotificationModel GetNotification()
        {
            notificate.Title = txtTitle.Text;
            notificate.Description = txtDescription.Text;
            notificate.AlertDateTime = dtPicker.Value;
            notificate.IsAlerted = !cbAlert.Checked;
            notificate.ClassificationID = (cbNType.SelectedItem as ClassificationModel)?.ID ?? new ClassificationModel().ID;
            return notificate;
        }

        private void txtRequired_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var tb = sender as TextBox;
            if (tb != null)
            {
                if (string.IsNullOrEmpty(tb.Text))
                {
                    e.Cancel = true;
                    tb.BackColor = Color.Red;
                    tb.ForeColor = Color.White;
                }
                else
                {
                    tb.BackColor = Color.White;
                    tb.ForeColor = Color.Black;
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            ReSetCausesValidation(pnlBackGrd.Controls.OfType<Control>().ToArray(), true);
            var isv = ValidateChildren(ValidationConstraints.Enabled);
            ReSetCausesValidation(pnlBackGrd.Controls.OfType<Control>().ToArray(), false);
            if (!isv)
            {
                DialogResult = DialogResult.Continue;
            }
        }

        private void AlertInput_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = DialogResult == DialogResult.Continue;
        }

        private void ReSetCausesValidation(Control[] controls, bool canCause)
        {
            foreach (Control c in controls)
            {
                c.CausesValidation = canCause;
            }
        }
    }
}
