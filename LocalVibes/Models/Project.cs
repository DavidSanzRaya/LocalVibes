namespace LocalVibes.Models
{
    public class Project
    {
        public int IdProject { get; set; }
        public string ProjectName { get; set; }
        public string? Biography { get; set; }
        public DateTime? FormationDate { get; set; }
        public byte[]? ProjectImage { get; set; }
        public int FKUser { get; set; }

        public List<Member> Members { get; set; }

        private User _user = null;

        public User User
        {
            get
            {
                if (_user == null)
                {
                    // Llama a DAL User get ID
                }
                return _user;
            }
            set
            {
                _user = value;
            }
        }
    }
}