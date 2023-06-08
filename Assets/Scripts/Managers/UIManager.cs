using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [Header("Stats")]
    [SerializeField] private PersonajeStats stats;

    [Header("Paneles")]
    [SerializeField] private GameObject panelStats;
    [SerializeField] private GameObject panelTienda;
    [SerializeField] private GameObject panelCrafting;
    [SerializeField] private GameObject panelCraftingInfo;
    [SerializeField] private GameObject panelInventario;
    [SerializeField] private GameObject panelInspectorQuests;
    [SerializeField] private GameObject panelPersonajeQuests;

    [Header("Barra")]
    [SerializeField] private Image vidaPlayer;
    [SerializeField] private Image manaPlayer;
    [SerializeField] private Image expPlayer;

    [Header("Texto")]
    [SerializeField] private TextMeshProUGUI vidaTMP;
    [SerializeField] private TextMeshProUGUI manaTMP;
    [SerializeField] private TextMeshProUGUI expTMP;
    [SerializeField] private TextMeshProUGUI nivelTMP;
    [SerializeField] private TextMeshProUGUI monedasTMP;

    [Header("Statas")]
    [SerializeField] private TextMeshProUGUI StatDañoTMP;
    [SerializeField] private TextMeshProUGUI StatDefensaTMP;
    [SerializeField] private TextMeshProUGUI StatCriticoTMP;
    [SerializeField] private TextMeshProUGUI StatBloqueoTMP;
    [SerializeField] private TextMeshProUGUI StatVelocidadTMP;
    [SerializeField] private TextMeshProUGUI StatNivelTMP;
    [SerializeField] private TextMeshProUGUI StatExpTMP;
    [SerializeField] private TextMeshProUGUI StatExpRequeridaTMP;

    [SerializeField] private TextMeshProUGUI AtributoFuerzaTMP;
    [SerializeField] private TextMeshProUGUI AtributoInteligenciaTMP;
    [SerializeField] private TextMeshProUGUI AtributoDestrezaTMP;
    [SerializeField] private TextMeshProUGUI AtributoDispobiblesTMP;


    private float vidaActual;
    private float vidaMax;
    private float manaActual;
    private float manaMax;
    private float expActual;
    private float expRequeridaNuevoNivel;


    void Update(){
        ActualizarUIPersonaje();
        ActualizarPanelStas();
        
    }

    private void ActualizarUIPersonaje(){
        //fiñlAmount es la cantidad de relleno de la barra de vida
        vidaPlayer.fillAmount = Mathf.Lerp(a:vidaPlayer.fillAmount, b:vidaActual / vidaMax, 10f * Time.deltaTime);
        manaPlayer.fillAmount = Mathf.Lerp(a: manaPlayer.fillAmount, b:manaActual / manaMax, 10f * Time.deltaTime);
        expPlayer.fillAmount = Mathf.Lerp(expPlayer.fillAmount,expActual / expRequeridaNuevoNivel, 10f * Time.deltaTime);

        vidaTMP.text = $"{vidaActual}/{vidaMax}";
        manaTMP.text =  $"{manaActual}/{manaMax}";
        expTMP.text = $"{((expActual / expRequeridaNuevoNivel) * 100):F2}%";
        nivelTMP.text = $"Nivel {stats.NIvel}";
        monedasTMP.text = MonedasManagers.Instance.MonedasTotales.ToString();
    }


    //Actualizar los datos del stats
    private void ActualizarPanelStas()
    {
        if(panelStats.activeSelf == false)
        {
            return;
        }

        StatDañoTMP.text = stats.Daño.ToString();
        StatDefensaTMP.text = stats.Defensa.ToString();
        StatCriticoTMP.text = $"{stats.PorcentajeCritico}%";
        StatBloqueoTMP.text = $"{stats.PorcentajeBloqueo}%";
        StatVelocidadTMP.text = stats.Velociadad.ToString();
        StatNivelTMP.text = stats.NIvel.ToString();
        StatExpTMP.text = stats.ExpActual.ToString();
        StatExpRequeridaTMP.text = stats.ExpRequerida.ToString();

        AtributoFuerzaTMP.text = stats.Fuerza.ToString();
        AtributoInteligenciaTMP.text = stats.Inteligencia.ToString();
        AtributoDestrezaTMP.text = stats.Destreza.ToString();
        AtributoDispobiblesTMP.text = $"Puntos: {stats.PuntosDispponibles}";
    }


    //Para poder tener una referencia de PersonajeVida usar sus variables (VidaActual VidaMax)
    public void ActualizarVidaPersonaje(float pVidaActual, float pVidaMax){
        vidaActual = pVidaActual;
        vidaMax = pVidaMax;

    }

    public void ActualizarManaPersonaje(float pManaActual, float pManaMax)
    {
        manaActual = pManaActual;
        manaMax = pManaMax;

    }

    public void ActualizarExpPersonaje(float pExpActual, float pExpRequerida)
    {
        expActual = pExpActual;
        expRequeridaNuevoNivel = pExpRequerida;
    }

    #region Paneles

    public void AbrirCerrarPanelStats()
    {
        //activeSelf nos dice si está activo el panel o no
        //Con la exclamacion niego que lo que saque el activeSelf para q abra y cierre
        //es decir si ve q esta abierto con la exclamacion lo pone en falso y se cierra
        panelStats.SetActive(!panelStats.activeSelf);
    }

    public void AbrirCerrarPanelInventario()
    {
        panelInventario.SetActive(!panelInventario.activeSelf);
    }

    public void AbrirCerrarPanelPersonajeQuests()
    {
        panelPersonajeQuests.SetActive(!panelPersonajeQuests.activeSelf);
    }

    public void AbrirCerrarPanelInspectorQuests()
    {
        panelInspectorQuests.SetActive(!panelInspectorQuests.activeSelf);
    }

    public void AbrirCerrarPanelTienda()
    {
        panelTienda.SetActive(!panelTienda.activeSelf);
    }

    public void AbrirPanelCrafting()
    {
        panelCrafting.SetActive(true);
    }
    public void CerraPanelCrafting()
    {
        panelCrafting.SetActive(false);
        AbrirCerrarPanelCraftingInfo(false);
    }

    public void AbrirCerrarPanelCraftingInfo(bool estado)
    {
        panelCraftingInfo.SetActive(estado);
    }

    public void AbrirPanelInteraccion(InteraccionExtrasNPC tipoInteracion)
    {
        switch (tipoInteracion)
        {
            case InteraccionExtrasNPC.Quest:
                AbrirCerrarPanelInspectorQuests();
                break;
            case InteraccionExtrasNPC.Tienda:
                AbrirCerrarPanelTienda();
                break;
            case InteraccionExtrasNPC.Crafting:
                AbrirPanelCrafting();
                break;
            
                
        }
    }

    



    #endregion

}
