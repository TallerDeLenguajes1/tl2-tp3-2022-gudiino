using System;
namespace EmpresaCadeteria{
    public class Cadete:Persona {
        private List<Pedido>? pedidos {get; set;}
        public Cadete(int iden, string nom, string dir,int num, string tel):base(iden, nom, dir, num, tel){
            pedidos=new List<Pedido>();
        }
        public void listar_info_cadete(){
            Console.WriteLine("DATOS CADETE");
            Console.WriteLine("Id     |Nombre      |Calle              |Numero       |Telefono");
            listar_info_persona();
            Console.WriteLine();
        }
        public float jornalAcobrar(){
            return CantidadPedidos()*Cadeteria.pago_x_entrega;
        }
        public int CantidadPedidos(){
            int cont=0;
            foreach (var pd2 in pedidos!)
            {
                if(pd2.getEstado()==2){//estado 2, entregado
                    cont++;
                }
            }
            return cont;
        }
        public void agregarPedido(Pedido nuevo){
            pedidos!.Add(nuevo);
        }
        public List<Pedido> getPedidos(){
            return pedidos!;
        }
        public void eliminarPedido(Pedido item){
            pedidos!.Remove(item);
        }
        public void mostrar_lista_pedidos(){
            if(pedidos!.Count!=0){
                foreach (var item in pedidos)
                {
                    item.listar_info_pedido();
                }
            }else{
                Console.WriteLine("LISTA DE PEDIDOS DEL CADETE VACIA");
            }
        }
    }
}