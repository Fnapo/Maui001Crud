using B007Clases.Data;
using B007Clases.Models;
using CommunityToolkit.Mvvm.ComponentModel;
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
        private B007CrudContext? _contextoBD;
        [ObservableProperty]
        private ObservableCollection<Cliente>? clientes = new ObservableCollection<Cliente>();

        public ClienteListarModelo(B007CrudContext contextoBD)
        {
            _contextoBD = contextoBD;

            MainThread.BeginInvokeOnMainThread(async () =>
            {
                await ObtenerClientes();
            });
        }

        public ClienteListarModelo()
        {
        
        }

        public async Task ObtenerClientes()
        {
            if (_contextoBD == null)
            {
                throw new InvalidOperationException("El contexto de base de datos no ha sido inicializado.");
            }

            var clientes = await _contextoBD.Clientes.ToListAsync();

            if(clientes != null && clientes.Count > 0)
            {
                Clientes = new ObservableCollection<Cliente>(clientes);
            }
        }
    }
}
