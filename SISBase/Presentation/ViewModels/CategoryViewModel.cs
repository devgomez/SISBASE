using SISBase.Domain.Entities;
using SISBase.Domain.Interfaces;
using System.Collections.ObjectModel;

namespace SISBase.Presentation.ViewModels
{
    public class CategoryViewModel : PagedCrudViewModelBase<Category>
    {
        public CategoryViewModel(ICategoryRepository repository)
            : base(repository)
        {
        }

        public ObservableCollection<Category> Categories => Items;

        public Category SelectedCategory
        {
            get => SelectedItem;
            set => SelectedItem = value;
        }

        protected override string DisplayName => "la categoria";

        protected override bool MatchesSearch(Category item, string search)
        {
            return item.Name.Contains(search, StringComparison.OrdinalIgnoreCase)
                || item.Description.Contains(search, StringComparison.OrdinalIgnoreCase)
                || item.Id.ToString().Contains(search, StringComparison.OrdinalIgnoreCase);
        }

        protected override string? Validate(Category item)
        {
            if (string.IsNullOrWhiteSpace(item.Name))
            {
                return "Ingrese el nombre de la categoria.";
            }

            return null;
        }

        protected override IEnumerable<Category> SortItems(IEnumerable<Category> items)
        {
            return items.OrderBy(c => c.Name);
        }
    }
}
