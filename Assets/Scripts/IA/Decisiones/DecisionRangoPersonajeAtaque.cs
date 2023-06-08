using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//esta desicion nos va indicar si podemos hacer la transicion
//de un estado al de atacar personaje

[CreateAssetMenu(menuName = "IA/Decisiones/Personaje en rango de ataque")]
public class DecisionRangoPersonajeAtaque : IADesicion
{
    public override bool Decidir(IAController controller)
    {
        return EnRangoDeAtaque(controller);
    }

    private bool EnRangoDeAtaque(IAController controller)
    {
        if (controller.PersonajeReferencia == null)
        {
            return false;
        }

        float distancia = (controller.PersonajeReferencia.position - controller.transform.position).sqrMagnitude;

        if (distancia < Mathf.Pow(controller.RangoDeAtaquePredeterminado, 2))
        {
            return true;
        }

        return false;
    }
}
