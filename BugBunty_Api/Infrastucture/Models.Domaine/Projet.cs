namespace BugBunty_Api.Infrastucture.Models.Domaine
{
    public class Projet
    {
        public int Id { get; set; }
        public string Titre { get; set; }
        public int Niveau_Vulnerabilite { get; set; }
        public int Prix_Severite { get; set; }
        public bool Statut { get; set; }

        public Rapport? Rapport { get; set; }
        public DetailProjet? detailProjet { get; set; }

        public int? UserId { get; set; }
        public User? UserEntreprise { get; set; }
    }
}
