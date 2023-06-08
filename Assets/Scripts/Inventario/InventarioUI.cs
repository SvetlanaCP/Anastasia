using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//como hay q llamar a metodos de esta clase en la clase
//inventario le hacemos un singlenton
public class InventarioUI : Singleton<InventarioUI>
{
    [Header("Panel Inventario Descripcion")]
    [SerializeField] private GameObject panelInventarioDescripcion;
    [SerializeField] private Image itemIcono;
    [SerializeField] private TextMeshProUGUI itemNombre;
    [SerializeField] private TextMeshProUGUI itemDescripcion; 


    [SerializeField] private InventarioSlot slotPrefab;
    [SerializeField] private Transform contenedor;


    public int IndexSlotInicalPorMover { get; private set; }
    public InventarioSlot SlotSeleccionado { get; private set; }
    List<InventarioSlot> slotsDisponibles = new List<InventarioSlot>();
    private void Start()
    {
        InicializarInventario();
        IndexSlotInicalPorMover = -1;
    }

    private void Update()
    {
        ActualizarSlotSeleccionado();
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (SlotSeleccionado != null)
            {
                IndexSlotInicalPorMover = SlotSeleccionado.Index;
            }

        }
    }
    private void InicializarInventario()
    {
        for (int i = 0; i < Inventario.Instance.NumeroSlots; i++)
        {
            InventarioSlot nuevoSlot = Instantiate(slotPrefab, contenedor);
            nuevoSlot.Index = i;    
            slotsDisponibles.Add(nuevoSlot);
        }
    }

    private void ActualizarSlotSeleccionado()
    {
        //regresa el objeto que esta siendo seleccionado
        GameObject goSeleccionado = EventSystem.current.currentSelectedGameObject;
        if (goSeleccionado == null)
        {
            return;
        }
        InventarioSlot slot = goSeleccionado.GetComponent<InventarioSlot>();
        if (slot != null)
        {
            SlotSeleccionado = slot;
        }
    }

    //actualizar la informacion de un item en un slot
    public void DibujarItemEnInventario(InventarioItem itemPorAñadir, int cantidad, int itemIndex)
    {
        InventarioSlot slot = slotsDisponibles[itemIndex];
        if (itemPorAñadir != null)
        {
            slot.ActivarSlotUI(estado:true);
            slot.ActualizarSlot(itemPorAñadir, cantidad);
        }
        else
        {
            slot.ActivarSlotUI(estado: false);
        }
    }

    private void ActualizarInventarioDescripcion(int index)
    {
        if (Inventario.Instance.ItemsInventario[index] != null)
        {
            itemIcono.sprite = Inventario.Instance.ItemsInventario[index].Icono;
            itemNombre.text = Inventario.Instance.ItemsInventario[index].Nombre;
            itemDescripcion.text = Inventario.Instance.ItemsInventario[index].Descripcion;
            panelInventarioDescripcion.SetActive(true);
        }
        else
        {
            panelInventarioDescripcion.SetActive(false);
        }
    }

    

    #region Evento

    private void SlotInteraccionRespuesta(TipoDeInteraccion tipo, int index)
    {
        if (tipo == TipoDeInteraccion.Click)
        {
            ActualizarInventarioDescripcion(index);
        }

    }
    private void OnEnable()
    {
        InventarioSlot.EventosSlotInteraccion += SlotInteraccionRespuesta;
    }

    private void OnDisable()
    {
        InventarioSlot.EventosSlotInteraccion -= SlotInteraccionRespuesta;
    }
    //Este metodo será usado cuando se le de al boton usar
    public void UsarItem()
    {
        if (SlotSeleccionado != null)
        {   //Llamo a este metodo que se encuentra en la clase inventarioSlot
            SlotSeleccionado.SlotUsarItem();

            //Esto es para que cuando cliqueas un slot, se quede seleccionado(en azulito)
            SlotSeleccionado.SeleccionarSlot();
        }
    }

    public void EquiparItem()
    {
        if (SlotSeleccionado != null)
        {
            SlotSeleccionado.SlotEquiparItem();
            SlotSeleccionado.SeleccionarSlot();
        }
    }

    public void RemoverItem()
    {
        if (SlotSeleccionado != null)
        {
            SlotSeleccionado.SlotRemoverItem();
            SlotSeleccionado.SeleccionarSlot();
        }
    }

    #endregion





}
