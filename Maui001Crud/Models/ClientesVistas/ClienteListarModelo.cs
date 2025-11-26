using B007Clases.Data;
using B007Clases.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Maui001Crud.Pages.ClientesPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maui001Crud.Models.ClientesVistas
{
    public partial class ClienteListarModelo : ObservableObject
    {
        private B007CrudContext _contextoBD;
        [ObservableProperty]
        private List<Cliente> clientes;

        public ClienteListarModelo(B007CrudContext contextoBD)
        {
            _contextoBD = contextoBD;
            Clientes = new List<Cliente>();

            //Task.Run(() => ObtenerClientes());
            MainThread.InvokeOnMainThreadAsync(() => ObtenerClientes());
        }

        /*
        public ClienteListarModelo()
        {
            clientes = new ObservableCollection<Cliente>();
        }*/

        public async Task ObtenerClientes()
        {
            if (_contextoBD == null)
            {
                throw new InvalidOperationException("El contexto de base de datos no ha sido inicializado.");
            }

            Clientes = await _contextoBD.Clientes.ToListAsync();
            /*
            if (clientesLocal != null && clientesLocal.Count > 0)
            {
                Clientes = new ObservableCollection<Cliente>(clientesLocal);
            }
            else
            {
                Clientes.Clear();
            }*/
            /*
            await MainThread.InvokeOnMainThreadAsync(() =>
            {
                Clientes = _contextoBD.Clientes.ToList();
                
                Clientes.Clear();
                foreach (var cliente in clientesLocal)
                {
                    Clientes.Add(cliente);
                }
            });*/
        }

        [RelayCommand]
        private async Task CrearCliente()
        {
            var uri = $"//crearcliente?id=0";

            await Shell.Current.GoToAsync(uri);
        }

        [RelayCommand]
        private async Task EditarCliente(Cliente cliente)
        {
            var uri = $"{nameof(ClienteCrearEditar)}?id={cliente.IdCliente}";

            await Shell.Current.GoToAsync(uri);
        }

        [RelayCommand]
        private async Task EliminarCliente(Cliente cliente)
        {
            bool borrar = await Shell.Current.DisplayAlert("Confirmar", $"¿Está seguro de eliminar al cliente {cliente.Nombre} {cliente.Apellidos}?", "Sí", "No");
            if (borrar)
            {
                if (_contextoBD == null)
                {
                    throw new InvalidOperationException("El contexto de base de datos no ha sido inicializado.");
                }
                _contextoBD.Clientes.Remove(cliente);
                await _contextoBD.SaveChangesAsync();
                Clientes.Remove(cliente);
            }
        }
    }
}
