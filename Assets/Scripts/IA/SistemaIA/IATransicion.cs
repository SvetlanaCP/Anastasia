using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class IATransicion 
{
    public IADesicion desicion;
    public IAEstado EstadoVerdadero;
    public IAEstado EstadoFalso;
}
