using UnityEngine;
using System;


//Referencia a la hora de apretar boton
public enum TipoAtributo
{
    Fuerza,
    Inteligencia,
    Destreza
}


//Cuando aprientas uno de los botones anteriores salta el evento
// A personaje q es el que escucha el evento
public class AtributosButton : MonoBehaviour
{
    public static Action<TipoAtributo> EventoAgregarAtributo;
    [SerializeField] private TipoAtributo tipo;

    public void AgregarAtributo()
    {
        EventoAgregarAtributo?.Invoke(tipo);
    }
}
