using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Personaje : MonoBehaviour
{
    //Referencia a los stats para poder usarlos
    [SerializeField] private PersonajeStats stats;


    //Uso una propiedad para poder usarla en LevelManager
    public PersonajeExperiencia PersonajeExperiencia { get; private set; }
    public PersonajeVida PersonajeVida { get; private set; }
    public PersonajeAnimaciones PersonajeAnimaciones { get; private set; }
    public PersonajeMana PersonajeMana { get; private set; }
    public PersonajeAtaque PersonajeAtaque { get; private set; }


    private void Awake(){
        PersonajeAtaque = GetComponent<PersonajeAtaque>();
        PersonajeVida = GetComponent<PersonajeVida>();
        PersonajeAnimaciones = GetComponent<PersonajeAnimaciones>();
        PersonajeMana = GetComponent<PersonajeMana>();
        PersonajeExperiencia = GetComponent<PersonajeExperiencia>();
    }

    public void RestaurarPersonaje(){
        PersonajeVida.RestaurarPersonaje();
        PersonajeAnimaciones.RevivirPersonaje();
        PersonajeMana.RestablecerMana();

    }

    private void AtributoRespuesta(TipoAtributo tipo)
    {
        if (stats.PuntosDispponibles <= 0)
        {
            return;
        }

        switch (tipo)
        {
            case TipoAtributo.Fuerza:
                stats.Fuerza++;
                stats.AñadirBonuPorAtributoFuerza();
                break;
            case TipoAtributo.Inteligencia:
                stats.Inteligencia++;
                stats.AñadirBonuPorAtributoInteligencia();
                break;
            case TipoAtributo.Destreza:
                stats.Destreza++;
                stats.AñadirBonuPorAtributoDestreza();
                break;
        }
        stats.PuntosDispponibles--;
    }

    //Para escuchar los eventos añadimos estas funciones
    private void OnEnable()
    {
        AtributosButton.EventoAgregarAtributo += AtributoRespuesta;
    }

    private void OnDisable()
    {
        AtributosButton.EventoAgregarAtributo -= AtributoRespuesta;
    }

}
