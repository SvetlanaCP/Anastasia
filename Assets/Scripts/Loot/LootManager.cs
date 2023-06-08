using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootManager : Singleton<LootManager>
{
    [Header("Config")]
    [SerializeField] private GameObject panelLoot;
    [SerializeField] private LootButton lootButtonPrefab;
    [SerializeField] private Transform lootContenor;//referencia de donde añadir el prefab

    public void MostarLoot(EnemigoLoot enemigoLoot)
    {
        panelLoot.SetActive(true);
        if (ContenedorOcupado())
        {//destruimos lo que tiene dentro, para ctualizarlo con el nuevo looteo
            foreach (Transform hijo in lootContenor.transform)
            {
                Destroy(hijo.gameObject);
            }
        }
        for (int i = 0; i < enemigoLoot.LootSeleccionado.Count; i++)
        {
            CargarLootPanel(enemigoLoot.LootSeleccionado[i]);
        }
    }

    public void CerrarPanel()
    {
        panelLoot.SetActive(false);
    }

    private void CargarLootPanel(DropItem dropItem)
    {//si el drop item no ha sido recogido podemos continuar
        if (dropItem.ItemRecogido)
        {
            return;
        }
        //si no lo ha sido
        LootButton loot = Instantiate(lootButtonPrefab, lootContenor);
        loot.ConfigurarLootItem(dropItem);
        loot.transform.SetParent(lootContenor);
    }

    private bool ContenedorOcupado()
    {
        LootButton[] hijos = lootContenor.GetComponentsInChildren<LootButton>();
        //Si el contenedor esta lleno de hijos,  regresamos verdadero
        if (hijos.Length >0 )
        {
            return true;
        }
        return false;
    }
}


