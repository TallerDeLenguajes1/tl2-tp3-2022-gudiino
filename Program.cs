using System;
namespace EmpresaCadeteria{
    class Program{
        static void Main(string[] args){
            Persona persona1=new Persona(10101010, "Juan Manuel", "San martin", 156,"1515151515");
            Cadeteria nuevo = new Cadeteria("Los Rapidos", "1234567890", 300);
            nuevo.cargarCadetes();
            nuevo.mostrarDatos();
            Console.WriteLine("FIN");
        }
        
    }
}
