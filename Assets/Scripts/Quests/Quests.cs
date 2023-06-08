using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Quests : ScriptableObject
{
    public static Action<Quests> EventoQuestCompletado;

    [Header("Info")]
    public string Nombre;
    public string ID;
    public int CantidadObjetivo;

    [Header("Descripcion")]
    [TextArea] public string Descripcion;

    [Header("Recompensa")]
    public int RecompensaOro;
    public float RecompensaExp;
    public QuestRecompensaItem RecompensaItem;


    [HideInInspector] public int CantidadActual;
    [HideInInspector] public bool QuestCompletadoCheck;

    public void AñadirProgreso(int cantidad)
    {
        CantidadActual += cantidad;
        VerificarQuestCompletado();
    }

    private void VerificarQuestCompletado()
    {
        if (CantidadActual >= CantidadObjetivo)
        {
            CantidadActual = CantidadObjetivo;
            QuestCompletado();
        }
    }

    private void QuestCompletado()
    {   //si el check no esta completado retorna
        if (QuestCompletadoCheck)
        {
            return;
        }
        //cuando esta completado, se invoca el evento
        QuestCompletadoCheck = true;
        EventoQuestCompletado?.Invoke(this);
    }

    private void OnEnable()
    {
        QuestCompletadoCheck = false;
        CantidadActual = 0;
    }
}

[Serializable]
public class QuestRecompensaItem
{
    public InventarioItem Item;
    public int Cantidad;
}
