using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonajeMana : MonoBehaviour
{

    [SerializeField] private float manaInicial;
    [SerializeField] private float manaMax;
    [SerializeField] private float regeneracionPorSegundo;

    public float ManaActual { get; private set; }
    public bool SePuedeRestaurar => ManaActual < manaMax;

    //Obtener referencia de la clase personajeVida 
    //Para ver que el personaje esta vivo, para regenerar la mana o no
    private PersonajeVida _personajeVida;

    private void Awake()
    {//Obtener referencia de la clase personajeVida 
     //Para ver que el personaje esta vivo, para regenerar la mana o no
        _personajeVida = GetComponent<PersonajeVida>();
    }

    void Start()
    {
        ManaActual = manaInicial;
        ActualizarBarraMana();

        //Llamar un metodo para repetirlo todas las veces que tu quieras por segundo
        InvokeRepeating(nameof(RegenererMana),1,1);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
            {
            UsarMana(10f);
        }
    }
    public void UsarMana(float cantidad)
    {
        //Verificar cuanta mana tengo para ver si puedo gastar
        if(ManaActual >= cantidad)
        {
            ManaActual -= cantidad;
            ActualizarBarraMana();
        }
    }

    public void RestaurarMana(float cantidad)
    {   //si la mana actual es superior o igual a la max
        if (ManaActual >= manaMax) 
        {//retornamos vacio ya que no puede restaurar mas mana porq esta atope
            return;
        }
        //le sumamos la pociom
        ManaActual += cantidad;
        if (ManaActual > manaMax)
        {
            ManaActual = manaMax;
        }
        //Actualiza los valores en el uiManager del personaje
        UIManager.Instance.ActualizarManaPersonaje(ManaActual, manaMax);
    }

    private void RegenererMana()
    {   //Comprobar si tenemos vida y si la mana no está al tope
        if (_personajeVida.Salud > 0f && ManaActual < manaMax)
        {
            ManaActual += regeneracionPorSegundo;
            ActualizarBarraMana();
        }
    }

    public void RestablecerMana()
    {
        ManaActual = manaInicial;
        ActualizarBarraMana();
    }

    private void ActualizarBarraMana()
    {
        UIManager.Instance.ActualizarManaPersonaje(ManaActual, manaMax);
    }
}
