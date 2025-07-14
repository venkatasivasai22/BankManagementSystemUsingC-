namespace BankUI
{
    partial class ChangePinForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblExistingPin = new System.Windows.Forms.Label();
            this.txtExistingPin = new System.Windows.Forms.TextBox();
            this.lblNewPin = new System.Windows.Forms.Label();
            this.txtNewPin = new System.Windows.Forms.TextBox();
            this.lblConfirmPin = new System.Windows.Forms.Label();
            this.txtConfirmPin = new System.Windows.Forms.TextBox();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(180, 31);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(201, 37);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Change PIN";
            // 
            // lblExistingPin
            // 
            this.lblExistingPin.AutoSize = true;
            this.lblExistingPin.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblExistingPin.Location = new System.Drawing.Point(75, 123);
            this.lblExistingPin.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblExistingPin.Name = "lblExistingPin";
            this.lblExistingPin.Size = new System.Drawing.Size(123, 25);
            this.lblExistingPin.TabIndex = 1;
            this.lblExistingPin.Text = "Existing PIN:";
            // 
            // txtExistingPin
            // 
            this.txtExistingPin.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtExistingPin.Location = new System.Drawing.Point(270, 118);
            this.txtExistingPin.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtExistingPin.MaxLength = 4;
            this.txtExistingPin.Name = "txtExistingPin";
            this.txtExistingPin.PasswordChar = '*';
            this.txtExistingPin.Size = new System.Drawing.Size(223, 30);
            this.txtExistingPin.TabIndex = 2;
            // 
            // lblNewPin
            // 
            this.lblNewPin.AutoSize = true;
            this.lblNewPin.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblNewPin.Location = new System.Drawing.Point(75, 185);
            this.lblNewPin.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNewPin.Name = "lblNewPin";
            this.lblNewPin.Size = new System.Drawing.Size(94, 25);
            this.lblNewPin.TabIndex = 3;
            this.lblNewPin.Text = "New PIN:";
            // 
            // txtNewPin
            // 
            this.txtNewPin.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtNewPin.Location = new System.Drawing.Point(270, 180);
            this.txtNewPin.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtNewPin.MaxLength = 4;
            this.txtNewPin.Name = "txtNewPin";
            this.txtNewPin.PasswordChar = '*';
            this.txtNewPin.Size = new System.Drawing.Size(223, 30);
            this.txtNewPin.TabIndex = 4;
            // 
            // lblConfirmPin
            // 
            this.lblConfirmPin.AutoSize = true;
            this.lblConfirmPin.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblConfirmPin.Location = new System.Drawing.Point(75, 246);
            this.lblConfirmPin.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblConfirmPin.Name = "lblConfirmPin";
            this.lblConfirmPin.Size = new System.Drawing.Size(123, 25);
            this.lblConfirmPin.TabIndex = 5;
            this.lblConfirmPin.Text = "Confirm PIN:";
            // 
            // txtConfirmPin
            // 
            this.txtConfirmPin.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtConfirmPin.Location = new System.Drawing.Point(270, 242);
            this.txtConfirmPin.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtConfirmPin.MaxLength = 4;
            this.txtConfirmPin.Name = "txtConfirmPin";
            this.txtConfirmPin.PasswordChar = '*';
            this.txtConfirmPin.Size = new System.Drawing.Size(223, 30);
            this.txtConfirmPin.TabIndex = 6;
            // 
            // btnSubmit
            // 
            this.btnSubmit.BackColor = System.Drawing.Color.LightGreen;
            this.btnSubmit.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnSubmit.Location = new System.Drawing.Point(180, 338);
            this.btnSubmit.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(120, 54);
            this.btnSubmit.TabIndex = 7;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.UseVisualStyleBackColor = false;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.LightCoral;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnCancel.Location = new System.Drawing.Point(330, 338);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(120, 54);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // ChangePinForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 462);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.txtConfirmPin);
            this.Controls.Add(this.lblConfirmPin);
            this.Controls.Add(this.txtNewPin);
            this.Controls.Add(this.lblNewPin);
            this.Controls.Add(this.txtExistingPin);
            this.Controls.Add(this.lblExistingPin);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ChangePinForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Change PIN";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblExistingPin;
        private System.Windows.Forms.TextBox txtExistingPin;
        private System.Windows.Forms.Label lblNewPin;
        private System.Windows.Forms.TextBox txtNewPin;
        private System.Windows.Forms.Label lblConfirmPin;
        private System.Windows.Forms.TextBox txtConfirmPin;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}