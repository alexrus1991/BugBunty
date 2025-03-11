namespace BugBunty_Api.Infrastucture.Models.Domaine
{
    public class User
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public Role? RoleUser { get; set; }

        public IEnumerable<Rapport>? Chercheur_Reports { get; set; }
        public IEnumerable<Rapport>? Admin_Validations_Reports { get; set; }

    }
}
