using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum TiposDeItem
{
    Armas,
    Pociones,
    Pergaminos,
    Ingredientes,
    Resoros
}
public class InventarioItem : ScriptableObject
{
    [Header("Parametros")]
    public string ID;
    public string Nombre;
    public Sprite Icono;
    [TextArea] public string Descripcion;

    [Header("Informacion")]
    public TiposDeItem Tipo;
    public bool esConsumible;
    public bool esAcomulable;
    public int acumulacionMax;

    [HideInInspector]public int Cantidad;

    //hago esto para q se cree un scriptableObject por cada slot
    public InventarioItem CopiarItem()
    {
        InventarioItem nuevaInstancia = Instantiate(original:this);
        return nuevaInstancia;
    }

    //son metodos virtuales para poder sobreescribirlos
    public virtual bool UsarItem()
    {
        return true;
    }

    public virtual bool EquiparItem()
    {
        return true;
    }

    public virtual bool RemoverItem()
    {
        return true;
    }

    public virtual string DescripcionItemCrafting()
    {
        return "";
    }
}
