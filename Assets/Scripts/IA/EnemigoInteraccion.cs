using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TipoDeteccion
{
    Rango,
    Melee
}
public class EnemigoInteraccion : MonoBehaviour
{
    [SerializeField] private GameObject seleccionRangoFX;
    [SerializeField] private GameObject seleccionMeleeFX;

    public void MostrarEnemigosSellecionados(bool estado, TipoDeteccion tipo)
    {//si la deteccion es de tipo rango
        if (tipo== TipoDeteccion.Rango)
        {
            seleccionRangoFX.SetActive(estado);
        }
        else
        {
            seleccionMeleeFX.SetActive(estado);
        }
       
    }

    public void DesactivarSpritesSeleccion()
    {
        seleccionMeleeFX.SetActive(false);
        seleccionRangoFX.SetActive(false);
    }
}
