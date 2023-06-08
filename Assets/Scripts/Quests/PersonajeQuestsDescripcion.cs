using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PersonajeQuestsDescripcion : QuestsDescripcion
{
    [SerializeField] private TextMeshProUGUI tareaObjetivo;
    [SerializeField] private TextMeshProUGUI recompensaOro;
    [SerializeField] private TextMeshProUGUI recompensaExp;

    [Header("Item")]
    [SerializeField] private Image recompensaItemIcono;
    [SerializeField] private TextMeshProUGUI recompensaCantidad;

    private void Update()
    {//si el quest esta completado
        if (QuestPorCompletar.QuestCompletadoCheck)
        {//se regresa 
            return;
        }
        //se actualiza mientras no esta completado y esta en progreso
        //se referencia para que se actualice 
        tareaObjetivo.text = $"{QuestPorCompletar.CantidadActual}/{QuestPorCompletar.CantidadObjetivo}";
    }
    public override void ConfigurarQuestUI(Quests questPorCArgar)
    {
        base.ConfigurarQuestUI(questPorCArgar);
        recompensaOro.text = questPorCArgar.RecompensaOro.ToString();
        recompensaExp.text = questPorCArgar.RecompensaExp.ToString();
        tareaObjetivo.text = $"{questPorCArgar.CantidadActual}/{questPorCArgar.CantidadObjetivo}";

        recompensaItemIcono.sprite = questPorCArgar.RecompensaItem.Item.Icono;
        recompensaCantidad.text = questPorCArgar.RecompensaItem.Cantidad.ToString();
    }

    private void QuestCompletadoRespuesta(Quests questCompletado)
    {
        if (questCompletado.ID == QuestPorCompletar.ID)
        {//una vez que la quests es completada
            tareaObjetivo.text = $"{QuestPorCompletar.CantidadActual}/{QuestPorCompletar.CantidadObjetivo}";
            //lo borro del panel de quest
            gameObject.SetActive(false);
        }
    }
    private void OnEnable()
    {
        if (QuestPorCompletar.QuestCompletadoCheck)
        {
            gameObject.SetActive(false);
        }
        Quests.EventoQuestCompletado += QuestCompletadoRespuesta;
    }

    private void OnDiseable()
    {
        Quests.EventoQuestCompletado -= QuestCompletadoRespuesta;
    }
}
