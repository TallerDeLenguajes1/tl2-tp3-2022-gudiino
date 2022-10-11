using System;
using System.Linq;
using System.IO;
using System.Net;
using NLog;
namespace EmpresaCadeteria{
    class Program{
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        static void Main(string[] args){
            Console.WriteLine();
            Console.WriteLine("INICIANDO SISTEMA...");
            Cadeteria sistema=new Cadeteria();
            List<Pedido> pedidos=new List<Pedido>();
            pedidos=pruebaCargaPedidos(sistema,pedidos);
            Console.WriteLine();
            Console.WriteLine("ENTER para IR al MENU");
            //Console.ReadKey();
            while (Console.ReadKey().Key != ConsoleKey.Enter){}
            Console.WriteLine();
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
                            Console.WriteLine("                  SISTEMA GESTION DE PEDIDOS CERRADO");
                            Console.WriteLine("======================================================================");
                            opcAux="0";
                            break;
                        case 1:
                            ;
                            Console.WriteLine("++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
                            //altaPedido(sistema);
                            pedidos=altaPedido2(sistema,pedidos);
                            Continuar();
                            break;
                        case 2:
                            Console.WriteLine("++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
                            AsignarPedidoCadete2(sistema, pedidos);
                            Console.WriteLine();
                            break;
                        case 3:
                            Console.WriteLine("++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
                            cambiar_estado_pedido(sistema, pedidos);
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
                    Console.WriteLine("======================================================================");
                    Console.WriteLine("      NO INGRESO NINGUN DATO O INGRESO UN DATO U OPCION NO VALIDA");
                    Console.WriteLine("    SOLO DEBE INGRESAR EL NUMERO DE LA OPCION A SELECCIONAR DEL MENU");
                    Console.WriteLine("======================================================================");
                    opc=FinalizarReintentar();
                    Console.WriteLine();
                    if(opc==0){
                        Console.WriteLine("======================================================================");
                        Console.WriteLine("                 SISTEMA GESTION DE PEDIDOS CERRADO");
                        Console.WriteLine("======================================================================");
                    }
                }
            }
            Console.WriteLine();
            Console.WriteLine("FIN");
        }
        //*******************************
        private static int conversion_a_entero(){
            int dato;
            string? dato_in=Console.ReadLine();
            try
            {
                dato=Convert.ToInt32(dato_in);
                return dato;
            }
            catch (FormatException)
            {
                Console.WriteLine("Ingreso un valor INVALIDO, debe ingresar un numeros ENTERO");
                throw;
            }
            catch(OverflowException)
            {
                Console.WriteLine("Ingreso un valor demasiado GRANDE");
                throw;
            }
            catch(Exception ex)
            {
                Console.WriteLine("OPERACION no valida");
                Console.WriteLine("+++ MENSAJE EXCEPCION:");
                Console.WriteLine(ex.Message);
                Logger.Warn(ex);
                Logger.Error(ex);
                Logger.Fatal(ex);
                throw;
            }
        }
        //*******************************************************
        private static void Continuar()
        {
            Console.WriteLine();
            Console.WriteLine("ENTER para VOLVER al MENU");
            Console.ReadKey();
            Console.WriteLine();
        }
        private static void menu_principal(){
            Console.WriteLine("________MENU PRINCIPAL________");
            Console.WriteLine();
            Console.WriteLine("1 --> ALTA PEDIDO"); 
            Console.WriteLine("2 --> ASIGNAR CADETE");
            Console.WriteLine("3 --> CAMBIAR ESTADO PEDIDO");
            Console.WriteLine("4 --> CAMBIAR CADETE");
            Console.WriteLine("5 --> INFORME PEDIDOS");
            Console.WriteLine("6 --> MOSTRAR DATOS CADETERIA");
            Console.WriteLine("0 --> FINALIZAR");
            Console.WriteLine();
        }
        public static void altaPedido(Cadeteria sistema){
            //ingreso datos del pedido
            int opc=1;
            while(opc!=0)
            {
                Pedido nuevoPedido=CargaDatosPedido(sistema.getNumPedido());
                if (nuevoPedido!=null){
                    Cadeteria.setNumPedido();
                    AsignarPedidoCadete(sistema, nuevoPedido);
                    opc=0;
                }else{
                    Console.WriteLine("FALLO ALTA PEDIDO");
                    opc=FinalizarReintentar();
                }
            }
        }
        public static List<Pedido> altaPedido2(Cadeteria sistema, List<Pedido> pedidos){
            //ingreso datos del pedido
            int opc=1;
            while(opc!=0)
            {
                Pedido nuevoPedido=CargaDatosPedido(sistema.getNumPedido());
                if (nuevoPedido!=null){
                    Cadeteria.setNumPedido();
                    pedidos.Add(nuevoPedido);
                    opc=0;
                }else{
                    Console.WriteLine("FALLO ALTA PEDIDO");
                    opc=FinalizarReintentar();
                }
            }
            return pedidos;
        }
        private static void AsignarPedidoCadete2(Cadeteria sistema, List<Pedido> pedidos){
            int opc=1;
            sistema.mostra_lista_cadetes();
            int rango=sistema.getCadetes().Count;
            while(opc!=0)
            {
                Console.Write("Ingrese el ID de algun cadete de la lista: ");
                string? valorCadete = Console.ReadLine();
                Console.WriteLine();
                int cadeteId;
                if(int.TryParse(valorCadete, out cadeteId) && cadeteId>=1 && cadeteId<=rango){
                    foreach (var item in sistema.getCadetes())
                    {
                        if(item.getID()==cadeteId){
                            if(pedidos.Count!=0)
                            {
                                foreach (var pedi in pedidos)
                                {
                                    pedi.listar_info_pedido();
                                }
                                Console.Write("\nIngrese el ID de algun pedido de la lista: ");
                                string? valorPedi2 = Console.ReadLine();
                                int pedidoId;
                                if(int.TryParse(valorPedi2, out pedidoId)){
                                    foreach (var item3 in pedidos)
                                    {
                                        if(item3.getIdPedi2()==pedidoId){
                                            item.agregarPedido(item3!);
                                            pedidos.Remove(item3);
                                            item3.setEstado();
                                            item.listar_info_cadete();
                                            Console.WriteLine("+++++++++++++++++++++++++++");
                                            Console.WriteLine("+    ASIGNACION EXITOSA   +");
                                            Console.WriteLine("+++++++++++++++++++++++++++");
                                            Continuar();
                                            return;
                                        }
                                    }
                                }else{
                                    Console.WriteLine("Ingreso de dato pedido invalido");
                                    Console.WriteLine("Reingrese datos con los valores numericos correspondientes, sino.");
                                    opc=FinalizarReintentar();
                                }
                            }else{
                                Console.WriteLine("No hay pedidos en la lista para ser asignado al cadete");
                                opc=0;
                                Continuar();
                            }
                        }
                    }
                }else{
                    Console.WriteLine("Ingreso de dato cadete invalido");
                    Console.WriteLine("Reingrese datos con los valores numericos correspondientes, sino.");
                    opc=FinalizarReintentar();
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
            Console.WriteLine("Estado inicial pedido: {0}",Enum.GetName(typeof(Pedido.Estados),0));
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
                    opc=FinalizarReintentar();
                }
            }
            return nuevoCL!;
        }
        //+++++++++++++++++++++++++++++++++
        private static void AsignarPedidoCadete(Cadeteria sistema, Pedido nuevoPedido){
            int opc=1;
            while(opc!=0)
            {
                Console.Write("Ingrese el ID del cadete: ");
                string? valorCadete = Console.ReadLine();
                Console.WriteLine();
                int cadeteId;
                if(int.TryParse(valorCadete, out cadeteId)){
                    foreach (var item in sistema.getCadetes())
                    {
                        if(item.getID()==cadeteId){
                            item.agregarPedido(nuevoPedido!);
                            nuevoPedido.setEstado();
                            item.listar_info_cadete();
                            return;
                        }
                    }
                }else{
                    Console.WriteLine("Ingreso de id cadete invalido");
                    Console.WriteLine("Reingrese datos con los valores numericos correspondientes, sino.");
                    opc=FinalizarReintentar();
                }
            } 
        }
        private static int FinalizarReintentar(){
            int opc=1;
            Console.WriteLine();
            Console.WriteLine("0 --> Finalizar");
            Console.WriteLine("1 --> Reintentar");
            Console.Write("Seleccion: ");
            string? opcAux=Console.ReadLine();
            if(!((int.TryParse(opcAux,out opc)) && (opc>=0) && (opc<=1))){
                Console.WriteLine("======================================================================");
                Console.WriteLine("     NO INGRESO NINGUN DATO O INGRESO UN DATO U OPCION NO VALIDA");
                Console.WriteLine("          POR DEFECTO SE LE SOLICITARA NUEVAMENTE LOS DATOS");
                Console.WriteLine("======================================================================");
                opc=1;
                Continuar();
            }
            return opc;
        }
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        public static void cambiar_estado_pedido(Cadeteria sistema,List<Pedido> pedidos){
            int opc=1;
            sistema.mostra_lista_cadetes();
            int rango=sistema.getCadetes().Count;
            while(opc!=0)
            {
                Console.Write("Ingrese el ID del cadete: ");
                string? valorCadete=Console.ReadLine();
                int cadeteId;
                if(int.TryParse(valorCadete, out cadeteId) && cadeteId>=1 && cadeteId<=rango){
                    foreach (var cdt in sistema.getCadetes())
                    {
                        if(cdt.getID()==cadeteId){
                            cdt.mostrar_lista_pedidos();
                            Console.Write("\nIngrese el ID de algun pedido de la lista: ");
                            string? valorPedi2 = Console.ReadLine();
                            int pedidoId;
                            if(int.TryParse(valorPedi2, out pedidoId)){
                                foreach (var pedi in cdt.getPedidos()){
                                    if(pedi.getIdPedi2()==pedidoId){
                                        pedi.setEstado();
                                        cdt.listar_info_cadete();
                                        Console.WriteLine("++++++++++++++++++++++++++++++");
                                        Console.WriteLine("+  CAMBIO DE ESTADO EXITOSO  +");
                                        Console.WriteLine("++++++++++++++++++++++++++++++");
                                        Continuar();
                                        return;
                                    }
                                }
                            }else{
                                Console.WriteLine("Ingreso de id pedido invalido");
                                Console.WriteLine("Reingrese datos con los valores numericos correspondientes, sino.");
                                opc=FinalizarReintentar();
                                Continuar();
                            }
                        }
                    }
                }else{
                    Console.WriteLine("Ingreso de id cadete invalido");
                    Console.WriteLine("Reingrese datos con los valores numericos correspondientes, sino.");
                    opc=FinalizarReintentar();
                    Continuar();
                }
            }
        }
        public static void cambiar_cadete_pedido(Cadeteria sistema){
            int opc=1;
            bool auxCdt=false;
            bool auxCdt2=false;
            bool auxPd2=false;
            Console.WriteLine("GESTION CAMBIO DE CADETES DE UN PEDIDO");
            while(opc!=0){
                Console.WriteLine("LISTADO DE CADETES:");
                sistema.mostra_lista_cadetes();
                Console.Write("\nIngrese el ID del cadete con el pedido: ");
                string? inCadete1=Console.ReadLine();
                int buscaCdt1;
                if(int.TryParse(inCadete1,out buscaCdt1)){
                    foreach (var cdt1 in sistema.getCadetes())
                    {
                        if(cdt1.getID()==buscaCdt1){
                            auxCdt=true;
                            Console.WriteLine("Lista de pedidos del cadete ID {0}",cdt1.getID());
                            cdt1.mostrar_lista_pedidos();
                            Console.WriteLine("\nIngrese el ID del pedido a cambiar: ");
                            string? numP=Console.ReadLine();
                            int buscaPd2;
                            if (int.TryParse(numP,out buscaPd2)){
                                foreach (var pd2 in cdt1.getPedidos()){
                                    if(pd2.getIdPedi2()==buscaPd2){
                                        auxPd2=true;
                                        Console.WriteLine("SELECCION DEL NUEVO CADETE: \n");
                                        sistema.mostra_lista_cadetes();
                                        Console.Write("\nIngrese el ID del cadete quien tendra el pedido: ");
                                        string? numCdtIn2=Console.ReadLine();
                                        int numCadete2;
                                        if(int.TryParse(numCdtIn2,out numCadete2)){
                                            foreach (var cdt2 in sistema.getCadetes()){
                                                if(cdt2.getID()==numCadete2){
                                                    cdt2.agregarPedido(pd2);
                                                    cdt1.eliminarPedido(pd2);
                                                    Console.WriteLine("GESTION CAMBIO CADETE EXITOSA\n");
                                                    pd2.listar_info_pedido();
                                                    Console.WriteLine("CADETE CON PEDIDO ANULADO");
                                                    cdt1.listar_info_cadete();
                                                    Console.WriteLine("\nCADETE CON PEDIDO ASIGNADO");
                                                    cdt2.listar_info_cadete();
                                                    return;
                                                }
                                            }
                                            if(!auxCdt2){
                                                Console.WriteLine("Ingreso de id cadete invalido, no figura en la lista");
                                                Console.WriteLine("Reingrese datos con los valores numericos correspondientes al cadete");
                                                opc=1;
                                                Continuar();
                                            }
                                        }else{
                                            Console.WriteLine("Ingreso de id cadete invalido");
                                            Console.WriteLine("Reingrese datos con los valores numericos correspondientes, sino.");
                                            opc=1;
                                            Continuar();
                                        }
                                    }
                                }
                                if(!auxPd2){
                                    Console.WriteLine("Ingreso de id pedido invalido, no figura en la lista");
                                    Console.WriteLine("Reingrese datos con los valores numericos correspondientes al pedido del cadete");
                                    opc=1;
                                    Continuar();
                                }

                            }else{
                                Console.WriteLine("Ingreso de id pedido invalido");
                                Console.WriteLine("Reingrese datos con los valores numericos correspondientes, sino.");
                                opc=FinalizarReintentar();
                                Continuar();
                            }
                        }
                    }
                    if(!auxCdt){
                        Console.WriteLine("Ingreso de id cadete invalido, no figura en la lista");
                        Console.WriteLine("Reingrese datos con los valores numericos correspondientes al cadete");
                        opc=1;
                        Continuar();
                    }
                }else{
                    Console.WriteLine("Ingreso de valor id cadete invalido");
                    Console.WriteLine("Reingrese datos con los valores numericos correspondientes");
                    opc=1;
                    Continuar();
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
        public static List<Pedido> pruebaCargaPedidos(Cadeteria sistema,List<Pedido> pedidos){
            //Cliente(int iden, string nom, string dir,int num, string tel, string dirREf)
            Cliente cliente1=new Cliente(1,"juan","alsina",156,"121212121","direRefenulo");
            Cliente cliente2=new Cliente(1,"marcos","alsina",156,"121212121","sin puerta");
            Cliente cliente3=new Cliente(1,"eli","alsina",156,"121212121","esquina");
            Cliente cliente4=new Cliente(1,"zulema","alsina",156,"121212121","casa roja");
            //Pedido(int num, string obs, int estado, Cliente cl)
            Pedido pedido1= new Pedido(sistema.getNumPedido()+1,"computadora",0,cliente1);
            Cadeteria.setNumPedido();
            //pedido1.listar_info_pedido();
            pedidos.Add(pedido1);
            //Console.WriteLine();
            Pedido pedido2= new Pedido(sistema.getNumPedido()+1,"lampara",0,cliente2);
            Cadeteria.setNumPedido();
            //pedido2.listar_info_pedido();
            pedidos.Add(pedido2);
            //Console.WriteLine();
            Pedido pedido3= new Pedido(sistema.getNumPedido()+1,"facturas",0,cliente3);
            Cadeteria.setNumPedido();
            //pedido3.listar_info_pedido();
            pedidos.Add(pedido3);
            //Console.WriteLine();
            Pedido pedido4= new Pedido(sistema.getNumPedido()+1,"supermercado",0,cliente4);
            Cadeteria.setNumPedido();
            //pedido4.listar_info_pedido();
            //Console.WriteLine();
            sistema.getCadetes()[5].agregarPedido(pedido1);
            sistema.getCadetes()[6].agregarPedido(pedido2);
            sistema.getCadetes()[7].agregarPedido(pedido3);
            sistema.getCadetes()[8].agregarPedido(pedido4);
            return pedidos;
        }
        
    }
}
