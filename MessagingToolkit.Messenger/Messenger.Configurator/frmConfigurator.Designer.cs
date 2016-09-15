namespace Messenger.Configurator
{
    partial class frmConfigurator
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
            this.btnGenerateModemConfiguration = new System.Windows.Forms.Button();
            this.txtOutput = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnGenerateModemConfiguration
            // 
            this.btnGenerateModemConfiguration.Location = new System.Drawing.Point(194, 27);
            this.btnGenerateModemConfiguration.Name = "btnGenerateModemConfiguration";
            this.btnGenerateModemConfiguration.Size = new System.Drawing.Size(259, 41);
            this.btnGenerateModemConfiguration.TabIndex = 0;
            this.btnGenerateModemConfiguration.Text = "Generate Modem Configuration";
            this.btnGenerateModemConfiguration.UseVisualStyleBackColor = true;
            this.btnGenerateModemConfiguration.Click += new System.EventHandler(this.btnGenerateModemConfiguration_Click);
            // 
            // txtOutput
            // 
            this.txtOutput.Location = new System.Drawing.Point(12, 102);
            this.txtOutput.Multiline = true;
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.Size = new System.Drawing.Size(672, 304);
            this.txtOutput.TabIndex = 1;
            // 
            // frmConfigurator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(699, 437);
            this.Controls.Add(this.txtOutput);
            this.Controls.Add(this.btnGenerateModemConfiguration);
            this.Name = "frmConfigurator";
            this.Text = "Messenger Application Configurator";
            this.Load += new System.EventHandler(this.frmConfigurator_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnGenerateModemConfiguration;
        private System.Windows.Forms.TextBox txtOutput;
    }
}

