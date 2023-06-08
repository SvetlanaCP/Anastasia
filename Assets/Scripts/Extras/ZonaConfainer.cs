using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZonaConfainer : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera camara;

    // Para detectar que el personaje esta entrando
    // y saliendo del confainer vamos a usar los siguientes metodos
    // OnTriggerEnter2D y OnTriggerExit2D

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            camara.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            camara.gameObject.SetActive(false);
        }
    }
}
