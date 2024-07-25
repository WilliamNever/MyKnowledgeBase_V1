namespace Fast8Calculting.Forms
{
    partial class UscBasicEasyInput
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            txtUpper = new TextBox();
            txtLower = new TextBox();
            txtChgRate = new TextBox();
            btnCalc = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(18, 16);
            label1.Name = "label1";
            label1.Size = new Size(50, 15);
            label1.TabIndex = 0;
            label1.Text = "Upper - ";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(18, 48);
            label2.Name = "label2";
            label2.Size = new Size(50, 15);
            label2.TabIndex = 1;
            label2.Text = "Lower - ";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(20, 80);
            label3.Name = "label3";
            label3.Size = new Size(63, 15);
            label3.TabIndex = 2;
            label3.Text = "ChgRate - ";
            // 
            // txtUpper
            // 
            txtUpper.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtUpper.Location = new Point(89, 12);
            txtUpper.Name = "txtUpper";
            txtUpper.Size = new Size(158, 23);
            txtUpper.TabIndex = 3;
            txtUpper.Validating += IntValidating;
            // 
            // txtLower
            // 
            txtLower.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtLower.Location = new Point(89, 44);
            txtLower.Name = "txtLower";
            txtLower.Size = new Size(158, 23);
            txtLower.TabIndex = 4;
            txtLower.Validating += IntValidating;
            // 
            // txtChgRate
            // 
            txtChgRate.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtChgRate.Location = new Point(89, 76);
            txtChgRate.Name = "txtChgRate";
            txtChgRate.Size = new Size(158, 23);
            txtChgRate.TabIndex = 5;
            txtChgRate.Validating += IntValidating;
            // 
            // btnCalc
            // 
            btnCalc.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnCalc.Location = new Point(172, 114);
            btnCalc.Name = "btnCalc";
            btnCalc.Size = new Size(75, 23);
            btnCalc.TabIndex = 6;
            btnCalc.Text = "Calc";
            btnCalc.UseVisualStyleBackColor = true;
            btnCalc.Click += btnCalc_Click;
            // 
            // UscBasicEasyInput
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(btnCalc);
            Controls.Add(txtChgRate);
            Controls.Add(txtLower);
            Controls.Add(txtUpper);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "UscBasicEasyInput";
            Size = new Size(270, 150);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private TextBox txtUpper;
        private TextBox txtLower;
        private TextBox txtChgRate;
        private Button btnCalc;
    }
}
