using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Receta 
{
    public string Nombre;

    [Header("1Material")]
    public InventarioItem Item1;
    public int Item1CantidadRequerida;
    
    [Header("2Material")]
    public InventarioItem Item2;
    public int Item2CantidadRequerida;

    [Header("Resultado")]
    public InventarioItem ItemResultado;
    public int ItemResultadoCantidad;


}
