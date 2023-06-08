using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeleccionManager : MonoBehaviour
{
    public static Action<EnemigoInteraccion> EventoEnemigoSeleccionado;
    public static Action  EventoObjetoNoSelecionado;

    public EnemigoInteraccion EnemigoSeleccionado { get; set; }

    private Camera camara;

    private void Start()
    {
        camara = Camera.main;
    }

    private void Update()
    {
        SeleccionarEnemigo();
    }

    private void SeleccionarEnemigo()
    {//verificar si estamos haciendo click al boton izquierdo del raton
        if (Input.GetMouseButtonDown(0))
        {//si en la variable hit estamos guardando la colision con el enemigo
            RaycastHit2D hit = Physics2D.Raycast(camara.ScreenToWorldPoint(Input.mousePosition), 
                Vector2.zero, Mathf.Infinity, LayerMask.GetMask("Enemigo"));
            
            if (hit.collider != null)
            {//referencia del enemigo al que le hemos echo click
                
                EnemigoSeleccionado = hit.collider.GetComponent<EnemigoInteraccion>();
                EnemigoVida enemigoVida = EnemigoSeleccionado.GetComponent<EnemigoVida>();
                EventoEnemigoSeleccionado?.Invoke(EnemigoSeleccionado);
                
                if (enemigoVida.Salud > 0f)
                {
                    EventoEnemigoSeleccionado?.Invoke(EnemigoSeleccionado);
                }
                else
                {
                    EnemigoLoot loot = EnemigoSeleccionado.GetComponent<EnemigoLoot>();
                    LootManager.Instance.MostarLoot(loot);
                }
            }
            else
            {
                EventoObjetoNoSelecionado?.Invoke();
            }
        }
    }
}
