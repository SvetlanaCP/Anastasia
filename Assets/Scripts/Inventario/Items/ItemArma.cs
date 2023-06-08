using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Arma")]
public class ItemArma : InventarioItem
{
    [Header("Arma")]
    public Arma Arma;

    public override bool EquiparItem()
    {//solo podemos equipar un arma si el contenedor no tiene ya un arma equipada
        if (ContenedorArma.Instance.ArmaEquipada != null)
        {
            return false;
        }

        ContenedorArma.Instance.EquiparArma(this);
        return true;
    }

    public override bool RemoverItem()
    {//si no hay un arma equipada no se puede remover 
        if (ContenedorArma.Instance.ArmaEquipada == null)
        {
            return false;
        }

        ContenedorArma.Instance.RemoverArma();
        return true;
    }

    public override string DescripcionItemCrafting()
    {
        string descripcion = $"- Critico: {Arma.ChanceCritico}%\n" + $"Bloqueo: {Arma.ChanceBloqueo}%";
        return descripcion;
    }
}
