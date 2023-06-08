using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [SerializeField] private int cantidadPorCrear;

    private List<GameObject> lista;
    public GameObject listaContenedor { get; private set; }

    public void CrearPooler(GameObject objetoPorCrear)
    {
        lista = new List<GameObject>();
        listaContenedor = new GameObject(name: $"Pool - {objetoPorCrear.name}");

        for (int i = 0; i < cantidadPorCrear; i++)
        {
            lista.Add(AñadirInstancia(objetoPorCrear));
        }
    }
    private GameObject AñadirInstancia(GameObject objetoPorCrear)
    {
        GameObject nuevoObjeto = Instantiate(objetoPorCrear, listaContenedor.transform);
        nuevoObjeto.SetActive(false);
        return nuevoObjeto;
    }

    public GameObject ObtenerInstancia()
    {
        for (int i = 0; i < lista.Count; i++)
        {
            if (lista[i].activeSelf == false)
            {
                return lista[i];
            }
        }

        return null;
    }

    //para destruir los proyectiles de armas a distancia
    public void DestruirPooler()
    {
        Destroy(listaContenedor);
        lista.Clear();
    }
}
