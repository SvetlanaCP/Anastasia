using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TipoPersonaje
{
    Player,
    IA
}
public class PersonajeFX : MonoBehaviour
{
    [Header("Pooler")]
    [SerializeField] private ObjectPooler pooler;

    [Header("Config")]
    [SerializeField] private GameObject canvasTextoAnimacionPrefab;
    [SerializeField] private Transform canvasTextoPosition;

    [Header("Tipo")]
    [SerializeField] private TipoPersonaje tipoPersonaje;

    private EnemigoVida _enemigoVida;

    private void Awake()
    {
        _enemigoVida = GetComponent<EnemigoVida>();
    }

    private void Start()
    {
        pooler.CrearPooler(canvasTextoAnimacionPrefab);
    }

    private IEnumerator IEMostrarTexto(float cantidad, Color color)
    {
        GameObject nuevoTextoGO = pooler.ObtenerInstancia();
        TextoAnimacion texto = nuevoTextoGO.GetComponent<TextoAnimacion>(); 
        texto.EstablecesTexto(cantidad, color);
        nuevoTextoGO.transform.SetParent(canvasTextoPosition);
        nuevoTextoGO.transform.position = canvasTextoPosition.position;
        nuevoTextoGO.SetActive(true);

        yield return new WaitForSeconds(1f);
        nuevoTextoGO.SetActive(false);
        nuevoTextoGO.transform.SetParent(pooler.listaContenedor.transform);
    }

    private void RespuestaDa�oRecibidoHaciaPlayer(float da�o)
    {
        if (tipoPersonaje == TipoPersonaje.Player)
        {
            StartCoroutine(IEMostrarTexto(da�o, Color.black));
        }
        
    }

    private void RespuestaDa�oHaciaEnemigo(float da�o, EnemigoVida enemigoVida)
    {//si el personaje es de tipo enemigo, mostramos el da�o q se le hace al enemigo
        if (tipoPersonaje == TipoPersonaje.IA && _enemigoVida == enemigoVida)
        {
            StartCoroutine(IEMostrarTexto(da�o, Color.red));
        }
    }
    private void OnEnable()
    {
        IAController.EventoDa�oRealizado += RespuestaDa�oRecibidoHaciaPlayer;
        PersonajeAtaque.EventoEnemigoDa�ado += RespuestaDa�oHaciaEnemigo;
    }

    private void OnDisable()
    {
        IAController.EventoDa�oRealizado -= RespuestaDa�oRecibidoHaciaPlayer;
        PersonajeAtaque.EventoEnemigoDa�ado -= RespuestaDa�oHaciaEnemigo;
    }
}