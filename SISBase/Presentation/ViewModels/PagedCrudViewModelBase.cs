using SISBase.Domain.Interfaces;
using SISBase.Presentation.Commands;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Windows;
using System.Windows.Input;

namespace SISBase.Presentation.ViewModels
{
    public abstract class PagedCrudViewModelBase<TEntity> : BaseViewModel
        where TEntity : class, new()
    {
        private readonly ICrudRepository<TEntity> _repository;
        private List<TEntity> _allItems = new();
        private List<TEntity> _filteredItems = new();

        protected PagedCrudViewModelBase(ICrudRepository<TEntity> repository)
        {
            _repository = repository;

            NewCommand = new RelayCommand(_ => New());
            SaveCommand = new RelayCommand(async _ => await SaveAsync());
            DeleteCommand = new RelayCommand(async _ => await DeleteAsync(SelectedItem));
            EditCommand = new RelayCommand(item => Edit(item as TEntity));
            DeleteRowCommand = new RelayCommand(async item => await DeleteAsync(item as TEntity));
            SearchCommand = new RelayCommand(_ => ApplyFilters());

            FirstPageCommand = new RelayCommand(_ => GoToFirstPage());
            PreviousPageCommand = new RelayCommand(_ => GoToPreviousPage());
            NextPageCommand = new RelayCommand(_ => GoToNextPage());
            LastPageCommand = new RelayCommand(_ => GoToLastPage());

            _ = LoadAsync();
        }

        public ObservableCollection<TEntity> Items { get; } = new();

        private TEntity _selectedItem = new();
        public TEntity SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value ?? new TEntity();
                OnPropertyChanged();
            }
        }

        private string _searchText = string.Empty;
        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                OnPropertyChanged();
            }
        }

        private string _statusFilter = "Todos";
        public string StatusFilter
        {
            get => _statusFilter;
            set
            {
                _statusFilter = value;
                OnPropertyChanged();
            }
        }

        private int _currentPage = 1;
        public int CurrentPage
        {
            get => _currentPage;
            private set
            {
                _currentPage = Math.Max(1, value);
                OnPropertyChanged();
            }
        }

        public int PageSize { get; set; } = 10;

        public int TotalItems => _filteredItems.Count;

        public int TotalPages => Math.Max(1, (int)Math.Ceiling(TotalItems / (double)PageSize));

        public string PaginationSummary
        {
            get
            {
                if (TotalItems == 0)
                {
                    return "Sin registros";
                }

                var start = ((CurrentPage - 1) * PageSize) + 1;
                var end = Math.Min(CurrentPage * PageSize, TotalItems);

                return $"Mostrando {start}-{end} de {TotalItems} registros";
            }
        }

        public ICommand NewCommand { get; }
        public ICommand SaveCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand DeleteRowCommand { get; }
        public ICommand SearchCommand { get; }
        public ICommand FirstPageCommand { get; }
        public ICommand PreviousPageCommand { get; }
        public ICommand NextPageCommand { get; }
        public ICommand LastPageCommand { get; }

        protected async Task LoadAsync()
        {
            _allItems = await _repository.GetAllAsync();
            await AfterLoadAsync();
            ApplyFilters();
        }

        protected virtual Task AfterLoadAsync()
        {
            return Task.CompletedTask;
        }

        private void New()
        {
            SelectedItem = CreateNewEntity();
        }

        protected virtual TEntity CreateNewEntity()
        {
            return new TEntity();
        }

        protected abstract string DisplayName { get; }

        protected abstract bool MatchesSearch(TEntity item, string search);

        protected virtual bool MatchesStatus(TEntity item)
        {
            return true;
        }

        protected virtual string? Validate(TEntity item)
        {
            return null;
        }

        protected virtual IEnumerable<TEntity> SortItems(IEnumerable<TEntity> items)
        {
            return items;
        }

        private void Edit(TEntity? item)
        {
            if (item == null)
            {
                return;
            }

            SelectedItem = item;
        }

        private async Task SaveAsync()
        {
            try
            {
                var validationError = Validate(SelectedItem);
                if (!string.IsNullOrWhiteSpace(validationError))
                {
                    MessageBox.Show(validationError);
                    return;
                }

                if (GetId(SelectedItem) == 0)
                {
                    await _repository.AddAsync(SelectedItem);
                }
                else
                {
                    await _repository.UpdateAsync(SelectedItem);
                }

                SelectedItem = CreateNewEntity();
                await LoadAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, $"Error de {DisplayName}");
            }
        }

        private async Task DeleteAsync(TEntity? item)
        {
            if (item == null)
            {
                return;
            }

            var id = GetId(item);
            if (id == 0)
            {
                return;
            }

            if (MessageBox.Show($"¿Eliminar {DisplayName}?", "Confirmar", MessageBoxButton.YesNo) != MessageBoxResult.Yes)
            {
                return;
            }

            await _repository.DeleteAsync(id);

            if (ReferenceEquals(item, SelectedItem))
            {
                SelectedItem = CreateNewEntity();
            }

            await LoadAsync();
        }

        protected void ApplyFilters()
        {
            var search = SearchText.Trim();

            _filteredItems = SortItems(_allItems.Where(item =>
                (string.IsNullOrWhiteSpace(search) || MatchesSearch(item, search)) &&
                MatchesStatus(item))).ToList();

            if (CurrentPage > TotalPages)
            {
                CurrentPage = TotalPages;
            }

            RefreshPage();
        }

        private void RefreshPage()
        {
            Items.Clear();

            var pageItems = _filteredItems
                .Skip((CurrentPage - 1) * PageSize)
                .Take(PageSize)
                .ToList();

            foreach (var item in pageItems)
            {
                Items.Add(item);
            }

            OnPropertyChanged(nameof(TotalItems));
            OnPropertyChanged(nameof(TotalPages));
            OnPropertyChanged(nameof(PaginationSummary));
            OnPropertyChanged(nameof(CurrentPage));
        }

        private void GoToFirstPage()
        {
            CurrentPage = 1;
            RefreshPage();
        }

        private void GoToPreviousPage()
        {
            CurrentPage = Math.Max(1, CurrentPage - 1);
            RefreshPage();
        }

        private void GoToNextPage()
        {
            CurrentPage = Math.Min(TotalPages, CurrentPage + 1);
            RefreshPage();
        }

        private void GoToLastPage()
        {
            CurrentPage = TotalPages;
            RefreshPage();
        }

        private static int GetId(TEntity entity)
        {
            var property = typeof(TEntity).GetProperty("Id", BindingFlags.Public | BindingFlags.Instance);
            if (property == null || property.PropertyType != typeof(int))
            {
                throw new InvalidOperationException($"{typeof(TEntity).Name} no tiene una propiedad Id de tipo int.");
            }

            return (int)(property.GetValue(entity) ?? 0);
        }
    }
}
