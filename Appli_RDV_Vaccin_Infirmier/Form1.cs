using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Appli_RDV_Vaccin_Infirmier
{
    public partial class Form1 : Form
    {
        private static string ConnectionString = "Data Source=../BDD/base_reservation_vaccin.db";
        PageAcceuil_Vaccin pgAccueil;
        
        public Form1(PageAcceuil_Vaccin pg_Accueil)
        {
            InitializeComponent();
            pgAccueil = pg_Accueil;
        }

        /// <summary>
        /// Bouton de connection :
        ///     Bouton qui permet de se connecter à notre base
        ///     et à avoir accès aux différents créneaux de réservations
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            bool VerifSiExistant = false;
            try
            {
                VerifSiExistant = connection_BDD(txt_bx_login.Text, txt_bx_mdp.Text);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message+" LINE " + ex.StackTrace);
            }
            if (VerifSiExistant)
            {
                AffichageInfoParSemaine FenetreInfos = new AffichageInfoParSemaine(true,this);
                FenetreInfos.Show();
                this.Hide();
            }
        }

        /// <summary>
        /// Connection à la base
        ///     Permet de vérifier si les informations rentrées sont exactes
        ///     ou non
        /// </summary>
        /// <param name="Login"></param>
        /// <param name="Passwd"></param>
        /// <returns></returns>
        private bool connection_BDD(string Login, string Passwd)
        {
            string QuerySearch = "select ID,nom from infirmier where login ='"+Login+"' and password = '"+Passwd+"'";
            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();

                var commande = new SQLiteCommand(QuerySearch,connection);

                using (SQLiteDataReader sqlReader = commande.ExecuteReader())
                {
                    if (sqlReader.HasRows)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }

        /// <summary>
        /// Bouton de RAZ des données
        ///     Permet de remettre à zéro les données dans la base
        ///     en y insérant des données factices
        ///     (à retirer bien sur si utilisations ouverte au public)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_add_data_Click(object sender, EventArgs e)
        {
            try
            {
                AjoutDataDansBDD AddDansBDD = new AjoutDataDansBDD();
                AddDansBDD.MAJData();
            }
            catch(Exception Ex)
            {
                MessageBox.Show(Ex.Message + "\n" + Ex.StackTrace);
            }
        }

        private void btn_leave_Click(object sender, EventArgs e)
        {
            pgAccueil.Show();
            this.Close();
        }
    }
}
