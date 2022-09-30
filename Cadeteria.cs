using System;
namespace EmpresaCadeteria{
    class Cadeteria{
        private string nombre {get; set;}
        private string telefono {get; set;}
        private List<Cadete>? cadetes {get; set;}
        private float pago_x_entrega {get; set;}
        public Cadeteria(string nom, string tel, float pago){
            nombre=nom;
            telefono=tel;
            pago_x_entrega=pago;
        }
        public void asignarPedidoCadete(){

        }
        public void pagarCadete(){

        }
        public void eliminarPedido(){

        }
        public void cambiarCadete(){
            
        }
    }
}