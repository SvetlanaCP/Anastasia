using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "IA/Acciones/Seguir Personaje")]
public class AccionSeguirPersonaje : IAAccion
{
    public override void Ejecutar(IAController controller)
    {
        SeguirPersonaje(controller);
    }

    private void SeguirPersonaje(IAController controller)
    {
        if (controller.PersonajeReferencia == null)
        {
            return;
        }
        //obtengo la direccion hacia el personaje 
        Vector3 direcHaciaPersonaje = controller.PersonajeReferencia.position - controller.transform.position;
        Vector3 direccion = direcHaciaPersonaje.normalized;

        float distancia = direcHaciaPersonaje.magnitude;

        if (distancia >= 1.30f)
        {
            controller.transform.Translate(
                direccion * controller.VelocidadMovimiento * Time.deltaTime);
        }


    }
}
