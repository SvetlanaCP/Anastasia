using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestsDescripcion : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI questNombre; 
    [SerializeField] private TextMeshProUGUI questDescripcion;

    public Quests QuestPorCompletar { get; set; }
    public virtual void ConfigurarQuestUI(Quests quest)
    {
        QuestPorCompletar = quest;
        questNombre.text = quest.Nombre;
        questDescripcion.text = quest.Descripcion;
    }
}
