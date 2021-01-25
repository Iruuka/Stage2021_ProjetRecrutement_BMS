namespace Appli_RDV_Vaccin_Infirmier
{
    partial class FaireReservation
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
            this.btn_reserver = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.rctxtbox_adresse = new System.Windows.Forms.RichTextBox();
            this.dtp_naissance = new System.Windows.Forms.DateTimePicker();
            this.txtbox_prenom = new System.Windows.Forms.TextBox();
            this.txtbox_nom = new System.Windows.Forms.TextBox();
            this.txtbox_numtel = new System.Windows.Forms.TextBox();
            this.txtbox_sexe = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btn_reserver
            // 
            this.btn_reserver.Location = new System.Drawing.Point(190, 151);
            this.btn_reserver.Name = "btn_reserver";
            this.btn_reserver.Size = new System.Drawing.Size(75, 23);
            this.btn_reserver.TabIndex = 0;
            this.btn_reserver.Text = "Réserver";
            this.btn_reserver.UseVisualStyleBackColor = true;
            this.btn_reserver.Click += new System.EventHandler(this.btn_reserver_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(37, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Prénom";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(43, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Nom";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 96);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(109, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Numéro de téléphone";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(247, 89);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Sexe";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(397, 25);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(45, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "Adresse";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(219, 25);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(96, 13);
            this.label6.TabIndex = 6;
            this.label6.Text = "Date de naissance";
            // 
            // rctxtbox_adresse
            // 
            this.rctxtbox_adresse.Location = new System.Drawing.Point(400, 50);
            this.rctxtbox_adresse.Name = "rctxtbox_adresse";
            this.rctxtbox_adresse.Size = new System.Drawing.Size(206, 124);
            this.rctxtbox_adresse.TabIndex = 7;
            this.rctxtbox_adresse.Text = "Une adresse qui est factice";
            // 
            // dtp_naissance
            // 
            this.dtp_naissance.Location = new System.Drawing.Point(171, 50);
            this.dtp_naissance.Name = "dtp_naissance";
            this.dtp_naissance.Size = new System.Drawing.Size(200, 20);
            this.dtp_naissance.TabIndex = 8;
            this.dtp_naissance.Value = new System.DateTime(1984, 7, 19, 0, 0, 0, 0);
            // 
            // txtbox_prenom
            // 
            this.txtbox_prenom.Location = new System.Drawing.Point(15, 25);
            this.txtbox_prenom.Name = "txtbox_prenom";
            this.txtbox_prenom.Size = new System.Drawing.Size(100, 20);
            this.txtbox_prenom.TabIndex = 9;
            this.txtbox_prenom.Text = "Jean";
            // 
            // txtbox_nom
            // 
            this.txtbox_nom.Location = new System.Drawing.Point(15, 66);
            this.txtbox_nom.Name = "txtbox_nom";
            this.txtbox_nom.Size = new System.Drawing.Size(100, 20);
            this.txtbox_nom.TabIndex = 10;
            this.txtbox_nom.Text = "LaCroute";
            // 
            // txtbox_numtel
            // 
            this.txtbox_numtel.Location = new System.Drawing.Point(15, 116);
            this.txtbox_numtel.Name = "txtbox_numtel";
            this.txtbox_numtel.Size = new System.Drawing.Size(100, 20);
            this.txtbox_numtel.TabIndex = 11;
            this.txtbox_numtel.Text = "00.00.00.00.00";
            // 
            // txtbox_sexe
            // 
            this.txtbox_sexe.Location = new System.Drawing.Point(215, 105);
            this.txtbox_sexe.Name = "txtbox_sexe";
            this.txtbox_sexe.Size = new System.Drawing.Size(100, 20);
            this.txtbox_sexe.TabIndex = 12;
            this.txtbox_sexe.Text = "N";
            // 
            // FaireReservation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(618, 188);
            this.Controls.Add(this.txtbox_sexe);
            this.Controls.Add(this.txtbox_numtel);
            this.Controls.Add(this.txtbox_nom);
            this.Controls.Add(this.txtbox_prenom);
            this.Controls.Add(this.dtp_naissance);
            this.Controls.Add(this.rctxtbox_adresse);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_reserver);
            this.Name = "FaireReservation";
            this.Text = "FaireReservation";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_reserver;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.RichTextBox rctxtbox_adresse;
        private System.Windows.Forms.DateTimePicker dtp_naissance;
        private System.Windows.Forms.TextBox txtbox_prenom;
        private System.Windows.Forms.TextBox txtbox_nom;
        private System.Windows.Forms.TextBox txtbox_numtel;
        private System.Windows.Forms.TextBox txtbox_sexe;
    }
}