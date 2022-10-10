using System;
using System.Linq;
using System.IO;
using System.Net;
namespace EmpresaCadeteria{
    class Program{
        static void Main(string[] args){
            Console.WriteLine("INICIANDO SISTEMA");
            Cadeteria sistema=new Cadeteria();
            pruebaCargaPedidos(sistema);
            Console.WriteLine("ENTER para IR al MENU");
            Console.ReadLine();
            //***********************
            int opc=1;
            while (opc!=0)
            {
                menu_principal();
                Console.Write("Seleccion: ");
                string? opcAux=Console.ReadLine();
                Console.WriteLine();
                if(int.TryParse(opcAux,out opc)&& opc>=0 && opc<=6)
                {
                    switch (opc)
                    {
                        case 0:
                            Console.WriteLine("======================================================================");
                            Console.WriteLine("                 CERRANDO SISTEMA GESTION DE PEDIDOS ");
                            Console.WriteLine("======================================================================");
                            opcAux="0";
                            break;
                        case 1:
                            ;
                            Console.WriteLine("++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
                            altaPedido(sistema);
                            Continuar();
                            break;
                        case 2:
                            Console.WriteLine("++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
                            Console.WriteLine();
                            break;
                        case 3:
                            Console.WriteLine("++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
                            cambiar_estado_pedido(sistema);
                            Continuar();
                            break;
                        case 4:
                            Console.WriteLine("++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
                            cambiar_cadete_pedido(sistema);
                            Continuar();
                            break;
                        case 5:
                            Console.WriteLine("++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
                            informe_pedidos(sistema);
                            Continuar();
                            break;
                        case 6:
                            Console.WriteLine("======================================================================");
                            Console.WriteLine("DATOS CADETERIA");
                            sistema.listar_info_cadeteria();
                            Continuar();
                            break;
                    }
                }else{
                    Console.WriteLine("====================================");
                    Console.WriteLine(" Ingrese una opcion Valida del MENU");
                    Console.WriteLine("====================================");
                    opc=1;
                    Console.WriteLine();
                }
            }
            Console.WriteLine();
            Console.WriteLine("FIN");
        }
        private static void Continuar()
        {
            Console.WriteLine();
            Console.WriteLine("ENTER para VOLVER al MENU");
            Console.ReadLine();
        }
        private static void menu_principal(){
            Console.WriteLine("     MENU PRINCIPAL");
            Console.WriteLine("************************");
            Console.WriteLine();
            Console.WriteLine("1 --> ALTA PEDIDO +"); 
            Console.WriteLine("2 --> ASIGNAR CADETE");
            Console.WriteLine("3 --> CAMBIAR ESTADO PEDIDO +");
            Console.WriteLine("4 --> CAMBIAR CADETE +");
            Console.WriteLine("5 --> INFORME PEDIDOS +");
            Console.WriteLine("6 --> MOSTRAR DATOS CADETERIA +");
            Console.WriteLine("0 --> FINALIZAR +");
            Console.WriteLine();
        }
        public static void altaPedido(Cadeteria sistema){
            //ingreso datos del pedido
            int opc=1;
            while(opc!=0)
            {
                Pedido nuevoPedido=CargaDatosPedido(sistema.getNumPedido());
                if (nuevoPedido!=null){
                    sistema.setNumPedido();
                    AsignarPedidoCadete(sistema, nuevoPedido);
                    opc=0;
                }else{
                    Console.WriteLine("FALLO ALTA PEDIDO");
                    Console.WriteLine("0 --> Finalizar");
                    Console.WriteLine("1 --> Continuar");
                    string? opcAux =Console.ReadLine();
                    if(!((int.TryParse(opcAux,out opc)) && opc>=0 && opc<=1)){
                        Console.WriteLine("Opcion numerica no valida");
                        Console.WriteLine("Por defecto se le solicitara nuevamente la carga de los datos");
                    }
                }
            }
        }
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        private static Pedido CargaDatosPedido(int numPedido){
            Console.WriteLine("DATOS DEL PEDIDO");
            Pedido? nuevoPedido;
            int nro_pedido=numPedido+1;
            Console.WriteLine("Numero de pedido: {0}", nro_pedido);
            Console.Write("Ingrese Detalle Pedido: ");
            string? observacion=Console.ReadLine();
            Console.WriteLine("Estado pedido: {0}",Enum.GetName(typeof(Pedido.Estados),0));
            //foreach(int i in Enum.GetValues(typeof(Pedido.Estados)))
            //Console.WriteLine("{0} --> {1}",i,Enum.GetName(typeof(Pedido.Estados),i));
            //Console.Write("Seleccion: ");
            int estado=0;
            Cliente nuevoCL=CargaDatosCliente();
            if(nuevoCL!=null){
                nuevoPedido= new Pedido(nro_pedido,observacion!,estado,nuevoCL);
            }else{
                Console.Write("FALLO CARGA DATOS CLIENTE");
                nuevoPedido=null;
            }
            return nuevoPedido!;
        }
        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        private static Cliente CargaDatosCliente(){
            Cliente? nuevoCL=null;
            int opc=1;
            while(opc!=0)
            {
                Console.WriteLine("DATOS DEL CLIENTE");
                Console.Write("Id: ");
                string? valorID=Console.ReadLine();
                int id;
                bool cambioID=int.TryParse(valorID,out id);
                //int id =Convert.ToInt32(Console.ReadLine());
                Console.Write("Nombre: ");
                string? nom =Console.ReadLine();
                Console.Write("Direccion: ");
                string? dire =Console.ReadLine();
                Console.Write("Numero: ");
                string? valorDir=Console.ReadLine();
                //int numDir =Convert.ToInt32(Console.ReadLine());
                int numDir;
                bool cambioDir=int.TryParse(valorDir,out numDir);
                Console.Write("Telefono: ");
                string? tel =Console.ReadLine();
                Console.Write("Referencia direccion: ");
                string? direRefe =Console.ReadLine();
                if(cambioID&&cambioDir){
                    nuevoCL=new Cliente(id,nom!,dire!,numDir,tel!,direRefe!);
                    opc=0; 
                }else{
                    Console.WriteLine("FALLO CARGA DATOS CLIENTE");
                    Console.WriteLine("Algunos de los datos Id cliente o Direccion cliente es invalido");
                    Console.WriteLine("Reingrese datos con los valores numericos correspondientes, sino.");
                    Console.WriteLine("0 --> Finalizar");
                    Console.WriteLine("1 --> Continuar");
                    string? opcAux=Console.ReadLine();
                    if(!((int.TryParse(opcAux,out opc)) && (opc>=0) && (opc<=1))){
                        Console.Write("Opcion numerica no valida");
                        Console.Write("Por defecto se le solicitara nuevamente la carga de los datos");
                    }
                }
            }
            return nuevoCL!;
        }
        //+++++++++++++++++++++++++++++++++
        private static void AsignarPedidoCadete(Cadeteria sistema, Pedido nuevoPedido){
            Console.Write("Ingrese el ID del cadete: ");
            string? valorCadete = Console.ReadLine();
            Console.WriteLine();
            int cadeteId;
            if(int.TryParse(valorCadete, out cadeteId)){
                foreach (var item in sistema.getCadetes())
                {
                    if(item.getID()==cadeteId){
                        item.agregarPedido(nuevoPedido!);
                        Console.WriteLine("===============================Resumen================================");
                        nuevoPedido.setEstado();
                        item.listar_info_cadete();
                        return;
                    }
                }
            }else{
                Console.Write("Ingreso de id cadete invalido");
            }
            
        }
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        public static void cambiar_estado_pedido(Cadeteria sistema){
            Console.Write("Ingrese el ID del cadete: ");
            int numCadete=Convert.ToInt32(Console.ReadLine());
            Console.Write("Ingrese el ID del pedido: ");
            int numpedido=Convert.ToInt32(Console.ReadLine());
            foreach (var item in sistema.getCadetes())
            {
                if(item.getID()==numCadete){
                    foreach (var item2 in item.getPedidos())
                    {
                        if(item2.getIdPedi2()==numpedido){
                            item2.setEstado();
                            return;
                        }
                    }
                }
            }
        }
        public static void cambiar_cadete_pedido(Cadeteria sistema){
            Console.Write("Ingrese el ID del cadete: ");
            int numCadete=Convert.ToInt32(Console.ReadLine());
            Console.Write("Ingrese el ID del pedido: ");
            int numpedido=Convert.ToInt32(Console.ReadLine());
            foreach (var item in sistema.getCadetes())
            {
                if(item.getID()==numCadete){
                    foreach (var item2 in item.getPedidos())
                    {
                        if(item2.getIdPedi2()==numpedido){
                            item.eliminarPedido(item2);
                            Console.Write("Ingrese el ID del nuevo cadete: ");
                            int numCadet=Convert.ToInt32(Console.ReadLine());
                            Cadete cdt;
                            foreach (var item3 in sistema.getCadetes())
                            {
                                if(item3.getID()==numCadet){
                                    cdt=item3;
                                    item3.agregarPedido(item2);
                                    Console.WriteLine("===============================Resumen================================");
                                    item2.listar_info_pedido();
                                    Console.Write("CAMBIO DE CADETES DEL PEDIDO");
                                    Console.Write("PEDIDO TENIDO POR:");
                                    item.listar_info_cadete();
                                    Console.Write("PEDIDO ASIGNADO A:");
                                    item3.listar_info_cadete();
                                    return;
                                }
                            }
                        }
                    }
                }
            }
        }
        private static void informe_pedidos(Cadeteria sistema){
            float montoTotalPago=0;
            int cantidadTotalPedido=0;
            foreach (var cadete in sistema.getCadetes())
            {
                cadete.listar_info_cadete();
                Console.WriteLine("Cantidad de pedidos: {0}",cadete.CantidadPedidos());
                cantidadTotalPedido+=cadete.CantidadPedidos();
                Console.WriteLine("Monto a cobrar: {0}",cadete.jornalAcobrar(sistema.getMontoPago()));
                montoTotalPago+=sistema.getMontoPago();
            }
            Console.WriteLine("===============================Resumen================================");
            Console.WriteLine("Cantidad total de pedidos: {0}",cantidadTotalPedido);
            Console.WriteLine("Cantidad total de pago: {0}",montoTotalPago);
            float promedio=Convert.ToSingle(cantidadTotalPedido)/Convert.ToSingle(sistema.getCadetes().Count());
            Console.WriteLine("Envios promedio por cadete: {0}",Math.Round(promedio,2));
        }
        public static void pruebaCargaPedidos(Cadeteria sistema){
            //Cliente(int iden, string nom, string dir,int num, string tel, string dirREf)
            Cliente cliente1=new Cliente(1,"juan","alsina",156,"121212121","direRefenulo");
            Cliente cliente2=new Cliente(1,"juan","alsina",156,"121212121","direRefenulo");
            Cliente cliente3=new Cliente(1,"juan","alsina",156,"121212121","direRefenulo");
            Cliente cliente4=new Cliente(1,"juan","alsina",156,"121212121","direRefenulo");
            //Pedido(int num, string obs, int estado, Cliente cl)
            Pedido pedido1= new Pedido(sistema.getNumPedido()+1,"computadora",0,cliente1);
            sistema.setNumPedido();
            Pedido pedido2= new Pedido(sistema.getNumPedido()+1,"computadora",0,cliente2);
            sistema.setNumPedido();
            Pedido pedido3= new Pedido(sistema.getNumPedido()+1,"computadora",0,cliente3);
            sistema.setNumPedido();
            Pedido pedido4= new Pedido(sistema.getNumPedido()+1,"computadora",0,cliente4);
            sistema.setNumPedido();
            sistema.getCadetes()[5].agregarPedido(pedido1);
            sistema.getCadetes()[6].agregarPedido(pedido2);
            sistema.getCadetes()[7].agregarPedido(pedido3);
            sistema.getCadetes()[8].agregarPedido(pedido4);
        }
        
    }
}
