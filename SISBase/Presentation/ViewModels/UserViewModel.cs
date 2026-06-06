using SISBase.Domain.Entities;
using SISBase.Domain.Interfaces;
using System.Collections.ObjectModel;

namespace SISBase.Presentation.ViewModels
{
    public class UserViewModel : PagedCrudViewModelBase<User>
    {
        private readonly IRoleRepository _roleRepository;

        public UserViewModel(IUserRepository repository, IRoleRepository roleRepository)
            : base(repository)
        {
            _roleRepository = roleRepository;
        }

        public ObservableCollection<User> Users => Items;

        public ObservableCollection<Role> Roles { get; } = new();

        public User SelectedUser
        {
            get => SelectedItem;
            set => SelectedItem = value;
        }

        protected override string DisplayName => "el usuario";

        protected override async Task AfterLoadAsync()
        {
            Roles.Clear();

            var roles = await _roleRepository.GetAllAsync();
            foreach (var role in roles.OrderBy(r => r.Name))
            {
                Roles.Add(role);
            }
        }

        protected override bool MatchesSearch(User item, string search)
        {
            return item.Username.Contains(search, StringComparison.OrdinalIgnoreCase)
                || item.Name.Contains(search, StringComparison.OrdinalIgnoreCase)
                || (item.Lastname ?? string.Empty).Contains(search, StringComparison.OrdinalIgnoreCase)
                || (item.Email ?? string.Empty).Contains(search, StringComparison.OrdinalIgnoreCase)
                || item.Id.ToString().Contains(search, StringComparison.OrdinalIgnoreCase);
        }

        protected override bool MatchesStatus(User item)
        {
            return StatusFilter switch
            {
                "Activos" => item.Status,
                "Inactivos" => !item.Status,
                _ => true
            };
        }

        protected override string? Validate(User item)
        {
            if (string.IsNullOrWhiteSpace(item.Username))
            {
                return "Ingrese el nombre de usuario.";
            }

            if (string.IsNullOrWhiteSpace(item.Name))
            {
                return "Ingrese el nombre.";
            }

            if (string.IsNullOrWhiteSpace(item.PasswordHash))
            {
                return "Ingrese la contrasena.";
            }

            return null;
        }

        protected override IEnumerable<User> SortItems(IEnumerable<User> items)
        {
            return items.OrderBy(u => u.Name).ThenBy(u => u.Lastname);
        }

        protected override User CreateNewEntity()
        {
            return new User
            {
                Status = true
            };
        }
    }
}
