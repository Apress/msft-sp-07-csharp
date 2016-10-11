namespace Mileage_Calculator
{
    partial class Form1
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.mileage = new System.Windows.Forms.NumericUpDown();
            this.rate = new System.Windows.Forms.Label();
            this.reimbursement = new System.Windows.Forms.Label();
            this.submit = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.mileage)).BeginInit();
            this.SuspendLayout();
            // 
            // mileage
            // 
            this.mileage.Location = new System.Drawing.Point(25, 21);
            this.mileage.Name = "mileage";
            this.mileage.Size = new System.Drawing.Size(82, 20);
            this.mileage.TabIndex = 0;
            // 
            // rate
            // 
            this.rate.AutoSize = true;
            this.rate.Location = new System.Drawing.Point(22, 59);
            this.rate.Name = "rate";
            this.rate.Size = new System.Drawing.Size(25, 13);
            this.rate.TabIndex = 1;
            this.rate.Text = "rate";
            // 
            // reimbursement
            // 
            this.reimbursement.AutoSize = true;
            this.reimbursement.Location = new System.Drawing.Point(22, 92);
            this.reimbursement.Name = "reimbursement";
            this.reimbursement.Size = new System.Drawing.Size(75, 13);
            this.reimbursement.TabIndex = 2;
            this.reimbursement.Text = "reimbursement";
            // 
            // submit
            // 
            this.submit.Location = new System.Drawing.Point(104, 162);
            this.submit.Name = "submit";
            this.submit.Size = new System.Drawing.Size(75, 23);
            this.submit.TabIndex = 3;
            this.submit.Text = "submit";
            this.submit.UseVisualStyleBackColor = true;
            this.submit.Click += new System.EventHandler(this.submit_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(216, 216);
            this.Controls.Add(this.submit);
            this.Controls.Add(this.reimbursement);
            this.Controls.Add(this.rate);
            this.Controls.Add(this.mileage);
            this.Name = "Form1";
            this.Text = "Mileage Calculator";
            ((System.ComponentModel.ISupportInitialize)(this.mileage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown mileage;
        private System.Windows.Forms.Label rate;
        private System.Windows.Forms.Label reimbursement;
        private System.Windows.Forms.Button submit;
    }
}

