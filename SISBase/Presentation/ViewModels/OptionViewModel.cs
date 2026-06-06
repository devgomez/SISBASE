using SISBase.Domain.Entities;
using SISBase.Domain.Interfaces;
using System.Collections.ObjectModel;

namespace SISBase.Presentation.ViewModels
{
    public class OptionViewModel : PagedCrudViewModelBase<Option>
    {
        public OptionViewModel(IOptionRepository repository)
            : base(repository)
        {
        }

        public ObservableCollection<Option> Options => Items;

        public Option SelectedOption
        {
            get => SelectedItem;
            set => SelectedItem = value;
        }

        protected override string DisplayName => "la opcion";

        protected override bool MatchesSearch(Option item, string search)
        {
            return item.Name.Contains(search, StringComparison.OrdinalIgnoreCase)
                || item.FormName.Contains(search, StringComparison.OrdinalIgnoreCase)
                || (item.Group ?? string.Empty).Contains(search, StringComparison.OrdinalIgnoreCase)
                || item.Id.ToString().Contains(search, StringComparison.OrdinalIgnoreCase);
        }

        protected override bool MatchesStatus(Option item)
        {
            return StatusFilter switch
            {
                "Activos" => item.Status,
                "Inactivos" => !item.Status,
                _ => true
            };
        }

        protected override string? Validate(Option item)
        {
            if (string.IsNullOrWhiteSpace(item.Name))
            {
                return "Ingrese el nombre de la opcion.";
            }

            if (string.IsNullOrWhiteSpace(item.FormName))
            {
                return "Ingrese el nombre del formulario asociado.";
            }

            return null;
        }

        protected override IEnumerable<Option> SortItems(IEnumerable<Option> items)
        {
            return items.OrderBy(o => o.Name);
        }
    }
}
