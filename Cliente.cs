using System;
namespace EmpresaCadeteria{
    public class Cliente:Persona {
        private string detalle_direccion {get; set;}//sobre la ubicacion de la casa
        public Cliente(int iden, string nom, string dir,int num, string tel, string dirREf):base(iden, nom, dir, num, tel){
            detalle_direccion=dirREf;
        }
    }
}