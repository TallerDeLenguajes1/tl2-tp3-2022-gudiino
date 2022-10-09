using System;
using System.Collections.Generic;
namespace EmpresaCadeteria{
    class Cadeteria{
        private string? nombre {get; set;}
        private string? telefono {get; set;}
        private static float pago_x_entrega {get; set;}
        private static int cantidad_pedidos=0;
        private List<Cadete>? cadetes {get; set;}
        public Cadeteria(){
            cargarDatos();
        }
        private void cargarDatos(){
            cadetes=new List<Cadete>();
            string archivo = "listaCadetes.csv";
            bool aux=false;
            List<string[]> lista_cadetes=HelperDeArchivos.LeerCsv(archivo,',');
            foreach (var item in lista_cadetes)
            {//Cadete(int iden, string nom, string dir,int num, string tel)
                if(aux==true){
                    int n = Convert.ToInt32(item[0]);
                    int x = Convert.ToInt32(item[3]);
                    cadetes.Add(new Cadete(n,item[1],item[2],x,item[4]));
                }else{
                    nombre=item[0];
                    telefono=item[1];
                    pago_x_entrega= Convert.ToSingle(item[2]);
                    aux=true;
                }
            }
        }
        public void listar_info_cadeteria(){
            Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
            Console.WriteLine("Nombre Cadeteria: {0} | Telefono: {1} | Pago por entrega: ${2}",nombre,telefono,pago_x_entrega);
            Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
            Console.WriteLine("Cadetes registrados");
            Console.WriteLine();
            Console.WriteLine("Id     |Nombre      |Calle              |Numero       |Telefono");
            foreach (var item in cadetes!)
            {
                item.listar_info_persona();
            }
        }
        public int getNumPedido(){
            return cantidad_pedidos;
        }
        public void setNumPedido(){
            cantidad_pedidos++;
        }
        public List<Cadete> getCadetes(){
            return cadetes!;
        }
        public float getMontoPago(){
            return pago_x_entrega;
        }
    }
}