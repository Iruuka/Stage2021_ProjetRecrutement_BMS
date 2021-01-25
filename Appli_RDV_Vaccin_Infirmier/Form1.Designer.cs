namespace Appli_RDV_Vaccin_Infirmier
{
    partial class Form1
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_bx_login = new System.Windows.Forms.TextBox();
            this.txt_bx_mdp = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.btn_add_data = new System.Windows.Forms.Button();
            this.btn_leave = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(85, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "RDV_VACCIN";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(57, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "LOGIN";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(25, 110);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Mot de Passe";
            // 
            // txt_bx_login
            // 
            this.txt_bx_login.Location = new System.Drawing.Point(114, 66);
            this.txt_bx_login.Name = "txt_bx_login";
            this.txt_bx_login.Size = new System.Drawing.Size(100, 20);
            this.txt_bx_login.TabIndex = 3;
            this.txt_bx_login.Text = "login1";
            // 
            // txt_bx_mdp
            // 
            this.txt_bx_mdp.Location = new System.Drawing.Point(114, 107);
            this.txt_bx_mdp.Name = "txt_bx_mdp";
            this.txt_bx_mdp.PasswordChar = '*';
            this.txt_bx_mdp.Size = new System.Drawing.Size(100, 20);
            this.txt_bx_mdp.TabIndex = 4;
            this.txt_bx_mdp.Text = "passwd12345";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(85, 134);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "Connexion";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btn_add_data
            // 
            this.btn_add_data.Location = new System.Drawing.Point(186, 15);
            this.btn_add_data.Name = "btn_add_data";
            this.btn_add_data.Size = new System.Drawing.Size(75, 23);
            this.btn_add_data.TabIndex = 6;
            this.btn_add_data.Text = "AjouterData";
            this.btn_add_data.UseVisualStyleBackColor = true;
            this.btn_add_data.Click += new System.EventHandler(this.btn_add_data_Click);
            // 
            // btn_leave
            // 
            this.btn_leave.Location = new System.Drawing.Point(4, 20);
            this.btn_leave.Name = "btn_leave";
            this.btn_leave.Size = new System.Drawing.Size(75, 23);
            this.btn_leave.TabIndex = 7;
            this.btn_leave.Text = "Retour";
            this.btn_leave.UseVisualStyleBackColor = true;
            this.btn_leave.Click += new System.EventHandler(this.btn_leave_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 169);
            this.Controls.Add(this.btn_leave);
            this.Controls.Add(this.btn_add_data);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txt_bx_mdp);
            this.Controls.Add(this.txt_bx_login);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_bx_login;
        private System.Windows.Forms.TextBox txt_bx_mdp;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btn_add_data;
        private System.Windows.Forms.Button btn_leave;
    }
}

