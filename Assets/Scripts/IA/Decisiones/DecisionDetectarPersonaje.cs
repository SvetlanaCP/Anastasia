using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//como va a ser un scriptableObjet vamos a referenciarlo
[CreateAssetMenu(menuName ="IA/Decisiones/Detectar Personaje") ]
public class DecisionDetectarPersonaje : IADesicion
{
    public override bool Decidir(IAController controller)
    {
        return DetectarPersonaje(controller);
    }

    private bool DetectarPersonaje(IAController controller)
    {//guarda en una variable si hemos colisionado con un personaje, (Physics2D.OverlapCircle metodo de unity para guardar referencia)
        Collider2D personajeDetectado = Physics2D.OverlapCircle(controller.transform.position,
                controller.RangoDeteccion, controller.PersonajeLayerMask);
        if (personajeDetectado != null)
        {
            controller.PersonajeReferencia = personajeDetectado.transform;
            return true;
        }
        controller.PersonajeReferencia = null;
        return false;
    }
}
