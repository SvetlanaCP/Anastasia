using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InteraccionExtrasNPC
{
    Quest,
    Tienda,
    Crafting
}

//añadir este atributo para poder añadir el dialogo en las carpetas
[CreateAssetMenu]
public class NPCDialogo : ScriptableObject
{
    [Header("Info")]
    public string Nombre;
    public Sprite Icono;
    public bool contieneInteraccionExtra;
    public InteraccionExtrasNPC InteraccionExtras;

    [Header("Saludo")]
    [TextArea] public string Saludo;

    [Header("Conversacion")]
    public DialogoTexto[] Conversacion;

    [Header("Despedida")]
    [TextArea] public string Despedida;
}

[Serializable]
public class DialogoTexto
{
    [TextArea] public string Frase;
}
