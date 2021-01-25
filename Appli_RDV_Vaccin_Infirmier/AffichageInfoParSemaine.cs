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
    public partial class AffichageInfoParSemaine : Form
    {
        private bool Infirmier;

        private static string adresseBDD = "Data Source=../BDD/base_reservation_vaccin.db";
        private string[,] TableauReservationSemaine;
        private int SemaineActuelle,SemainePrecedente,SemaineSuivante;
        private int[] JourDeLaSemaine;
        TableLayoutPanel[] tblLayPnl_semaine;
        private Form PagePrecedente;

        public AffichageInfoParSemaine(bool Infir, Form Page_Precedente)
        {
            InitializeComponent();
            PagePrecedente = Page_Precedente;
            Infirmier = Infir;
            TableauReservationSemaine = new string[28,4];
            tblLayPnl_semaine = new TableLayoutPanel[4] { tblLayPnl_Lundi, tblLayPnl_Mardi, tblLayPnl_Jeudi, tblLayPnl_Vendredi };
            JourDeLaSemaine = new int[4];
            RecuperationSemaineActuelle();
            lbl_semaine.Text = "Semaine " + SemaineActuelle;
            RecuperationSemainePrecSuiv();
            AffichageInfos();
        }

        /// <summary>
        /// Affichage Infos :
        ///     Fonction qui peut être appellée à l'extérieur de la classe pour
        ///     mettre à jour les informations affichées à l'écran
        /// </summary>
        public void AffichageInfos()
        {
            foreach(TableLayoutPanel tblPnl in tblLayPnl_semaine)
            {
                ClearTableLayoutPanel(tblPnl);
            }
            UpdateAffichage();
        }

        public void SetInfirmier(bool EstInfirmier)
        {
            Infirmier = EstInfirmier;
        }

        /// <summary>
        /// btn_sem_prec_click :
        ///     fonction OnClick du bouton "btn_sem_prec" qui permet
        ///     à la page de vérifier si il y a une semaine précédente par rapport à la semaine actuelle
        ///     et met à jour, ou non, l'affichage
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_sem_prec_Click(object sender, EventArgs e)
        {
            if (SemainePrecedente < SemaineActuelle)
            {
                SemaineActuelle--;
                RecuperationSemainePrecSuiv();
                UpdateAffichage();
            }
        }

        /// <summary>
        /// btn_sem_suiv_click :
        ///     fonction Onclick du bouton "btn_sem_suiv" qui permet
        ///     à la page de vérifier si il y a une semaine suivante par rapport à la semaine actuelle
        ///     et met à jour, ou non, l'affichage
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_sem_suiv_Click(object sender, EventArgs e)
        {
            if (SemaineSuivante > SemaineActuelle)
            {
                SemaineActuelle++;
                RecuperationSemainePrecSuiv();
                UpdateAffichage();
            }
        }

        private void btn_quit_Click(object sender, EventArgs e)
        {
            PagePrecedente.Show();
            this.Close();
        }

        /// <summary>
        /// Update Affichage :
        ///     Fonction qui permet de remettre à zéro les TableLayoutPanel
        ///     contenant les différents créneaux et
        ///     remet les créneaux en ayant fait une mise à jour sur les créneaux disponibles ou non.
        ///     Si Infirmier = true : les zones libres ET les informations des patients seront affichées.
        ///     Si Infirmier = false : les zones libres seront affichées
        /// </summary>
        public void UpdateAffichage()
        {
            foreach(TableLayoutPanel tblPnl in tblLayPnl_semaine)
            {
                tblPnl.RowCount = 28;
                ClearTableLayoutPanel(tblPnl);
            }
            switch (Infirmier)
            {
                case true:
                    RecuperationCreneauInfirmier();
                    VerificationCreneau();
                    break;
                case false:
                    RecupPourAffPatient();
                    break;
            }
            lbl_semaine.Text = "Semaine " + SemaineActuelle;
        }

        /// <summary>
        /// Récupération Semaine Actuelle :
        ///     Récupère le numéro de la semaine actuelle dans la base de données
        /// </summary>
        private void RecuperationSemaineActuelle()
        {
            string recupsemaine = "select ID from semaine limit 1";
            using(SQLiteConnection connection = new SQLiteConnection(adresseBDD))
            {
                connection.Open();
                SQLiteCommand commande = new SQLiteCommand(recupsemaine, connection);
                using(SQLiteDataReader sqlreader = commande.ExecuteReader())
                {
                    while (sqlreader.Read())
                    {
                        SemaineActuelle = Convert.ToInt32(sqlreader["ID"]);
                    }
                }
            }
        }

        /// <summary>
        /// Recuperation Semaine Prec Suiv :
        ///     Fonction qui englobe les appels de la fonction SemRech
        ///     Pour récupérer les numéros des Semaines Précédente et Suivante
        /// </summary>
        private void RecuperationSemainePrecSuiv()
        {
            SemRech("select ID from semaine where ID<" + SemaineActuelle + " order by ID desc limit 1",false);
            SemRech("select ID from semaine where ID>" + SemaineActuelle + " limit 1",true);
        }

        /// <summary>
        /// Sem Rech => Semaine Recherche :
        ///     Recherche le numéro de la semaine dans la base
        ///     qui suit ou précède la semaine actuelle.
        ///     Si ce numéro n'existe pas, alors nous agirons comme suit :
        ///         -SemaineAChanger = true : nous cherchons à trouver la semaine suivante.
        ///             Si il n'y a rien après notre semaine actuelle, nous mettons semaineSuivant à SemaineActuelle
        ///         -SemaineAChanger = false : nous cherchons à trouver la semaine précédente.
        ///             Si il n'y a rien avant notre semaine actuelle, nous mettans SemainePrecedente à SemaineActuelle
        /// </summary>
        /// <param name="ChaineDeSelection"></param>
        /// <param name="SemaineAChanger"></param>
        private void SemRech(string ChaineDeSelection,bool SemaineAChanger)
        {
            using (SQLiteConnection connection = new SQLiteConnection(adresseBDD))
            {
                connection.Open();
                SQLiteCommand commande = new SQLiteCommand(ChaineDeSelection, connection);
                using(SQLiteDataReader sqlreader = commande.ExecuteReader())
                {
                    if (sqlreader.Read())
                    {
                        switch (SemaineAChanger)
                        {
                            case true:
                                SemaineSuivante = Convert.ToInt32(sqlreader["ID"]);
                                break;
                            case false:
                                SemainePrecedente = Convert.ToInt32(sqlreader["ID"]);
                                break;
                        }
                    }
                    else
                    {
                        switch (SemaineAChanger)
                        {
                            case true:
                                SemaineSuivante = SemaineActuelle;
                                break;
                            case false:
                                SemainePrecedente = SemaineActuelle;
                                break;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Recupération jour :
        ///     Récupère les ID des jours dont leur numéro de semaine
        ///     correspond au numéro de la semaine actuelle
        /// </summary>
        private void RecuperationJour()
        {
            int compteurPourTableau = 0;
            string jours = "select ID from jour where ID_Semaine=" + SemaineActuelle;
            using(SQLiteConnection connection = new SQLiteConnection(adresseBDD))
            {
                connection.Open();
                SQLiteCommand commande = new SQLiteCommand(jours, connection);
                using(SQLiteDataReader sqlreader = commande.ExecuteReader())
                {
                    while (sqlreader.Read())
                    {
                        JourDeLaSemaine[compteurPourTableau] = Convert.ToInt32(sqlreader["ID"]);
                        compteurPourTableau++;
                    }
                }
            }
        }

        /// <summary>
        /// Recuperation Creneau Infirmier :
        ///     Récupère tout les créneaux de la semaine actuelle
        ///     Et les affiche à l'infirmier qui peut consulter quel patient va venir à quel horraire
        /// </summary>
        private void RecuperationCreneauInfirmier()
        {
            RecuperationJour();
            string listeJour = "("+ JourDeLaSemaine[0]+","+ JourDeLaSemaine[1] + "," + JourDeLaSemaine[2] + "," + JourDeLaSemaine[3] + ")";
            string Creneau = "select ID, EstPris, Debut_Creneau from creneau where ID_jour in "+listeJour;
            using(SQLiteConnection connection = new SQLiteConnection(adresseBDD))
            {
                connection.Open();

                var commande = new SQLiteCommand(Creneau, connection);

                using(SQLiteDataReader sqlreader = commande.ExecuteReader())
                {
                    int compteurCreneau = 0;
                    int compteurJour = 0;
                    while (sqlreader.Read())
                    {
                        if (Convert.ToInt32(sqlreader["EstPris"])==0)
                        {
                            AffichageInfirmierLibre(compteurJour,compteurCreneau,Convert.ToString(sqlreader["Debut_Creneau"]));
                        }
                        else
                        {
                            AffichageInfirmierOccupe(compteurJour, compteurCreneau, Convert.ToInt32(sqlreader["ID"]),Convert.ToString(sqlreader["Debut_Creneau"]));
                        }
                        compteurCreneau++;
                        if (compteurCreneau == 28)
                        {
                            compteurCreneau = 0;
                            compteurJour++;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Affichage Infirmier Libre :
        ///     Affiche le créneau libre,
        ///     signifiant qu'aucun patient n'a
        ///     pris cet horraire
        /// </summary>
        /// <param name="compteurJour"></param>
        /// <param name="compteurCreneau"></param>
        /// <param name="AAfficher"></param>
        private void AffichageInfirmierLibre(int compteurJour, int compteurCreneau, string AAfficher)
        {
            tblLayPnl_semaine[compteurJour].Controls.Add(new Label
            {
                Text = AAfficher,
                Anchor = AnchorStyles.Left,
                AutoSize = true
            }, 0, compteurCreneau);
            TableauReservationSemaine[compteurCreneau, compteurJour] = "Libre";
        }

        /// <summary>
        /// Affichage Infirmier Occupe :
        ///     Affiche le créneau avec les informations
        ///     Du patient qui a réservé ce créneau
        /// </summary>
        /// <param name="compteurJour"></param>
        /// <param name="compteurCreneau"></param>
        /// <param name="IDCreneau"></param>
        /// <param name="AAfficher"></param>
        private void AffichageInfirmierOccupe(int compteurJour, int compteurCreneau, int IDCreneau, string AAfficher)
        {
            tblLayPnl_semaine[compteurJour].Controls.Add(new Label
            {
                Text = AAfficher,
                Anchor = AnchorStyles.Left,
                AutoSize = true
            }, 0, compteurCreneau);
            RecuperationReservation(compteurCreneau, compteurJour, IDCreneau);
        }

        /// <summary>
        /// Recup Pour Affichage Patient :
        ///     Première fonction servant à récupérer les informations à afficher sur
        ///     l'écran d'un utilisateur de type "Patient"
        ///     Cette dernière récupère les jours de la semaine actuelle pour les utiliser dans la recherche
        ///     des créneaux disponibles dans la base
        /// </summary>
        private void RecupPourAffPatient()
        {
            RecuperationJour();
            string listeJour = "(" + JourDeLaSemaine[0] + "," + JourDeLaSemaine[1] + "," + JourDeLaSemaine[2] + "," + JourDeLaSemaine[3] + ")";
            string RecupCreneauLibre = "select ID,ID_jour,Debut_Creneau from creneau where EstPris = 0 and ID_jour in "+listeJour;
            ConnectionBase(RecupCreneauLibre);
        }

        /// <summary>
        /// Connection Base :
        ///     Permet la connection à la base de données
        ///     en passant en arguments la chaine de caractères servant
        ///     à la recherche
        /// </summary>
        /// <param name="ChaineSelection"></param>
        private void ConnectionBase(string ChaineSelection)
        {
            using(SQLiteConnection connection = new SQLiteConnection(adresseBDD))
            {
                connection.Open();
                SQLiteCommand commande = new SQLiteCommand(ChaineSelection, connection);
                SelectionDansBase(commande);
            }
        }

        /// <summary>
        /// Selection Dans Base :
        ///     Fonction qui crée et utilise un SQLiteDataReader.
        ///     Ce dernier nous permettra de lire et traiter les informations que nous avons recherché
        ///     dans notre base
        /// </summary>
        /// <param name="commande"></param>
        private void SelectionDansBase(SQLiteCommand commande)
        {
            SQLiteDataReader sqlreader = commande.ExecuteReader();
            TraitementSelection(sqlreader);
        }

        /// <summary>
        /// Traitement Selection
        ///     Avec notre SQLiteDataReader passé en argument, nous allons
        ///     pouvoir encore séparer le travail en récupérant les données (fonction SauvegardeDonnees)
        ///     puis en les affichant à l'utilisateur (AffichageDonnees)
        ///     Pour pouvoir stocker ces données, nous allons utiliser des tableaux avec une taille de 100 maximum
        ///     (100 = quantité maximum de créneaux en 1 semaines)
        /// </summary>
        /// <param name="sqlreader"></param>
        private void TraitementSelection(SQLiteDataReader sqlreader)
        {
            int[] ListeID_Creneau = new int[100];
            string[] CreneauDispo = new string[100];
            int[] ListeID_Jour = new int[100];
            SauvegardeDonnees(ListeID_Creneau, ListeID_Jour, CreneauDispo,sqlreader);
            AffichageDonnees(ListeID_Creneau,ListeID_Jour,CreneauDispo);
        }

        /// <summary>
        /// Affichage Données :
        ///     Fonction permettant l'affichage des données récupérees.
        ///     En fonction de l'ID du jour dans "listeID_Jour", les créneaux ne seront pas forcément
        ///     affichés aux mêmes endroits.
        ///     Les créneaux du Lundi iront dans le TableLayoutPanel du lundi.
        ///     Pareil pour le Mardi, ect...
        ///     Si un créneau dans la liste "creneauDispo" est null, alors la boucle d'affichage s'arrête là :
        ///     Plus aucune données par la suite ne sera utilsiable (car toutes à null)
        /// </summary>
        /// <param name="listeID_Creneau"></param>
        /// <param name="listeID_Jour"></param>
        /// <param name="creneauDispo"></param>
        private void AffichageDonnees(int[] listeID_Creneau, int[] listeID_Jour, string[] creneauDispo)
        {
            int compteurJour = 0;
            int ID_Jour_En_Cour = listeID_Jour[compteurJour];
            TableLayoutPanel tblPnl = tblLayPnl_semaine[compteurJour];
            ClearTableLayoutPanel(tblPnl);
            tblPnl.RowCount = 0;
            for(int compteurCreneau = 0; compteurCreneau < listeID_Creneau.Length; compteurCreneau++)
            {
                if (creneauDispo[compteurCreneau] != null)
                {
                    if (ID_Jour_En_Cour != listeID_Jour[compteurCreneau])
                    {
                        compteurJour = Array.IndexOf(JourDeLaSemaine, listeID_Jour[compteurCreneau]);
                        ID_Jour_En_Cour = listeID_Jour[compteurCreneau];
                        tblPnl = tblLayPnl_semaine[compteurJour];
                        tblPnl.RowCount = 0;
                    }
                    tblPnl.RowCount++;
                    EventHandler EH = CreationEventHandler(listeID_Creneau[compteurCreneau]);
                    AjoutDansTableLayoutPanel(tblPnl, compteurCreneau, creneauDispo[compteurCreneau], EH);
                }
                else
                {
                    break;
                }
            }
        }

        private void AjoutDansTableLayoutPanel(TableLayoutPanel tblLay,int compteurCreneau,string CreneauDispo,EventHandler EH)
        {
            Button BoutonRDV = new Button
            {
                Text = "Je réserve",
                Visible = true
            };
            BoutonRDV.Click += EH;
            tblLay.Controls.Add(BoutonRDV, 1, compteurCreneau);
            tblLay.Controls.Add(new Label()
            {
                Text = CreneauDispo
            },0,compteurCreneau);
        }

        /// <summary>
        /// Creation Event Handler :
        ///     Fonction qui crée un EventHandler pour remplacer la création d'une fonction dans la classe
        ///     Nom possible de la fonction anonyme : ReserverCreneau :
        ///         Permet de faire afficher la page de réservation d'un créneau
        /// </summary>
        /// <param name="ID_Creneau"></param>
        /// <returns></returns>
        private EventHandler CreationEventHandler(int ID_Creneau)
        {
            return (object o, EventArgs a) =>
            {
                FaireReservation Page_Reservation = new FaireReservation(this,ID_Creneau);
                Page_Reservation.Show();
                this.Hide();
            };
        }

        /// <summary>
        /// Sauvegarde Données :
        ///     Avec des tableaux passés en arguments, et d'un SQLiteDataReader,
        ///     cette fonction permet de récupérer et sauvegarder, dans les tableaux, les valeurs lues
        ///     par le SQLiteDataReader
        /// </summary>
        /// <param name="ID_Creneau"></param>
        /// <param name="ID_Jour"></param>
        /// <param name="Creneau"></param>
        /// <param name="sqlreader"></param>
        private void SauvegardeDonnees(int[] ID_Creneau, int[] ID_Jour, string[] Creneau, SQLiteDataReader sqlreader)
        {
            int compteur = 0;
            while (sqlreader.Read())
            {
                ID_Creneau[compteur] = Convert.ToInt32(sqlreader["ID"]);
                ID_Jour[compteur] = Convert.ToInt32(sqlreader["ID_jour"]);
                Creneau[compteur] = Convert.ToString(sqlreader["Debut_Creneau"]);
                compteur++;
            }
        }

        /// <summary>
        /// Recupération Réservation :
        ///     Récupère les réservations qui ont déjà été faites sur un créneau donné
        ///     pour les enregistrer dans le tableau de réservation de la semaine
        /// </summary>
        /// <param name="compteurCreneau"></param>
        /// <param name="compteurJour"></param>
        /// <param name="ID_Creneau"></param>
        private void RecuperationReservation(int compteurCreneau, int compteurJour, int ID_Creneau)
        {
            string recupCren = "select nom,prenom,date_naissance,sexe,numero_telephone,adresse" +
                                    " from reservation where ID_Creneau ="+ID_Creneau;
            using(SQLiteConnection connection = new SQLiteConnection(adresseBDD))
            {
                connection.Open();
                SQLiteCommand commande = new SQLiteCommand(recupCren, connection);
                using(SQLiteDataReader sqlreader = commande.ExecuteReader())
                {
                    while (sqlreader.Read())
                    {
                        string nom = Convert.ToString(sqlreader["nom"]);
                        string prenom = Convert.ToString(sqlreader["prenom"]);
                        string date_naissance = "Né le : " + Convert.ToString(sqlreader["date_naissance"]);
                        string sexe = Convert.ToString(sqlreader["sexe"]);
                        string num_tel = Convert.ToString(sqlreader["numero_telephone"]);
                        string adresse = Convert.ToString(sqlreader["adresse"]);
                        string InfosAAfficher = prenom + " " + nom + "\t" + date_naissance + "" +
                            "\nSexe : " + sexe + "\tTéléphone : " + num_tel + "\n" + adresse;
                        TableauReservationSemaine[compteurCreneau, compteurJour] = InfosAAfficher;
                    }
                }
            }
        }

        /// <summary>
        /// Verification Creneau :
        ///     Vérifie quel jour le programme doit afficher
        ///     et affiche en conséquence.
        ///     Puisque le Vendredi, une demi journée seulement,
        ///     le programme sépare les matinées et les après-midi
        /// </summary>
        private void VerificationCreneau()
        {
            TableLayoutPanel tblLayPan;
            Label lbl_AAfficher = new Label();
            for(int compteurJour = 0; compteurJour < 4; compteurJour++)
            {
                tblLayPan = tblLayPnl_semaine[compteurJour];
                tblLayPan.RowCount = 28;
                for (int compteurCreneau = 0; compteurCreneau < 16; compteurCreneau++)
                {
                    AffichageCreneau(compteurJour, compteurCreneau, tblLayPan, lbl_AAfficher);
                }
                if(compteurJour != 3)
                {
                    for (int compteurCreneau = 16; compteurCreneau < 28; compteurCreneau++)
                        AffichageCreneau(compteurJour, compteurCreneau, tblLayPan, lbl_AAfficher);
                }
            }
        }

        /// <summary>
        /// Affichage Creneau :
        ///     Permet l'affichage des créneaux côtés Infirmier en utilisant
        ///     Le jour actuel, le créneau actuel, un TableLayoutPanel et un Label qui est à afficher
        ///     et qui sont, tout les quatres, passés en arguments
        /// </summary>
        /// <param name="compteurJour"></param> => entier
        /// <param name="compteurCreneau"></param> => Entier
        /// <param name="tblLayPan"></param> => TableLayoutPanel
        /// <param name="lbl_AAfficher"></param> => Label
        private void AffichageCreneau(int compteurJour, int compteurCreneau, TableLayoutPanel tblLayPan, Label lbl_AAfficher)
        {
            switch (TableauReservationSemaine[compteurCreneau, compteurJour].EndsWith("Libre"))
            {
                case true:
                    tblLayPan.RowStyles.Add(new RowStyle() { Height = 40, SizeType = SizeType.Absolute });
                    break;
                case false:
                    tblLayPan.RowStyles.Add(new RowStyle() { Height = 70, SizeType = SizeType.Absolute });
                    break;
            }
            tblLayPan.Controls.Add(new Label { 
                    Text = TableauReservationSemaine[compteurCreneau, compteurJour], 
                    Anchor = AnchorStyles.Left, 
                    AutoSize = true 
                }, 
            1,compteurCreneau);
        }

        /// <summary>
        /// Clear Table LayoutPanel :
        ///     Retire les styles de lignes et les contrôles étant à l'intérieur
        ///     du TableLayoutPanel passé en argument de la fonction
        /// </summary>
        /// <param name="tblPnl"></param>
        private void ClearTableLayoutPanel(TableLayoutPanel tblPnl)
        {
            tblPnl.RowStyles.Clear();
            tblPnl.Controls.Clear();
        }

        private void AffichageInfoParSemaine_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Close();
        }
    }
}
