using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;



public enum TiposDeAtaque
{
    Melee,
    Embestida
}
public class IAController : MonoBehaviour
{
    public static Action<float> EventoDañoRealizado;

    [Header("Stats")]
    [SerializeField] private PersonajeStats stats;

    [Header("Estados")]
    [SerializeField] private IAEstado estadoInicial;
    [SerializeField] private IAEstado estadoDefault;

    [Header("Config")]
    [SerializeField] private float rangoDeteccion;
    [SerializeField] private float rangoDeAtaque;
    [SerializeField] private float rangoDeEmbestida;
    [SerializeField] private float velocidadMovimiento;
    [SerializeField] private float velocidadDeEmbestida;
    [SerializeField] private LayerMask personajeLayerMask;

    [Header("Ataque")]
    [SerializeField] private float daño;
    [SerializeField] private float tiempoEntreAtaques;
    [SerializeField] private TiposDeAtaque tipoAtaque;   

    [Header("Debug")]
    [SerializeField] private bool mostrarDeteccion;
    [SerializeField] private bool mostrarRangoDeAtaque;
    [SerializeField] private bool mostrarRangoDeEmbestida;
    

    private float tiempoParaSiguienteAtaque;
    private BoxCollider2D _boxCollider2D;

    public Transform PersonajeReferencia { get; set; }
    public IAEstado EstadoActual { get; set; }
    public EnemigoMovimiento EnemigoMovimiento { get; set; }
    public float RangoDeteccion => rangoDeteccion;
    public float VelocidadMovimiento => velocidadMovimiento;
    public LayerMask PersonajeLayerMask => personajeLayerMask;
    public float Daño => daño;
    public TiposDeAtaque TipoAtaque => tipoAtaque;
    public float RangoDeAtaquePredeterminado => tipoAtaque == TiposDeAtaque.Embestida ? rangoDeEmbestida : rangoDeAtaque;
    
    //Aqui se inicia el estado con el que queremos iniciar
    private void Start()
    {
        _boxCollider2D = GetComponent<BoxCollider2D>();
        EstadoActual = estadoInicial;
        EnemigoMovimiento = GetComponent<EnemigoMovimiento>();
        
    }
    
    //Aqui lo ejecutamos 
    private void Update()
    {
        EstadoActual.EjecutarEstado(controller: this);
    }

    public void CambiarEstado(IAEstado nuevoEstado)
    {
        //si el estado nuevo es diferente al estado que tiene en ese momento
        if (nuevoEstado != estadoDefault)
        {//se da el paso a cambiar de estado
            EstadoActual = nuevoEstado;
        }
    }

    public void AtaqueMelee(float cantidad)
    {
        if (PersonajeReferencia != null)
        {
            AplicarDañoPersonaje(cantidad);
        }
    }

    public void AtaqueEmbestida(float cantidad)
    {
        StartCoroutine(IEEmbestida(cantidad));
    }

    //para hacer esta funcion se hara una formula matematica  la de paravola
    //para que el enemigo se lance golpee y vuelva
    private IEnumerator IEEmbestida (float cantidad)
    {
        Vector3 personajePosicion = PersonajeReferencia.position; //Referencia Personaje
        Vector3 posicionInicial = transform.position;  //ReferenciaEnemigo
        Vector3 direccionHaciaPersonaje =(personajePosicion - posicionInicial).normalized; //HAcia donde es la envestidaç
        Vector3 posicionDeAtaque = personajePosicion - direccionHaciaPersonaje * 0.5f;  //para que l enemigo no se pegue del todo, igual que n seguiPersonaje
        _boxCollider2D.enabled = false; //cancelo el boxCollaider del enemigo cuando haga la envestida, para no generar problemas de colision

        float transicionDeAtaque = 0f;

        //mientras la transicion de ata
        while (transicionDeAtaque <= 1f)
        {
            transicionDeAtaque += Time.deltaTime * velocidadMovimiento;
            //Aqui uso la formula matematica de la paravola  //Transicion de ataque al cuadrado
            float interpolacion = (-MathF.Pow(transicionDeAtaque, 2) + transicionDeAtaque) *  4f;
            //para mover al enemigo (su transform)
            transform.position = Vector3.Lerp(posicionInicial, posicionDeAtaque, interpolacion);
            yield return null;     
        }
        //una vez que se hace el movimiento del enemigo, se hace una comprobacion de
        //seguridad para asegurarnos de q el personaje esta, se le aplica daño
        if (PersonajeReferencia != null)
        {
            AplicarDañoPersonaje(cantidad);
        }

        _boxCollider2D.enabled = true; //una vez q hace la envestida se vuelve a activar
    }

    public void AplicarDañoPersonaje(float cantidad)
    {
        float dañoPorRealizar = 0;

        //se verifica q se le pueda hacer daño
        if ( Random.value < stats.PorcentajeBloqueo / 100)
        {
            return;
        }

        //dañoo que se le va a realizar asegurando q como minimo recibe 1
        dañoPorRealizar = Mathf.Max(cantidad - stats.Defensa, 1f);
        //se optiene la referncia del personaje para poder dañarlo
        PersonajeReferencia.GetComponent<PersonajeVida>().RecibirDaño(dañoPorRealizar);
        //enviar el evento del daño para q se vea el daño q se esta inflingiendo
        EventoDañoRealizado?.Invoke(dañoPorRealizar);
    }

    public bool PersonajeEnRangoDeAtaque(float rango)
    {
        float distanciaHaciaPersonaje = (PersonajeReferencia.position - transform.position).sqrMagnitude;
        if (distanciaHaciaPersonaje < Mathf.Pow(rango,2))
        {
            return true;
        }
        return false;
    }


    public bool EsTiempoDeAtacar()
    {//si el tiempo actual del juego es mayor que el tiempo de ataque
        if (Time.time > tiempoParaSiguienteAtaque)
        {//retorno verdadero para que ataque
            return true;

        }
        return false;
    }

    public void ActualizarTiempoEntreAtaques() 
    { 
        tiempoParaSiguienteAtaque = Time.time + tiempoEntreAtaques;

    }
    private void OnDrawGizmos()
    {
        if (mostrarDeteccion)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, rangoDeteccion);
        }
        if (mostrarRangoDeAtaque)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, rangoDeAtaque);
        }

        if (mostrarRangoDeEmbestida)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, rangoDeEmbestida);
        }
    }
}
