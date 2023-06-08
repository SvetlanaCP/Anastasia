using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsPorAgregar : MonoBehaviour
{
    [Header("Cinfig")]
    [SerializeField] private InventarioItem inventarioItemReferencia;
    [SerializeField] private int cantidadPorAgregar;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Inventario.Instance.AñadirItem(inventarioItemReferencia, cantidadPorAgregar);
            Destroy(gameObject);
        }
    }
}
