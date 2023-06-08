using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonajeDetector : MonoBehaviour
{
    public static Action<EnemigoInteraccion> EventoEnemigoDetectado;
    public static Action EventoEnemigoPerdido;
    
    
    //para guardar la referencia del enemigo
    public EnemigoInteraccion EnemigoDetectado { get; private set; }
    private void OnTriggerEnter2D(Collider2D other)
    {//si el objeto que cole¡icionamos tiene un tag de enemigo
        if (other.CompareTag("Enemigo"))
        {//referencia del enemigo
            EnemigoDetectado = other.GetComponent<EnemigoInteraccion>();
            //si el enemigo esta vivo
            if (EnemigoDetectado.GetComponent<EnemigoVida>().Salud > 0)
            {//se puede mostrar q esta selecionado
                EventoEnemigoDetectado?.Invoke(EnemigoDetectado);
            }
            
            
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemigo"))
        {
            EventoEnemigoPerdido?.Invoke();
        }
    }
}
