using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventario : Singleton<Inventario>
{
    [SerializeField] private Personaje personaje;
    public Personaje Personaje => personaje;

    [SerializeField] private int numeroSlots;
    public int NumeroSlots => numeroSlots;

    [Header("Items")]
    [SerializeField] private InventarioItem[] itemsInventario;


    //creo esta propiedad para q le lleguen los items a InventarioUI
    public InventarioItem[] ItemsInventario => itemsInventario;

    private void Start()
    {
        itemsInventario = new InventarioItem[numeroSlots];
    }

    public void AņadirItem(InventarioItem itemPorAņadir, int cantidad)
    {
        if (itemPorAņadir == null)
        {
            return;
        }

        //verificacion si ya hay un items igual en el inventario
        List<int> indexes = VerificarExistencias(itemPorAņadir.ID);
        if (itemPorAņadir.esAcomulable)
        {
            if (indexes.Count > 0)
            {
                for (int i = 0; i < indexes.Count; i++)
                {
                    //agarramos el pirmer indexes y comparamos si su cantidad no ha superado a su acomulacion
                    if (itemsInventario[indexes[i]].Cantidad < itemPorAņadir.acumulacionMax)
                    {
                        //Si es asi aņadimos la cantidad
                        itemsInventario[indexes[i]].Cantidad += cantidad;
                        //si su cantidad supera la acomulacion maxima
                        if (itemsInventario[indexes[i]].Cantidad > itemPorAņadir.acumulacionMax)
                        {
                            //optengo la diferencia
                            int diferencia = itemsInventario[indexes[i]].Cantidad - itemPorAņadir.acumulacionMax;
                            // extablezco su cantidad a la acomulacion max
                            itemsInventario[indexes[i]].Cantidad = itemPorAņadir.acumulacionMax;
                            //recursividad para que se aņada 
                            AņadirItem(itemPorAņadir, diferencia);
                        }

                        //Dibujar el item en el inventario
                        InventarioUI.Instance.DibujarItemEnInventario(itemPorAņadir,
                            itemsInventario[indexes[i]].Cantidad, itemIndex: indexes[i]);
                        return;
                    }
                }
            }
        }


        if (cantidad <=0)
        {
            return;
        }
        //si las unidades son mas de la q el slot puede tener
        if (cantidad > itemPorAņadir.acumulacionMax)
        {
            //aņadimos las maximas en un slot ej 60 aunidades pero en el
            //slot solo entran 50, entonces entran las 50
            AņadirItemEnSlotDisponible(itemPorAņadir, itemPorAņadir.acumulacionMax);
            //Aqui se actualiza y quedan 10
            cantidad -= itemPorAņadir.acumulacionMax;
            //Vuelvo a usar recursividad y se vuleve a hacer todo hasta q esté todo dentro
            //y cuando vuelve a dar la vuelta ya va directamente al else con los q sobran
            AņadirItem(itemPorAņadir, cantidad);
        }
        else //en el caso que este el slot completo y
             //siguen items por introducirce se aņade en un slot nuevo
        {
            AņadirItemEnSlotDisponible(itemPorAņadir, cantidad);
        }
    }

    private List<int> VerificarExistencias(string ItemID)
    {
        List<int> indexesDelItem = new List<int>();
        for (int i = 0; i < itemsInventario.Length; i++)
        {

            if (itemsInventario[i] != null)
            {
                if (itemsInventario[i].ID == ItemID)
                {
                    indexesDelItem.Add(i);
                }
            }
            

        }
        return indexesDelItem;
    }

    public int ObtenerCantidadItems(string itemID)
    {
        List<int> indexes = VerificarExistencias(itemID);
        int cantidadTotal = 0;
        foreach (int index in indexes)
        {
            if (itemsInventario[index].ID == itemID)
            {
                cantidadTotal += itemsInventario[index].Cantidad;
            }
        }
        return cantidadTotal;
    }

    public void ConsumirItem(string itemID)
    {
        List<int> indexes = VerificarExistencias(itemID);
        if (indexes.Count > 0)
        {
            EliminarItem(indexes[indexes.Count - 1]);
        }
    }

    //Metodo para aņadir item en un slot vacio
    private void AņadirItemEnSlotDisponible(InventarioItem item, int cantidad)
    {
        //recorremos el inventario
        for (int i = 0; i < itemsInventario.Length; i++)
        {
            //si tenemos un slot vacio hay q aņadir el inventario que coja
            if (itemsInventario[i] == null)
            {
                itemsInventario[i] = item.CopiarItem();
                itemsInventario[i].Cantidad = cantidad;
                InventarioUI.Instance.DibujarItemEnInventario(item, cantidad, itemIndex: i);
                return;
            }
        }
    }

    private void EliminarItem(int index)
    {
        // cuando es usado elimina 1
        ItemsInventario[index].Cantidad--;
        //si quedan 0 item en el slot lo elimina
        if (itemsInventario[index].Cantidad <=0)
        {
            itemsInventario[index].Cantidad = 0;
            itemsInventario[index] = null;
            InventarioUI.Instance.DibujarItemEnInventario(itemPorAņadir:null, cantidad:0, index);
        }
        //en el caso q siga habiendo se actualiza restando el usado
        else
        {
            InventarioUI.Instance.DibujarItemEnInventario(itemsInventario[index],
                itemsInventario[index].Cantidad, index);
        }
    }

    public void MoverItem(int indexInicial, int indexFinal)
    {
        //si no hay items por mover en el valor inial regresamos || si el slot final esta ocupado regresamos
        if (itemsInventario[indexInicial] == null || itemsInventario[indexFinal] != null)
        {
            return;
        }

        //copiar el item en el slot final
        InventarioItem itemPorMover = itemsInventario[indexInicial].CopiarItem();
        itemsInventario[indexFinal] = itemPorMover;
        //actualizar
        InventarioUI.Instance.DibujarItemEnInventario(itemPorMover, itemPorMover.Cantidad, indexFinal);

        //Borra Item de slot inicial
        itemsInventario[indexInicial] = null;
        //actualizar
        InventarioUI.Instance.DibujarItemEnInventario(null, 0, indexInicial);
    }
    private void UsarItem(int index)
    {
        if (itemsInventario[index] == null)
        {
            return;
        }
        //se verifica que se pueda usar el item
        if (itemsInventario[index].UsarItem())
        {//si así es elimina 1
            EliminarItem(index);
        }
    }

    private void EquiparItem(int index)
    {
        if (itemsInventario[index] == null)
        {
            return;
        }
        //si no es un arma retornamos
        if (itemsInventario[index].Tipo != TiposDeItem.Armas)
        {
            return;
        }

        itemsInventario[index].EquiparItem();
    }

    private void RemoverItem(int index)
    {
        if (itemsInventario[index] == null)
        {
            return;
        }
        //si no es un arma retornamos
        if (itemsInventario[index].Tipo != TiposDeItem.Armas)
        {
            return;
        }

        itemsInventario[index].RemoverItem();
    }

    #region Eventos

    private void SlotIteraccionRespuesta(TipoDeInteraccion tipo, int index)
    {
        switch (tipo)
        {
            case TipoDeInteraccion.Usar:
                UsarItem(index);
                break;
            case TipoDeInteraccion.Equipar:
                EquiparItem(index);
                break;
            case TipoDeInteraccion.Remover:
                RemoverItem(index);
                break;
            
        }
    }
    private void OnEnable()
    {
        InventarioSlot.EventosSlotInteraccion += SlotIteraccionRespuesta;
    }

    private void OnDisable()
    {
        InventarioSlot.EventosSlotInteraccion -= SlotIteraccionRespuesta;
    }
    #endregion
}

