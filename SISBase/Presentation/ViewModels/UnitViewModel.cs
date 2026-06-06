using SISBase.Domain.Entities;
using SISBase.Domain.Interfaces;
using System.Collections.ObjectModel;

namespace SISBase.Presentation.ViewModels
{
    public class UnitViewModel : PagedCrudViewModelBase<Unit>
    {
        public UnitViewModel(IUnitRepository repository)
            : base(repository)
        {
        }

        public ObservableCollection<Unit> Units => Items;

        public Unit SelectedUnit
        {
            get => SelectedItem;
            set => SelectedItem = value;
        }

        protected override string DisplayName => "la unidad";

        protected override bool MatchesSearch(Unit item, string search)
        {
            return item.Name.Contains(search, StringComparison.OrdinalIgnoreCase)
                || item.Id.ToString().Contains(search, StringComparison.OrdinalIgnoreCase);
        }

        protected override string? Validate(Unit item)
        {
            if (string.IsNullOrWhiteSpace(item.Name))
            {
                return "Ingrese el nombre de la unidad.";
            }

            return null;
        }

        protected override IEnumerable<Unit> SortItems(IEnumerable<Unit> items)
        {
            return items.OrderBy(u => u.Name);
        }
    }
}
