using B007Clases.Data;
using B007Clases.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maui001Crud.Models.ClientesVistas
{
    public partial class ClienteCrearModelo : ObservableObject, IQueryAttributable
    {
        private B007CrudContext _contextoBD;

        [ObservableProperty]
        private long idCliente;
        [ObservableProperty]
        private string? nombre;
        [ObservableProperty]
        private string? apellidos;
        [ObservableProperty]
        private string? email;
        [ObservableProperty]
        private string? telefono;
        [ObservableProperty]
        private DateOnly fechaAlta;
        [ObservableProperty]
        private DateTime creado;
        [ObservableProperty]
        private DateTime modificado;
        [ObservableProperty]
        private string titulo = "Crear Cliente ...";
        [ObservableProperty]
        private bool cargandoVisible = false;

        private Cliente? clienteOriginal = null;
        private long idClienteOriginal = 0;


        public ClienteCrearModelo(B007CrudContext contextoBD)
        {
            _contextoBD = contextoBD;
        }

        /*
        public ClienteCrearModelo()
        {

        }*/

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            idClienteOriginal = Convert.ToInt64(query["id"]);

            Task.Run(async () =>
            {
                if (idClienteOriginal != 0)
                {
                    CargandoVisible = true;
                    clienteOriginal = await _contextoBD.Clientes.FirstOrDefaultAsync(c => c.IdCliente == idClienteOriginal);

                    if (clienteOriginal != null)
                    {
                        IdCliente = clienteOriginal.IdCliente;
                        Nombre = clienteOriginal.Nombre;
                        Apellidos = clienteOriginal.Apellidos;
                        Email = clienteOriginal.Email;
                        Telefono = clienteOriginal.Telefono;
                        FechaAlta = clienteOriginal.FechaAlta;
                        Creado = clienteOriginal.Creado;
                        Modificado = clienteOriginal.Modificado;
                        Titulo = "Editar Cliente ...";
                    }
                    else
                    {
                        await Shell.Current.Navigation.PopAsync();
                    }
                    CargandoVisible = false;
                }
            });
        }

        [RelayCommand]
        private async Task GuardarCliente()
        {
            CargandoVisible = true;
            if (clienteOriginal == null)
            {
                Cliente nuevoCliente = new Cliente
                {
                    Nombre = Nombre,
                    Apellidos = Apellidos,
                    Email = Email,
                    Telefono = Telefono,
                    FechaAlta = DateOnly.FromDateTime(DateTime.Now),
                    Creado = DateTime.Now,
                    Modificado = DateTime.Now
                };
                _contextoBD.Clientes.Add(nuevoCliente);
            }
            else
            {
                clienteOriginal.Nombre = Nombre;
                clienteOriginal.Apellidos = Apellidos;
                clienteOriginal.Email = Email;
                clienteOriginal.Telefono = Telefono;
                clienteOriginal.Modificado = DateTime.Now;
                _contextoBD.Clientes.Update(clienteOriginal);
            }
            await _contextoBD.SaveChangesAsync();
            await Shell.Current.Navigation.PopAsync();
            CargandoVisible = false;
        }
    }
}
