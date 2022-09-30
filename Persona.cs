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
    }
}