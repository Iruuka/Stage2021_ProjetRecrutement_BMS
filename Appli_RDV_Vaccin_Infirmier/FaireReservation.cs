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
    public partial class FaireReservation : Form
    {
        private static string adresseBDD = "Data Source=../BDD/base_reservation_vaccin.db";
        AffichageInfoParSemaine Page_Affichage;
        private int creneau;
        private string Nom;
        private string Prenom;
        private string Adresse;
        private string NumTel;
        private string Date_Naiss;
        private string Sexe;
        private int Age;

        public FaireReservation(AffichageInfoParSemaine page_A_Rafficher, int ID_Creneau)
        {
            InitializeComponent();
            Page_Affichage = page_A_Rafficher;
            creneau = ID_Creneau;
        }

        private void btn_reserver_Click(object sender, EventArgs e)
        {
            switch (Verification_Entree_Utilisateur())
            {
                case true:
                    InsertionDonnees();
                    fermeturePage();
                    break;
                case false:
                    Affichage_Erreur();
                    break;
            }
        }

        /// <summary>
        /// Affichage d'erreur
        ///     Permet d'afficher un message pour prévenir d'une éventuelle erreur
        ///     durant la tentative de réservation
        /// </summary>
        private void Affichage_Erreur()
        {
            MessageBox.Show("Une erreur est survenue lors de votre enregistrement.\nVérifiez vos informations" +
                "ou réessayez plus tard" +
                "\nAttention : les caractères spéciaux sont interdits (hors -)");
        }

        /// <summary>
        /// Vérification des entrées de l'utilsiateur :
        ///     Permet de vérifier ce que l'utilsiateur a entré.
        ///     Si une des vérifications réussit, le programme retourne
        ///     un booléen positif.
        ///     Sinon, le programme retourne un booléen négatif.
        /// </summary>
        /// <returns></returns>
        private bool Verification_Entree_Utilisateur()
        {
            char[] caracteresInterdits = { '"','\'','\\','/','=','^','(',')','[',']','{','}' };
            if (
                string.IsNullOrEmpty(txtbox_prenom.Text)
                || string.IsNullOrEmpty(txtbox_nom.Text)
                || string.IsNullOrEmpty(txtbox_numtel.Text)
                || string.IsNullOrEmpty(txtbox_sexe.Text)
                || string.IsNullOrEmpty(rctxtbox_adresse.Text)
            ) return false;
            foreach(char interdit in caracteresInterdits)
            {
                if (txtbox_prenom.Text.Contains(interdit) || txtbox_nom.Text.Contains(interdit)
                    || txtbox_sexe.Text.Contains(interdit) ||txtbox_numtel.Text.Contains(interdit)
                    || rctxtbox_adresse.Text.Contains(interdit)) return false;
            }
            return true;
        }

        /// <summary>
        /// Recherche de la dernière réservation : 
        ///     Pour pouvoir insérer dans la base, nous devons aller rechercher
        ///     La dernière réservation faites.
        ///     Si aucune réservation n'a été trouvée, on retourne 0,ation signifiant
        ///     que c'est notre première réserv
        /// </summary>
        /// <returns></returns>
        private int RechercheDerniereReservation()
        {
            string CommandeRecherche = "select ID from reservation order by ID desc limit 1";
            using (SQLiteConnection connection = new SQLiteConnection(adresseBDD))
            {
                connection.Open();
                SQLiteCommand commande = new SQLiteCommand(CommandeRecherche, connection);
                using(SQLiteDataReader sqlreader = commande.ExecuteReader())
                {
                    while (sqlreader.Read())
                    {
                        return Convert.ToInt32(sqlreader["ID"]);
                    }
                }
            }
            return 0;
        }

        /// <summary>
        /// Protection de la base : (actuellement obsolète)
        ///     Permet de protéger la base de données
        ///     contre le caractère "'", qui peut être un danger pour
        ///     la base, car un utilisateur peut vider la base
        ///     en l'utilisant
        /// </summary>
        private void ProtectionBase()
        {
            Nom = txtbox_nom.Text.Replace("'", "\'");
            Prenom = txtbox_prenom.Text.Replace("'", "\'");
            Sexe = txtbox_sexe.Text.Replace("'", "\'");
            NumTel = txtbox_numtel.Text.Replace("'", "\'");
            Adresse = rctxtbox_adresse.Lines[0].Replace("'", "\'");
            MessageBox.Show(rctxtbox_adresse.Lines[0].Replace("'", "\'"));
        }

        /// <summary>
        /// Calcule de l'Age et de la date de Naissance :
        ///     Avec l'entrée de la date de naissance de l'utilisateur, nous allons
        ///     pouvoir découper le texte du "DateTimePicker" pour ne garder que le jour, le mois et l'année.
        ///     Et nous pourrons aussi calculer l'âge de l'utilisateur en récupérant l'année actuelle
        ///     avec l'année de naissance (pas fiable car en début d'année, tout ceux étant né 
        ///     en fin d'année auront le même âge que ceux né au début
        /// </summary>
        private void CalculAgeNaiss()
        {
            string Date_Naissance = dtp_naissance.Text;
            Date_Naiss = Date_Naissance.Split(' ')[1] + " " + Date_Naissance.Split(' ')[2]+ " " + Date_Naissance.Split(' ')[3];
            int AnneeActuelle = DateTime.Today.Year;
            Age = AnneeActuelle - Convert.ToInt32(Date_Naissance.Split(' ')[3]);
        }

        /// <summary>
        /// InsertionDonnées :
        ///     Permet d'insérer les données récupérées de l'utilisateur
        ///     Pour les insérer dans notre base
        /// </summary>
        private void InsertionDonnees()
        {
            ProtectionBase();
            CalculAgeNaiss();
            int dernierereserv = RechercheDerniereReservation()+1;
            string CommandeInsert = "insert into reservation" +
                " values (" +dernierereserv+","+creneau+",'"+Nom +
                "','"+Prenom+"','"+ Date_Naiss+"',"+Age+",'" +Sexe+"','"+NumTel+"','"+rctxtbox_adresse+"')";
            using(SQLiteConnection connection = new SQLiteConnection(adresseBDD))
            {
                connection.Open();
                SQLiteCommand commande = new SQLiteCommand(CommandeInsert, connection);
                commande.ExecuteNonQuery();
            }
        }

        private void fermeturePage()
        {
            Page_Affichage.AffichageInfos();
            Page_Affichage.Show();
            this.Close();
        }

        private void FaireReservation_FormClosing(object sender, FormClosingEventArgs e)
        {
            Page_Affichage.Show();
            this.Close();
        }

    }
}
