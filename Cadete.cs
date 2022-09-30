using System;
namespace EmpresaCadeteria{
    public class Cadete:Persona {
        private List<Pedido>? pedidos {get; set;}
        public Cadete(int iden, string nom, string dir,int num, string tel):base(iden, nom, dir, num, tel){
            
        }
        public float jornalAcobrar(){
            return 0;
        }
        public void cantPedidosEntregados(){
            
        }
    }
}