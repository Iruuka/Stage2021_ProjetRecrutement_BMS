using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Appli_RDV_Vaccin_Infirmier
{
    public partial class PageAcceuil_Vaccin : Form
    {
        public PageAcceuil_Vaccin()
        {
            InitializeComponent();
        }

        private void btn_rdv_Click(object sender, EventArgs e)
        {
            AffichageInfoParSemaine Page_Aff_Creneau = new AffichageInfoParSemaine(false,this);
            Page_Aff_Creneau.Show();
            this.Hide();
        }

        private void btn_infirmier_Click(object sender, EventArgs e)
        {
            Form1 PageConnection = new Form1(this);
            PageConnection.Show();
            this.Hide();
        }
    }
}
