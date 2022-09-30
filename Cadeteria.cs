using System;
using System.Collections.Generic;
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
        public void cargarCadetes(){
            cadetes=new List<Cadete>();
            string archivo = "listaCadetes.csv";
            List<string[]> lista_cadetes=HelperDeArchivos.LeerCsv(archivo,',');
            foreach (var item in lista_cadetes)
            {//Cadete(int iden, string nom, string dir,int num, string tel)
                int n = Convert.ToInt32(item[0]);
                int x = Convert.ToInt32(item[3]);
                cadetes.Add(new Cadete(n,item[1],item[2],x,item[4]));
            }
        }
        public void mostrarDatos(){
            Console.WriteLine("+++++++++++++++++++++++++++++++++");
            Console.WriteLine("Nombre Cadeteria: {0}",nombre);
            Console.WriteLine("Telefono: {0}",telefono);
            Console.WriteLine("Pago por entrega: {0}",pago_x_entrega);
            Console.WriteLine("+++++++++++++++++++++++++++++++++");
            Console.WriteLine("Cadetes registrados");
            Console.WriteLine();
            foreach (var item in cadetes!)
            {
                item.listarinfo();
            }
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