using LocalVibes.DALs;
using System.ComponentModel.DataAnnotations;

namespace LocalVibes.Models.ViewModels
{
    public class ProfileProjectViewModel
    {
        public Project? Project { get; set; }
		public bool IsFavorite { get; set; }
		public bool IsOwner { get; set; }


        private List<Locations>? _locations;
        public List<Locations>? Locations
        {
            get
            {
                if (_locations == null)
                    _locations = new LocationsDAL().GetAll();

                return _locations;
            }

            set
            {
                _locations = value;
            }
        }
    }
}
