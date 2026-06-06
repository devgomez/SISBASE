using SISBase.Domain.Entities;
using SISBase.Domain.Interfaces;
using System.Collections.ObjectModel;

namespace SISBase.Presentation.ViewModels
{
    public class RoleViewModel : PagedCrudViewModelBase<Role>
    {
        public RoleViewModel(IRoleRepository repository)
            : base(repository)
        {
        }

        public ObservableCollection<Role> Roles => Items;

        public Role SelectedRole
        {
            get => SelectedItem;
            set => SelectedItem = value;
        }

        protected override string DisplayName => "el rol";

        protected override bool MatchesSearch(Role item, string search)
        {
            return item.Name.Contains(search, StringComparison.OrdinalIgnoreCase)
                || item.Id.ToString().Contains(search, StringComparison.OrdinalIgnoreCase);
        }

        protected override string? Validate(Role item)
        {
            if (string.IsNullOrWhiteSpace(item.Name))
            {
                return "Ingrese el nombre del rol.";
            }

            return null;
        }

        protected override IEnumerable<Role> SortItems(IEnumerable<Role> items)
        {
            return items.OrderBy(r => r.Name);
        }
    }
}
