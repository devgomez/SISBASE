using SISBase.Domain.Entities;
using SISBase.Domain.Interfaces;
using SISBase.Presentation.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

    {
        private readonly IRoleRepository _repository;

    public class RoleViewModel : PagedCrudViewModelBase<Role>
        {
        public RoleViewModel(IRoleRepository repository)
            : base(repository)
            NewCommand =
        {
            Roles.Clear();
        public ObservableCollection<Role> Roles => Items;

        public Role SelectedRole

            get => SelectedItem;
            set => SelectedItem = value;
        }

        protected override string DisplayName => "el rol";

        protected override bool MatchesSearch(Role item, string search)
        {
            return item.Name.Contains(search, StringComparison.OrdinalIgnoreCase);
        }

        protected override string? Validate(Role item)
        {
            if (string.IsNullOrWhiteSpace(item.Name))

                return "Ingrese el nombre del rol.";
                SelectedRole.Id);

            return null;


        protected override IEnumerable<Role> SortItems(IEnumerable<Role> items)
        {
            return items.OrderBy(r => r.Name);
        }

