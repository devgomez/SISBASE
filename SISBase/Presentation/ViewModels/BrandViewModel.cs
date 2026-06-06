using SISBase.Domain.Entities;
using SISBase.Domain.Interfaces;
using System.Collections.ObjectModel;

namespace SISBase.Presentation.ViewModels
{
    public class BrandViewModel : PagedCrudViewModelBase<Brand>
    {
        public BrandViewModel(IBrandRepository repository)
            : base(repository)
        {
        }

        public ObservableCollection<Brand> Brands => Items;

        public Brand SelectedBrand
        {
            get => SelectedItem;
            set => SelectedItem = value;
        }

        protected override string DisplayName => "la marca";

        protected override bool MatchesSearch(Brand item, string search)
        {
            return item.Name.Contains(search, StringComparison.OrdinalIgnoreCase)
                || item.Id.ToString().Contains(search, StringComparison.OrdinalIgnoreCase);
        }

        protected override string? Validate(Brand item)
        {
            if (string.IsNullOrWhiteSpace(item.Name))
            {
                return "Ingrese el nombre de la marca.";
            }

            return null;
        }

        protected override IEnumerable<Brand> SortItems(IEnumerable<Brand> items)
        {
            return items.OrderBy(b => b.Name);
        }
    }
}
