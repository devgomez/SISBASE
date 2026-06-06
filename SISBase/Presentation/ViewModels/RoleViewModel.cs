using SISBase.Domain.Entities;
using SISBase.Domain.Interfaces;
using SISBase.Presentation.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace SISBase.Presentation.ViewModels
{
    public class RoleViewModel : BaseViewModel
    {
        private readonly IRoleRepository _repository;

        public ObservableCollection<Role> Roles
        {
            get;
        } = new();

        private Role _selectedRole = new();

        public Role SelectedRole
        {
            get => _selectedRole;
            set
            {
                _selectedRole = value;
                OnPropertyChanged();
            }
        }

        public ICommand NewCommand { get; }

        public ICommand SaveCommand { get; }

        public ICommand DeleteCommand { get; }

        public RoleViewModel(
            IRoleRepository repository)
        {
            _repository = repository;

            NewCommand =
                new RelayCommand(_ => New());

            SaveCommand =
                new RelayCommand(async _ => await SaveAsync());

            DeleteCommand =
                new RelayCommand(async _ => await DeleteAsync());

            _ = LoadAsync();
        }

        private async Task LoadAsync()
        {
            Roles.Clear();

            var roles =
                await _repository.GetAllAsync();

            foreach (var role in roles)
            {
                Roles.Add(role);
            }
        }

        private void New()
        {
            SelectedRole = new Role();
        }

        private async Task SaveAsync()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(
                    SelectedRole.Name))
                {
                    MessageBox.Show(
                        "Ingrese el nombre del rol.");

                    return;
                }

                if (SelectedRole.Id == 0)
                {
                    await _repository.AddAsync(
                        SelectedRole);
                }
                else
                {
                    await _repository.UpdateAsync(
                        SelectedRole);
                }

                SelectedRole = new Role();

                await LoadAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async Task DeleteAsync()
        {
            if (SelectedRole.Id == 0)
                return;

            if (MessageBox.Show(
                "¿Eliminar registro?",
                "Confirmar",
                MessageBoxButton.YesNo)
                != MessageBoxResult.Yes)
            {
                return;
            }

            await _repository.DeleteAsync(
                SelectedRole.Id);

            SelectedRole = new Role();

            await LoadAsync();
        }
    }
}
