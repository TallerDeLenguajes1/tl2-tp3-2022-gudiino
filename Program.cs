using System;
namespace EmpresaCadeteria{
    class Program{
        static void Main(string[] args){
            Persona persona1=new Persona(10101010, "Juan Manuel", "San martin", 156,"1515151515");
            Console.WriteLine("FIN");
        }
        private void cargarCadetes(){
            string archivo = "listaCadetes.csv";
            List<string[]> lista_cadetes=HelperDeArchivos.LeerCsv(archivo,',');
            foreach (var item in lista_cadetes)
            {
                
            }
        }
    }
}
