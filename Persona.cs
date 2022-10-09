using System;
namespace EmpresaCadeteria{
    public class Persona {
        protected int id {get; set;}
        protected string nombre {get; set;}
        protected string calle {get; set;}
        protected int numero {get; set;}
        protected string telefono {get; set;}
        public Persona(int iden, string nom, string dir, int num, string tel){
            id=iden;
            nombre=nom;
            calle=dir;
            numero=num;
            telefono=tel;
        }
        public void listar_info_persona(){
            //Console.WriteLine("Id: {0}; Nombre: {1}; Calle: {2}; Numero: {3}; Telefono: {4}",id,nombre,calle,numero,telefono);
            //Console.WriteLine("Id     |Nombre      |Calle              |Numero       |Telefono");
            Console.Write(id);
            for (int i = 0; i < (8-(int)Math.Floor(Math.Log10(id) + 1)); i++)Console.Write(" ");
            Console.Write(nombre);
            for (int i = 0; i < (13-nombre.Count()); i++)Console.Write(" ");
            Console.Write(calle);
            for (int i = 0; i < (20-calle.Count()); i++)Console.Write(" ");
            Console.Write(numero);
            for (int i = 0; i < (14-(int)Math.Floor(Math.Log10(numero) + 1)); i++)Console.Write(" ");
            Console.Write(telefono);
            Console.WriteLine();
        }
        public int getID(){
            return id;
        }
    }
}