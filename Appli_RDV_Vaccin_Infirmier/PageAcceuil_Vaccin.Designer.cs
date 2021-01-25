namespace Appli_RDV_Vaccin_Infirmier
{
    partial class PageAcceuil_Vaccin
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
            this.btn_rdv = new System.Windows.Forms.Button();
            this.btn_infirmier = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btn_rdv
            // 
            this.btn_rdv.Location = new System.Drawing.Point(60, 67);
            this.btn_rdv.Name = "btn_rdv";
            this.btn_rdv.Size = new System.Drawing.Size(132, 23);
            this.btn_rdv.TabIndex = 0;
            this.btn_rdv.Text = "Prendre rendez-vous";
            this.btn_rdv.UseVisualStyleBackColor = true;
            this.btn_rdv.Click += new System.EventHandler(this.btn_rdv_Click);
            // 
            // btn_infirmier
            // 
            this.btn_infirmier.Location = new System.Drawing.Point(60, 119);
            this.btn_infirmier.Name = "btn_infirmier";
            this.btn_infirmier.Size = new System.Drawing.Size(136, 23);
            this.btn_infirmier.TabIndex = 1;
            this.btn_infirmier.Text = "Voir les rendez-vous";
            this.btn_infirmier.UseVisualStyleBackColor = true;
            this.btn_infirmier.Click += new System.EventHandler(this.btn_infirmier_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(85, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "RDV VACCIN";
            // 
            // PageAcceuil_Vaccin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(256, 188);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_infirmier);
            this.Controls.Add(this.btn_rdv);
            this.Name = "PageAcceuil_Vaccin";
            this.Text = "PageAcceuil_Vaccin";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_rdv;
        private System.Windows.Forms.Button btn_infirmier;
        private System.Windows.Forms.Label label1;
    }
}