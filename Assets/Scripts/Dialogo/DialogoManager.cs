using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


//Lo indentifico como singlenton para que pueda ser llamado en otras clases
public class DialogoManager : Singleton<DialogoManager>
{
    [SerializeField] private GameObject panelDialogo;
    [SerializeField] private Image npcIcono;
    [SerializeField] private TextMeshProUGUI npcNombreTMP;
    [SerializeField] private TextMeshProUGUI npcConversacionTMP;

    public NPCInteraccion NPCDisponible { get; set; }

    private Queue<string> dialogosSecuencia;
    private bool dialogoAnimado;
    private bool despedidaMostrar;

    private void Start()
    {
        dialogosSecuencia = new Queue<string>();
    }

    private void Update()
    {
        if (NPCDisponible == null)
        {
            return;
        }
        
        if (Input.GetKeyDown(KeyCode.E))
        {
            ConfigurarPanel(NPCDisponible.Dialogo);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {//si ya se ha despedido
            if (despedidaMostrar)
            {
                //cerramos el panel de dialogo
                AbrirCerrarPanelDialogo(false);
                despedidaMostrar = false;
                return;
            }

            if (NPCDisponible.Dialogo.contieneInteraccionExtra)
            {
                UIManager.Instance.AbrirPanelInteraccion(NPCDisponible.Dialogo.InteraccionExtras);
                AbrirCerrarPanelDialogo(false);
                return;
            }

            if (dialogoAnimado)
            {
                ContinuarDialogo();
            }
        }
    }

    //si pasas true se activa el panel si es false se desactiva
    public void AbrirCerrarPanelDialogo(bool estado)
    {
        panelDialogo.SetActive(estado);
    }

    private void ConfigurarPanel(NPCDialogo npcDialogo)
    {
        AbrirCerrarPanelDialogo(true);
        CargarDialogoSecuencia(npcDialogo);

        npcIcono.sprite = npcDialogo.Icono;
        npcNombreTMP.text = npcDialogo.Nombre;
        MostrarTextoConAnimation(npcDialogo.Saludo);
    }

    private void CargarDialogoSecuencia(NPCDialogo npcDialogo)
    {
        if (npcDialogo.Conversacion == null || npcDialogo.Conversacion.Length <= 0)
        {
            return;
        }

        for (int i = 0; i < npcDialogo.Conversacion.Length; i++){
            dialogosSecuencia.Enqueue(npcDialogo.Conversacion[i].Frase);
        }

    }

    private void ContinuarDialogo()
    {
        //comprobacion de seguridad de q no hay dialogo
        if (NPCDisponible == null)
        {
            return;
        }
        //comprobacion de que se haya mostrado la despedida, para cerrar tambien
        if (despedidaMostrar)
        {
            return;
        }
        //si ya no hay mas frases que mostrar
        if (dialogosSecuencia.Count == 0)
        {
            //se muestra la despedida
            string despedida = NPCDisponible.Dialogo.Despedida;
            MostrarTextoConAnimation(despedida);
            despedidaMostrar = true;
            return;
        }

        //para hacer el cambio de dialogo
        string siguienteDialogo = dialogosSecuencia.Dequeue();
        MostrarTextoConAnimation(siguienteDialogo);
    }
    private IEnumerator AnimarTexto(string frase)
    {
        dialogoAnimado = false;
        npcConversacionTMP.text = "";
        char[] letras = frase.ToCharArray();
        for (int i = 0; i < letras.Length; i++)
        {
            npcConversacionTMP.text += letras[i];
            //para el tiempo que tarda en ponerse las letras
            yield return new WaitForSeconds(0.03f);
        }

        dialogoAnimado = true;
    }

    private void MostrarTextoConAnimation(string frase)
    {
        StartCoroutine(AnimarTexto(frase));
    }
}
