using System;
namespace EmpresaCadeteria{
    public class Persona {
        private int id {get; set;}
        private string nombre {get; set;}
        private string calle {get; set;}
        private int numero {get; set;}
        private string telefono {get; set;}
        public Persona(int iden, string nom, string dir, int num, string tel){
            id=iden;
            nombre=nom;
            calle=dir;
            numero=num;
            telefono=tel;
        }
        public void listarinfo(){
            Console.WriteLine("Id: {0}; Nombre: {1}; Calle: {2}; Numero: {3}; Telefono: {4}",id,nombre,calle,numero,telefono);
            //Console.WriteLine("Nombre: {0}",nombre);
            //Console.WriteLine("Calle: {0}",calle);
            //Console.WriteLine("Numero: {0}",numero);
            //Console.WriteLine("Telefono: {0}",telefono);
            Console.WriteLine();
        }
    }
}