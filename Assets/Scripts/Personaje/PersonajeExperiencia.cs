using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonajeExperiencia : MonoBehaviour
{

    [Header("Stats")]
    [SerializeField] private PersonajeStats stats;

    [Header("Config")]
    [SerializeField] private int nivelMax;
    [SerializeField] private int expBase;
    [SerializeField] private int valorIncremental;

    private float expActual;
    private float expActualTemp;
    private float expRequeridaSiguienteNIvel;


    void Start()
    {
        
        stats.NIvel = 1;
        expRequeridaSiguienteNIvel = expBase;
        stats.ExpRequerida = expRequeridaSiguienteNIvel;
        ActualizarBarraExp();
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.X))
        {
            AñadirExperiencia(2f);
        }
    }
    //Al matar a un enemigo se optiene experiencia
    public void AñadirExperiencia(float expOptenida)
    {
        if(expOptenida > 0f) 
        {
            float expRestanteNuevoNivel = expRequeridaSiguienteNIvel - expActualTemp; 
            if(expOptenida >= expRestanteNuevoNivel)
            {
                expOptenida -= expRestanteNuevoNivel;
                expActual += expOptenida;
                ActualizarNivel();
                //Uso recursion al llamar un metodo dentro de su metodo
                //para que la expOptenida entre ya que hay que volver a usar
                //el metodo despues de llamar a ActualizarNivel()
                AñadirExperiencia(expOptenida);

            }
            else
            {
                expActual += expOptenida;
                expActualTemp += expOptenida;
                if (expActualTemp == expRequeridaSiguienteNIvel)
                {
                    ActualizarNivel();
                }
            }
        }
        stats.ExpActual = expActual;
        ActualizarBarraExp();
    }

    private void ActualizarNivel()
    {
        if (stats.NIvel < nivelMax)
        {
            stats.NIvel++;
            expActualTemp = 0f;
            expRequeridaSiguienteNIvel *= valorIncremental;
            stats.ExpRequerida = expRequeridaSiguienteNIvel;
            stats.PuntosDispponibles += 3;
        }
    }
    private void ActualizarBarraExp()
    {
        UIManager.Instance.ActualizarExpPersonaje(expActualTemp, expRequeridaSiguienteNIvel);
    }

    private void RespuestaEnemigoDerrotado(float exp)
    {
        AñadirExperiencia(exp);
    }

    private void OnEnable()
    {
        EnemigoVida.EventoEnemigoDerrotado += RespuestaEnemigoDerrotado;
    }

    private void OnDisable()
    {
        EnemigoVida.EventoEnemigoDerrotado -= RespuestaEnemigoDerrotado;
    }
}
