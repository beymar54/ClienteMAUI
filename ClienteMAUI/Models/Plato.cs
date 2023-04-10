using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClienteMAUI.Models
{
    public class Plato : INotifyPropertyChanged
    {
        private int _Id;
        public int Id {
            get { return _Id; }
            set {
                if (_Id == value)
                    return;
                _Id = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Id)));
            } 
        }
        private string _Nombre;
        public string Nombre {
            get => _Nombre;//Es exactamente igual a la línea 14 (solo que con notación lambda)
            set {
                if (_Nombre == value)
                    return;
                _Nombre = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Nombre)));
            }
        }

        private decimal _Costo;
        public decimal Costo
        {
            get => _Costo;//Es exactamente igual a la línea 14 (solo que con notación lambda)
            set
            {
                if (_Costo == value)
                    return;
                _Costo = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Costo)));
            }
        }

        private string _Ingredientes;
        public string Ingredientes
        {
            get => _Ingredientes;//Es exactamente igual a la línea 14 (solo que con notación lambda)
            set
            {
                if (_Ingredientes == value)
                    return;
                _Ingredientes = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Ingredientes)));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
