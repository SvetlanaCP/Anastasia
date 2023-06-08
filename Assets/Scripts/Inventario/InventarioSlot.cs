using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public enum TipoDeInteraccion
{
    Click,
    Usar,
    Equipar,
    Remover
}

public class InventarioSlot : MonoBehaviour
{
    public static Action<TipoDeInteraccion, int> EventosSlotInteraccion;

    [SerializeField] private Image itemIcono;
    [SerializeField] private GameObject fondoCantidad;
    [SerializeField] private TextMeshProUGUI cantidadTMP;
    public int Index { get; set; }

  
    private Button _Button;

    private void Awake()
    {   //para que cuando seleccionemos un boton se quede cliqueado
        _Button = GetComponent<Button>();
    }


    public void SeleccionarSlot()
    {
        _Button.Select();
    }
    public void ActualizarSlot(InventarioItem item, int cantidad)
    {
        itemIcono.sprite = item.Icono;
        cantidadTMP.text = cantidad.ToString();
    }

    public void ActivarSlotUI(bool estado)
    {
        itemIcono.gameObject.SetActive(estado);
        fondoCantidad.SetActive(estado);
    }

    public void ClickSlot()
    {
        EventosSlotInteraccion?.Invoke(TipoDeInteraccion.Click, Index);

        //mover Item
        if (InventarioUI.Instance.IndexSlotInicalPorMover !=-1)
        {
            if (InventarioUI.Instance.IndexSlotInicalPorMover != Index )
            {
                //mover
                Inventario.Instance.MoverItem(InventarioUI.Instance.IndexSlotInicalPorMover, Index);
            }
        }
    }

    //Este metodo verifica que tenemos un item en el slot(casilla)
    public void SlotUsarItem()
    {
        //si hay item en este index del slot 
        if (Inventario.Instance.ItemsInventario[Index] != null)
        {
            //se puede lanzar el vento Usar 
            EventosSlotInteraccion?.Invoke(TipoDeInteraccion.Usar, Index);

            //esto se envia a inventario, y se escucha en OnEnable o en OnDisable
        }
    }

    public void SlotEquiparItem()
    {
        if (Inventario.Instance.ItemsInventario[Index] != null)
        {
            EventosSlotInteraccion?.Invoke(TipoDeInteraccion.Equipar, Index);
        }
    }

    public void SlotRemoverItem()
    {
        if (Inventario.Instance.ItemsInventario[Index] != null)
        {
            EventosSlotInteraccion?.Invoke(TipoDeInteraccion.Remover, Index);
        }
    }


}
