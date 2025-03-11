namespace BugBunty_Api.Infrastucture.Models.Domaine
{
    public class DetailProjet
    {
        public int Id { get; set; }

        public string Explication_Globale { get; set; }
        public string Regles { get; set; }
        public string Emplacement_Bug { get; set; }
        public string? Vilnerabilite_Haute { get; set; }
        public string? Vilnerabilite_Moyenne { get; set; }
        public string? Vilnerabilite_Basse { get; set; }

        public int ProjetId { get; set; }
        public Projet Projet { get; set; }

    }
}
