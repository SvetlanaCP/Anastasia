using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestsManager : Singleton<QuestsManager>
{
    [Header("Personaje")]
    [SerializeField] private Personaje personaje;

    [Header("Quests")]
    [SerializeField] private Quests[] questsDisponible;

    [Header("Misionero Quests")]
    [SerializeField] private MisioneroQuestsDescripcion misioneroQuestPrefab;
    [SerializeField] private Transform misioneroQuestContenedor;


    [Header("Personaje Quests")]
    [SerializeField] private PersonajeQuestsDescripcion personajeQuestPrefab;
    [SerializeField] private Transform personajeQuestContenedor;

    [Header("Panel Quests Completado")]
    [SerializeField] private GameObject panelQuestCompletado;
    [SerializeField] private TextMeshProUGUI questNombre;
    [SerializeField] private TextMeshProUGUI questRecompensaOro;
    [SerializeField] private TextMeshProUGUI questRecompensaExp;
    [SerializeField] private TextMeshProUGUI questRecompensaItemCantidad;
    [SerializeField] private Image questRecompensaItemIcono;

    public Quests QuestPorReclamar { get; private set; }

    private void Start()
    {
        CargarQuestEnMisionero();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            AñadirProgreso("Mata10", 1);
            AñadirProgreso("Mata25", 1);
            AñadirProgreso("Mata50", 1);
        }
    }

    private void CargarQuestEnMisionero()
    {
        for (int i = 0; i < questsDisponible.Length; i++)
        {
           
            MisioneroQuestsDescripcion nuevoQuest = Instantiate(misioneroQuestPrefab, misioneroQuestContenedor);
            nuevoQuest.ConfigurarQuestUI(questsDisponible[i]);

        }
    }

    private void AñadirQuestPorCompletar(Quests questPorCompletar)
    {
        //Instanciar el prefab, guardarlo en una referencia de su mismo tipo
        //para poder llamar al metodo de configurar ui
        PersonajeQuestsDescripcion nuevoQuest = Instantiate(personajeQuestPrefab, personajeQuestContenedor);
        nuevoQuest.ConfigurarQuestUI(questPorCompletar);
    }

    public void AñadirQuest(Quests questPorCompletar)
    {
        AñadirQuestPorCompletar(questPorCompletar);
    }

    public void ReclamarRecompensa()
    {
        if (QuestPorReclamar == null)
        {
            return;
        }
        //con esto reclamamos las recompesas, es decir la añadimos al personaje
        MonedasManagers.Instance.AñadirMonedas(QuestPorReclamar.RecompensaOro);
        personaje.PersonajeExperiencia.AñadirExperiencia(QuestPorReclamar.RecompensaExp);
        Inventario.Instance.AñadirItem(QuestPorReclamar.RecompensaItem.Item, QuestPorReclamar.RecompensaItem.Cantidad);
        //una vez reclames, que se cierre el panel
        panelQuestCompletado.SetActive(false);
        //borramos la quest
        QuestPorReclamar = null;
    }
    public void AñadirProgreso(string questID,int cantidad)
    {
        //guardamos en una variable lo que sale de la funcion QuestExiste
        Quests questPorActualizar = QuestExiste(questID);
        //y actualizamos la cantidad
        questPorActualizar.AñadirProgreso(cantidad);
    }

    private Quests QuestExiste(string questID)
    {//bucle que da vuelta al array de quest
        for (int i = 0; i < questsDisponible.Length; i++)
        {//si el quest coincide con el que entra por parametro
            if (questsDisponible[i].ID == questID)
            {//se retorna
                return questsDisponible[i];
            }
        }
        //si no existe lo devulve en nulo
        return null;
    }

    private void MostrarQuestCompletado(Quests questCompletado)
    {
        panelQuestCompletado.SetActive(true);
        questNombre.text = questCompletado.Nombre;
        questRecompensaOro.text = questCompletado.RecompensaOro.ToString();
        questRecompensaExp.text = questCompletado.RecompensaExp.ToString();
        questRecompensaItemCantidad.text = questCompletado.RecompensaItem.Cantidad.ToString();
        questRecompensaItemIcono.sprite = questCompletado.RecompensaItem.Item.Icono;

    }
    private void QuestCompletadoRespuesta(Quests questCompletado)
    {
        QuestPorReclamar = QuestExiste(questCompletado.ID);
        if (QuestPorReclamar != null)
        {
            MostrarQuestCompletado(QuestPorReclamar);
        }
    }

    //OnEnable y OnDisable para escuchar el evento
    private void OnEnable()
    {
        Quests.EventoQuestCompletado += QuestCompletadoRespuesta;
    }

    private void OnDisable()
    {
        Quests.EventoQuestCompletado -= QuestCompletadoRespuesta;
    }
}
