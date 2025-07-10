namespace BankUI
{
    partial class UnifiedDashboardForm
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.txtDisplay = new System.Windows.Forms.TextBox();
            this.btnAction1 = new System.Windows.Forms.Button();
            this.btnAction2 = new System.Windows.Forms.Button();
            this.btnAction3 = new System.Windows.Forms.Button();
            this.btnAction4 = new System.Windows.Forms.Button();
            this.txtAmount = new System.Windows.Forms.TextBox();
            this.txtPhone = new System.Windows.Forms.TextBox();
            this.btnProcess = new System.Windows.Forms.Button();
            this.btnLogout = new System.Windows.Forms.Button();
            this.btnChangePin = new System.Windows.Forms.Button();
            this.lblAmount = new System.Windows.Forms.Label();
            this.lblPhone = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(18, 14);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(1164, 49);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Dashboard";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtDisplay
            // 
            this.txtDisplay.Font = new System.Drawing.Font("Consolas", 10F);
            this.txtDisplay.Location = new System.Drawing.Point(18, 68);
            this.txtDisplay.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtDisplay.Multiline = true;
            this.txtDisplay.Name = "txtDisplay";
            this.txtDisplay.ReadOnly = true;
            this.txtDisplay.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDisplay.Size = new System.Drawing.Size(1162, 470);
            this.txtDisplay.TabIndex = 1;
            this.txtDisplay.Text = "Welcome! Select an option from the buttons below.";
            // 
            // btnAction1
            // 
            this.btnAction1.BackColor = System.Drawing.Color.LightBlue;
            this.btnAction1.Location = new System.Drawing.Point(18, 568);
            this.btnAction1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnAction1.Name = "btnAction1";
            this.btnAction1.Size = new System.Drawing.Size(180, 62);
            this.btnAction1.TabIndex = 2;
            this.btnAction1.Text = "Action 1";
            this.btnAction1.UseVisualStyleBackColor = false;
            this.btnAction1.Click += new System.EventHandler(this.btnAction1_Click);
            // 
            // btnAction2
            // 
            this.btnAction2.BackColor = System.Drawing.Color.LightGreen;
            this.btnAction2.Location = new System.Drawing.Point(225, 568);
            this.btnAction2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnAction2.Name = "btnAction2";
            this.btnAction2.Size = new System.Drawing.Size(180, 62);
            this.btnAction2.TabIndex = 3;
            this.btnAction2.Text = "Action 2";
            this.btnAction2.UseVisualStyleBackColor = false;
            this.btnAction2.Click += new System.EventHandler(this.btnAction2_Click);
            // 
            // btnAction3
            // 
            this.btnAction3.BackColor = System.Drawing.Color.LightCoral;
            this.btnAction3.Location = new System.Drawing.Point(432, 568);
            this.btnAction3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnAction3.Name = "btnAction3";
            this.btnAction3.Size = new System.Drawing.Size(180, 62);
            this.btnAction3.TabIndex = 4;
            this.btnAction3.Text = "Action 3";
            this.btnAction3.UseVisualStyleBackColor = false;
            this.btnAction3.Click += new System.EventHandler(this.btnAction3_Click);
            // 
            // btnAction4
            // 
            this.btnAction4.BackColor = System.Drawing.Color.LightYellow;
            this.btnAction4.Location = new System.Drawing.Point(639, 568);
            this.btnAction4.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnAction4.Name = "btnAction4";
            this.btnAction4.Size = new System.Drawing.Size(180, 62);
            this.btnAction4.TabIndex = 5;
            this.btnAction4.Text = "Action 4";
            this.btnAction4.UseVisualStyleBackColor = false;
            this.btnAction4.Click += new System.EventHandler(this.btnAction4_Click);
            // 
            // txtAmount
            // 
            this.txtAmount.Location = new System.Drawing.Point(120, 660);
            this.txtAmount.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Size = new System.Drawing.Size(176, 26);
            this.txtAmount.TabIndex = 6;
            this.txtAmount.Visible = false;
            // 
            // txtPhone
            // 
            this.txtPhone.Location = new System.Drawing.Point(420, 660);
            this.txtPhone.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Size = new System.Drawing.Size(176, 26);
            this.txtPhone.TabIndex = 7;
            this.txtPhone.Visible = false;
            // 
            // btnProcess
            // 
            this.btnProcess.BackColor = System.Drawing.Color.Orange;
            this.btnProcess.Location = new System.Drawing.Point(639, 654);
            this.btnProcess.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnProcess.Name = "btnProcess";
            this.btnProcess.Size = new System.Drawing.Size(180, 46);
            this.btnProcess.TabIndex = 8;
            this.btnProcess.Text = "Process";
            this.btnProcess.UseVisualStyleBackColor = false;
            this.btnProcess.Visible = false;
            this.btnProcess.Click += new System.EventHandler(this.btnProcess_Click);
            // 
            // btnLogout
            // 
            this.btnLogout.BackColor = System.Drawing.Color.Gray;
            this.btnLogout.Location = new System.Drawing.Point(1002, 568);
            this.btnLogout.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(180, 62);
            this.btnLogout.TabIndex = 9;
            this.btnLogout.Text = "Logout";
            this.btnLogout.UseVisualStyleBackColor = false;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // btnChangePin
            // 
            this.btnChangePin.BackColor = System.Drawing.Color.LightPink;
            this.btnChangePin.Location = new System.Drawing.Point(846, 568);
            this.btnChangePin.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnChangePin.Name = "btnChangePin";
            this.btnChangePin.Size = new System.Drawing.Size(120, 62);
            this.btnChangePin.TabIndex = 12;
            this.btnChangePin.Text = "Change PIN";
            this.btnChangePin.UseVisualStyleBackColor = false;
            this.btnChangePin.Visible = false;
            this.btnChangePin.Click += new System.EventHandler(this.btnChangePin_Click);
            // 
            // lblAmount
            // 
            this.lblAmount.AutoSize = true;
            this.lblAmount.Location = new System.Drawing.Point(18, 665);
            this.lblAmount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAmount.Name = "lblAmount";
            this.lblAmount.Size = new System.Drawing.Size(69, 20);
            this.lblAmount.TabIndex = 10;
            this.lblAmount.Text = "Amount:";
            this.lblAmount.Visible = false;
            // 
            // lblPhone
            // 
            this.lblPhone.AutoSize = true;
            this.lblPhone.Location = new System.Drawing.Point(318, 665);
            this.lblPhone.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPhone.Name = "lblPhone";
            this.lblPhone.Size = new System.Drawing.Size(59, 20);
            this.lblPhone.TabIndex = 11;
            this.lblPhone.Text = "Phone:";
            this.lblPhone.Visible = false;
            // 
            // UnifiedDashboardForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 738);
            this.Controls.Add(this.lblPhone);
            this.Controls.Add(this.lblAmount);
            this.Controls.Add(this.btnChangePin);
            this.Controls.Add(this.btnLogout);
            this.Controls.Add(this.btnProcess);
            this.Controls.Add(this.txtPhone);
            this.Controls.Add(this.txtAmount);
            this.Controls.Add(this.btnAction4);
            this.Controls.Add(this.btnAction3);
            this.Controls.Add(this.btnAction2);
            this.Controls.Add(this.btnAction1);
            this.Controls.Add(this.txtDisplay);
            this.Controls.Add(this.lblTitle);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "UnifiedDashboardForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Unified Dashboard";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TextBox txtDisplay;
        private System.Windows.Forms.Button btnAction1;
        private System.Windows.Forms.Button btnAction2;
        private System.Windows.Forms.Button btnAction3;
        private System.Windows.Forms.Button btnAction4;
        private System.Windows.Forms.TextBox txtAmount;
        private System.Windows.Forms.TextBox txtPhone;
        private System.Windows.Forms.Button btnProcess;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.Button btnChangePin;
        private System.Windows.Forms.Label lblAmount;
        private System.Windows.Forms.Label lblPhone;
    }
}