using System;
namespace EmpresaCadeteria{
    class Pedido{
        private int num_pedido {get; set;}
        private string observacion {get; set;}
        protected Cliente? nuevo_cl {get; set;}
        private string estado_entrega {get; set;}
        public Pedido(int num, string obs, string estado){
            num_pedido=num;
            observacion=obs;
            estado_entrega=estado;
        }
        public void setEstado(){
            
        }
    }
    
}