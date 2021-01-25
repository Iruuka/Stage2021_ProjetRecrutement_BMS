using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appli_RDV_Vaccin_Infirmier
{
    public class AjoutDataDansBDD
    {
        private string adresseBDD;

        public AjoutDataDansBDD()
        {
            adresseBDD = "Data Source=BDD/base_reservation_vaccin.db";
        }

        public void MAJData()
        {
            EraseData();
            AddFalseData();
        }

        private void EraseData()
        {
            string DeletionInfos = "Delete from creneau; Delete from jour; Delete from reservation; Delete from semaine;";
            using (SQLiteConnection connection = new SQLiteConnection(adresseBDD))
            {
                connection.Open();

                var commande = new SQLiteCommand(DeletionInfos, connection);

                int LignesRetiree = commande.ExecuteNonQuery();
            }
        }

        private void AddFalseData()
        {
            AddWeeks();
            AddReservation();
            System.Windows.Forms.MessageBox.Show("Création des données finie !");
        }

        private void InsertData(string ChaineAInserer, SQLiteConnection connection)
        {
            var commande = new SQLiteCommand(ChaineAInserer, connection);
            commande.ExecuteNonQuery();
        }

        private void AddWeeks()
        {
            int compteurJourTotal = 1;
            int CompteurCreneau = 1;
            for (int compteur = 1; compteur < 4; compteur++)
            {
                int premierJour = 7 * (-1 + compteur) + 1;
                int dernierJour = premierJour + 4;
                string InsertionSemaine = "insert into semaine(ID,premierJour,DernierJour) values("+ compteur + "," + premierJour + "," + dernierJour + ")";
                using (SQLiteConnection connection = new SQLiteConnection(adresseBDD))
                {
                    connection.Open();

                    InsertData(InsertionSemaine, connection);

                    int IDEntre = (int)connection.LastInsertRowId;
                    for (int compteurJour = 0; compteurJour < 5; compteurJour++)
                    {
                        if (compteurJour != 2)
                        {
                            int Jour = premierJour + compteurJour;
                            string InsertJour = "insert into jour(ID, Numero,ID_Semaine) values("+ compteurJourTotal + ", " + Jour + "," + IDEntre + ")";
                            InsertData(InsertJour, connection);
                            int DernierJourEntre = (int)connection.LastInsertRowId;
                            CompteurCreneau = AjoutCreneau(DernierJourEntre, 8, 12, connection,CompteurCreneau);
                            if(compteurJour != 4)
                            {
                                CompteurCreneau = AjoutCreneau(DernierJourEntre, 14, 17, connection,CompteurCreneau);
                            }
                            compteurJourTotal++;

                        }
                    }
                }
            }
        }

        private int AjoutCreneau(int compteurJour, int HeureDebut, int HeureFin, SQLiteConnection connection, int compteurCreneau)
        {
            for (int compteurHeure = HeureDebut; compteurHeure < HeureFin; compteurHeure++)
            {
                for (int compteurMinute = 0; compteurMinute < 60; compteurMinute += 15)
                {
                    string DebutCreneau = compteurHeure + ":" + compteurMinute;
                    string InsertCreneau = "insert into creneau(ID,ID_Jour,Debut_Creneau,EstPris) values("+ compteurCreneau+"," + compteurJour+ ",'" + DebutCreneau + "'," + 0 + ")";
                    InsertData(InsertCreneau, connection);
                    compteurCreneau++;
                }
            }
            return compteurCreneau;
        }

        private void AddReservation()
        {
            string selectPremierCreneau = "select ID from creneau limit 6";
            using(SQLiteConnection connection = new SQLiteConnection(adresseBDD))
            {
                connection.Open();

                var commande = new SQLiteCommand(selectPremierCreneau,connection);

                using(SQLiteDataReader SQLReader = commande.ExecuteReader())
                {
                    int Id_Reservation = 1;
                    while (SQLReader.Read())
                    {
                        int ID_Creneau = Convert.ToInt32(SQLReader["ID"]);
                        string Reservation = "insert into reservation(ID,ID_Creneau,nom,prenom,date_naissance,sexe,numero_telephone,adresse)" +
                            " values("+ Id_Reservation +","+ ID_Creneau+",'jean','pierre','2000-12-21','M','55555','adressefactice')";
                        InsertData(Reservation, connection);
                        Id_Reservation++;
                    }
                }
            }
        }
    }
}
