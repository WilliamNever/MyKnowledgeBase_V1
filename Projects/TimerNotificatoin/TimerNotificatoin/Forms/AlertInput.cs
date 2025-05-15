using TimerNotificatoin.Core.Consts;
using TimerNotificatoin.Core.Enums;
using TimerNotificatoin.Core.Helpers;
using TimerNotificatoin.Core.Interfaces;
using TimerNotificatoin.Core.Models;

namespace TimerNotificatoin.Forms
{
    public partial class AlertInput : Form
    {
        private NotificationModel notificate = null!;
        private ContentsForm? _cform = null;
        public AlertInput()
        {
            var dtn = DateTime.Now;
            InitializeComponent();
            ReSetCausesValidation(pnlBackGrd.Controls.OfType<Control>().ToArray(), false);
            dtPicker.MinDate = dtn.AddYears(-20);
            dtPicker.MaxDate = dtn.AddYears(20);
            dtpEndOfDate.MinDate = dtn.AddYears(-20);
            dtpEndOfDate.MaxDate = dtn.AddYears(20);

            InitControls();
        }

        private void InitControls()
        {
            var vals = HOSTServices.GetClassifications();
            cbNType.Items.AddRange(vals.ToArray());
            cbNType.SelectedIndex = 0;
            cbNType.DisplayMember = "DisplayName";
            cbNType.ValueMember = "ID";

            var tmps = HOSTServices.GetTemplates();
            dlLoopTemplates.Items.AddRange(tmps.ToArray());
            dlLoopTemplates.SelectedIndex = 0;
            dlLoopTemplates.DisplayMember = "Name";
            dlLoopTemplates.ValueMember = "Id";
        }

        public void SetNotification(NotificationModel notification)
        {
            notificate = notification;

            txtTitle.Text = notification.Title;
            txtDescription.Text = notification.Description;
            dtPicker.Value = notification.AlertDateTime;
            cbAlert.Checked = notification.ToAlert;
            //for (int i = 0; i < cbNType.Items.Count; i++)
            //{
            //    var itmVal = (cbNType.Items[i] as ClassificationModel) ?? new ClassificationModel();
            //    if (itmVal.ID == notification.ClassificationID)
            //    {
            //        cbNType.SelectedIndex = i;
            //        break;
            //    }
            //}
            ComboBoxSelectedItemChange(cbNType, notificate.ClassificationID);

            cbkHasEndDate.Checked = false;
            if (notificate.NotificationType == EnNotificationType.Loop)
            {
                if (notificate.EndDatetime.HasValue)
                {
                    cbkHasEndDate.Checked = notificate.EndDatetime.HasValue;
                    dtpEndOfDate.Value = notificate.EndDatetime.Value;
                }
                if (notificate.NTemplateId.HasValue)
                {
                    ComboBoxSelectedItemChange(dlLoopTemplates, notificate.NTemplateId.Value);
                }
            }
        }
        #region function methods
        protected static void ComboBoxSelectedItemChange<T>(ComboBox cbbk, T value)
        {
            for (int i = 0; i < cbbk.Items.Count; i++)
            {
                var itmVal = cbbk.Items[i] as ICompareIdentity<T>;
                if (itmVal != null && itmVal.Identity != null && itmVal.Identity.Equals(value))
                {
                    cbbk.SelectedIndex = i;
                    break;
                }
            }
        }
        #endregion
        public NotificationModel GetNotification()
        {
            notificate.Title = txtTitle.Text;
            notificate.Description = txtDescription.Text;
            notificate.AlertDateTime = dtPicker.Value;
            notificate.ToAlert = cbAlert.Checked;
            notificate.ClassificationID = (cbNType.SelectedItem as ClassificationModel)?.ID ?? new ClassificationModel().ID;
            notificate.CurrentAlertDateTime = notificate.AlertDateTime;

            if (notificate.NotificationType == EnNotificationType.Loop)
            {
                notificate.EndDatetime = cbkHasEndDate.Checked ? dtpEndOfDate.Value : null;
                notificate.NTemplateId = (dlLoopTemplates.SelectedItem as NotificationTemplateModel)?.Id;
            }
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

        private void cbNType_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedType = cbNType.SelectedItem as ClassificationModel;
            if (selectedType != null
                && selectedType.NotificationType == EnNotificationType.Loop)
            {
                grpBoxLooper.Show();
                grpBoxLooper.Enabled = true;
            }
            else
            {
                grpBoxLooper.Hide();
                grpBoxLooper.Enabled = false;
            }
        }

        private void cbkHasEndDate_CheckedChanged(object sender, EventArgs e)
        {
            dtpEndOfDate.Enabled = cbkHasEndDate.Checked;
        }

        private void btnShowDetails_Click(object sender, EventArgs e)
        {
            var tmp = dlLoopTemplates.SelectedItem as NotificationTemplateModel;
            if (tmp != null)
            {
                _cform ??= ContentsForm.CreateForm($"Template - {tmp.Name}"
                    , () => Invoke(() => _cform = null)
                    , new Font(new FontFamily("Times New Roman"), 14f));
                _cform.ShowMessage(ConversionsHelper.NJ_SerializeToJson(tmp), EnMessageType.MessageShow);
                _cform.Show();
            }
        }
    }
}
