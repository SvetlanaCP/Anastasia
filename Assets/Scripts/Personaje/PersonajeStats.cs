using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Stats")]
public class PersonajeStats : ScriptableObject
{
    [Header("Stats")]
    public float Da�o = 5f;
    public float Defensa = 2f; 
    public float Velociadad = 5f; 
    public float NIvel;
    public float ExpActual;
    public float ExpRequerida;
    [Range(0f, 100f)] public float PorcentajeCritico;
    [Range(0f, 100f)] public float PorcentajeBloqueo;

    [Header("Atributos")]
    public int Fuerza;
    public int Inteligencia;
    public int Destreza;

    [HideInInspector]public int PuntosDispponibles;


    public void A�adirBonuPorAtributoFuerza()
    {
        Da�o += 2f;
        Defensa += 1f;
        PorcentajeBloqueo += 0.3f;
    }

    public void A�adirBonuPorAtributoInteligencia()
    {
        Da�o += 3f;
        PorcentajeCritico += 0.2f;
    }

    public void A�adirBonuPorAtributoDestreza()
    {
        Velociadad += 0.1f;
        PorcentajeBloqueo += 0.03f;

    }

    public void A�adirBonusPorArma(Arma arma)
    {
        Da�o += arma.Da�o;
        PorcentajeCritico += arma.ChanceCritico;
        PorcentajeBloqueo += arma.ChanceBloqueo;
    }

    public void RemoverBonusPorArma(Arma arma)
    {
        Da�o -= arma.Da�o;
        PorcentajeCritico -= arma.ChanceCritico;
        PorcentajeBloqueo -= arma.ChanceBloqueo;
    }


    public void ResetearValores()
    {
        Da�o = 5f;
        Defensa = 2f;
        Velociadad = 5f;
        NIvel = 1;
        ExpActual = 0f;
        ExpRequerida = 0f;
        PorcentajeBloqueo = 0f;
        PorcentajeCritico = 0f;

        Fuerza = 0;
        Inteligencia = 0;
        Destreza = 0;

        PuntosDispponibles = 0;

    }

}
