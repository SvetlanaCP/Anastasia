using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PersonajeVida : VidaBase
{

    public static Action EventoPersonajeDerrotado;
    
    public bool Derrotado { get; private set; }
    public bool PuedeSerCurado => Salud < saludMax;

    //Esta variable es para que el personaje una vez muerto no tenga problema de colisiones con los enemigos
    private BoxCollider2D _boxCollider2D;

  

    private void Awake(){
        _boxCollider2D = GetComponent<BoxCollider2D>();
    }

    protected override void Start(){
        base.Start();
        ActualizarBarraVida(vidaActual:Salud, saludMax);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            RecibirDaño(10);
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            RestaurarSalud(10);
        }
    }

    public void RestaurarSalud(float cantidad){

        if (Derrotado)
        {
            return;
        }

        if (PuedeSerCurado){
            Salud += cantidad;
            if (Salud > saludMax){
                Salud = saludMax;
            }

            ActualizarBarraVida(Salud, saludMax);
           
        }
    }

    protected override void PersonajeDerrotado()
    {
        _boxCollider2D.enabled = false;
        Derrotado = true;
        EventoPersonajeDerrotado?.Invoke();
    }

    public void RestaurarPersonaje(){
        _boxCollider2D.enabled = true;
        Derrotado = false;
        Salud = saludInicial;
        ActualizarBarraVida(vidaActual:Salud, vidaMax:saludInicial);
    }


    protected override void ActualizarBarraVida(float vidaActual, float vidaMax){
        UIManager.Instance.ActualizarVidaPersonaje(vidaActual, vidaMax);
    }

    
}
