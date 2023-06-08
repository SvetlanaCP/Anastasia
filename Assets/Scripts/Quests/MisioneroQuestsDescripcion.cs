using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MisioneroQuestsDescripcion : QuestsDescripcion
{
    [SerializeField] private TextMeshProUGUI questRecompensa;
    public override void ConfigurarQuestUI(Quests quest)
    {
        base.ConfigurarQuestUI(quest);
        questRecompensa.text = $"-{quest.RecompensaOro} oro"+
                               $"\n-{quest.RecompensaExp} exp"+
                               $"\n-{quest.RecompensaItem.Item.Nombre} x {quest.RecompensaItem.Cantidad}";
    }

    public void AceptarQuest()
    {
        if (QuestPorCompletar == null)
        {
            return;
        }
        //se añade la mision al personaje
        QuestsManager.Instance.AñadirQuest(QuestPorCompletar);
        //se elimina del quest del misionero
        gameObject.SetActive(false);
    }
}
