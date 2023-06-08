using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CraftingManager : Singleton<CraftingManager>
{
    [Header("Config")]
    [SerializeField] private RecetaTarjeta recetaTarjetaPrefab;
    [SerializeField] private Transform recetaContenedor;

    [Header("Receta Info")]
    [SerializeField] private Image primerMateriaIcono;
    [SerializeField] private TextMeshProUGUI primerMaterialNombre;
    [SerializeField] private TextMeshProUGUI primerMaterialCantidad;
    [SerializeField] private Image segundoMateriaIcono;
    [SerializeField] private TextMeshProUGUI segundoMaterialNombre;
    [SerializeField] private TextMeshProUGUI segundoMaterialCantidad;
    [SerializeField] private TextMeshProUGUI recetaMesaje;
    [SerializeField] private Button buttonCraftear;

    [Header("Item Resultado")]
    [SerializeField] private Image itemResultadoIcono;
    [SerializeField] private TextMeshProUGUI itemResultadoNombre;
    [SerializeField] private TextMeshProUGUI itemResultadoDescripcion;


    [Header("Recetas")]
    [SerializeField] private RecetaLista recetas;


    public Receta RecetaSeleccionada { get; set; }

    private void Start()
    {
        CargarRecetas();
    }
    private void CargarRecetas()
    {
        for (int i = 0; i < recetas.Recetas.Length; i++)
        {
            RecetaTarjeta receta=  Instantiate(recetaTarjetaPrefab, recetaContenedor);
            receta.ConfigurarRecetaTarjeta(recetas.Recetas[i]);
        }
    }

    public void MostrarReceta(Receta receta)
    {
        RecetaSeleccionada = receta;
        primerMateriaIcono.sprite = receta.Item1.Icono;
        primerMaterialNombre.text = receta.Item1.Nombre;
        primerMaterialCantidad.text =
            $"{Inventario.Instance.ObtenerCantidadItems(receta.Item1.ID)}/{receta.Item1CantidadRequerida}";
        segundoMateriaIcono.sprite = receta.Item2.Icono;
        segundoMaterialNombre.text = receta.Item2.Nombre;
        segundoMaterialCantidad.text =
            $"{Inventario.Instance.ObtenerCantidadItems(receta.Item2.ID)}/{receta.Item2CantidadRequerida}";

        if (SePuedeCraftear(receta))
        {
            recetaMesaje.text = "Puedes craftear";
            buttonCraftear.interactable = true;
        }
        else
        {
            recetaMesaje.text = "Necesitas mas materiales";
            buttonCraftear.interactable = false;
        }

        itemResultadoIcono.sprite = receta.ItemResultado.Icono;
        itemResultadoNombre.text = receta.ItemResultado.Nombre;
        itemResultadoDescripcion.text = receta.ItemResultado.DescripcionItemCrafting();
    }

    public bool SePuedeCraftear(Receta receta)
    {
        if (Inventario.Instance.ObtenerCantidadItems(receta.Item1.ID) >= receta.Item1CantidadRequerida
            && Inventario.Instance.ObtenerCantidadItems(receta.Item2.ID) >= receta.Item2CantidadRequerida)
        {
            return true;
        }
        return false;
    }

    public void Craftear()
    {
        for (int i = 0; i < RecetaSeleccionada.Item1CantidadRequerida; i++)
        {
            Inventario.Instance.ConsumirItem(RecetaSeleccionada.Item1.ID);
        }

        for (int i = 0; i < RecetaSeleccionada.Item2CantidadRequerida; i++)
        {
            Inventario.Instance.ConsumirItem(RecetaSeleccionada.Item2.ID);
        }

        Inventario.Instance.A�adirItem(RecetaSeleccionada.ItemResultado, RecetaSeleccionada.ItemResultadoCantidad);
        MostrarReceta(RecetaSeleccionada);
    }

}