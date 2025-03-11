namespace BugBunty_Api.Infrastucture.Models.Domaine
{
    public class Rapport
    {
        public int Id { get; set; }
        public string Titre { get; set; }
        public DateTime? DateRapport { get; set; }
        public string Description { get; set; }

        public int ProjetId { get; set; }
        public Projet Projet { get; set; }

        public int UserChercheurId { get; set; }
        public User UserChercheur { get; set; }

        public int? UserAdminId { get; set; }
        public User? UserAdmin { get; set; }
    }
}
