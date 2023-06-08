using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "IA/Estado")]
public class IAEstado : ScriptableObject
{
    public IAAccion[] Acciones;
    public IATransicion[] Transiciones;


    public void EjecutarEstado(IAController controller)
    {
        EjecutarAcciones(controller);
        RealizarTransicones(controller);
    }
    private void EjecutarAcciones (IAController controller)
    {//comprobacion de seguridad para q retorne si no hay "valor"
        if (Acciones == null || Acciones.Length <=0)
        {
            return;
        }
        
        //recorre todas las acciones
        for (int i = 0; i < Acciones.Length; i++)
        {//Ejecuta la accion que entra en controller
            Acciones[i].Ejecutar(controller);
        }
    }

    private void RealizarTransicones(IAController controller)
    {
        if (Transiciones == null || Transiciones.Length <= 0)
        {
            return;
        }

        //se va a obtener el valor de la decion de cada transion
        for (int i = 0; i < Transiciones.Length; i++)
        {
            bool decisionValor = Transiciones[i].desicion.Decidir(controller);
            //si decisionValor es verdadero
            if (decisionValor)
            {//se canbia el esatdo
                controller.CambiarEstado(Transiciones[i].EstadoVerdadero);
            }
            else
            {
                controller.CambiarEstado(Transiciones[i].EstadoFalso);
            }
        }
    }
}
