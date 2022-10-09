using System;
using System.Linq;
namespace EmpresaCadeteria{
    class Program{
        static void Main(string[] args){
            Console.WriteLine("INICIANDO SISTEMA");
            Cadeteria sistema=new Cadeteria();
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
                            pruebaCargaPedidos(sistema);
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
                            Console.WriteLine();
                            break;
                        case 6:
                            Console.WriteLine("======================================================================");
                            Console.WriteLine("DATOS CADETERIA");
                            sistema.listar_info_cadeteria();
                            Console.WriteLine();
                            break;
                    }
                }else{
                    Console.WriteLine("===========================");
                    Console.WriteLine(" Ingrese una opcion Valida");
                    Console.WriteLine("===========================");
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
            Console.WriteLine("0 --> FINALIZAR");
            Console.WriteLine();
        }
        public static void altaPedido(Cadeteria sistema){
            //ingreso datos del pedido
            Console.WriteLine("DATOS DEL PEDIDO");
            int nro_pedido=sistema.getNumPedido()+1;
            Console.WriteLine("Numero de pedido: {0}", nro_pedido);
            Console.Write("Ingrese observacion pedido: ");
            string? observacion=Console.ReadLine();
            Console.WriteLine("Estado pedido: {0}",Enum.GetName(typeof(Pedido.Estados),0));
            //foreach(int i in Enum.GetValues(typeof(Pedido.Estados)))
            //Console.WriteLine("{0} --> {1}",i,Enum.GetName(typeof(Pedido.Estados),i));
            //Console.Write("Seleccion: ");
            int estado=0;
            //ingreso dato del cliente
            //Cliente(int iden, string nom, string dir,int num, string tel, string dirREf)
            Console.WriteLine("DATOS DEL CLIENTE");
            Console.Write("Id: ");
            int id =Convert.ToInt32(Console.ReadLine());
            Console.Write("Nombre: ");
            string? nom =Console.ReadLine();
            Console.Write("Direccion: ");
            string? dire =Console.ReadLine();
            Console.Write("Numero: ");
            int numDir =Convert.ToInt32(Console.ReadLine());
            Console.Write("Telefono: ");
            string? tel =Console.ReadLine();
            Console.Write("Referencia direccion: ");
            string? direRefe =Console.ReadLine();
            Cliente nuevoCL=new Cliente(id,nom!,dire!,numDir,tel!,direRefe!);
            Pedido nuevoPedido= new Pedido(nro_pedido,observacion!,estado,nuevoCL);
            if (nuevoPedido!=null){
                sistema.setNumPedido();
            }else{
                Console.Write("FALLO ALTA PEDIDO");
            }
            // //asignar cadete
            Console.Write("Ingrese el ID del cadete: ");
            Console.WriteLine();
            int numCadet=Convert.ToInt32(Console.ReadLine());
            Cadete cdt;
            foreach (var item in sistema.getCadetes())
            {
                if(item.getID()==numCadet){
                    cdt=item;
                    item.agregarPedido(nuevoPedido!);
                    Console.WriteLine("===============================Resumen================================");
                    nuevoPedido!.listar_info_pedido();
                    item.listar_info_cadete();
                    return;
                }
            }
        }
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
            Pedido pedido1= new Pedido(1,"computadora",0,cliente1);
            Pedido pedido2= new Pedido(1,"computadora",0,cliente2);
            Pedido pedido3= new Pedido(1,"computadora",0,cliente3);
            Pedido pedido4= new Pedido(1,"computadora",0,cliente4);
            sistema.getCadetes()[5].agregarPedido(pedido1);
            sistema.getCadetes()[6].agregarPedido(pedido2);
            sistema.getCadetes()[7].agregarPedido(pedido3);
            sistema.getCadetes()[8].agregarPedido(pedido4);
        }
        
    }
}
