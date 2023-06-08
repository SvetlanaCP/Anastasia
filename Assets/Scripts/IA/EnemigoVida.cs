using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoVida : VidaBase
{
    public static Action<float> EventoEnemigoDerrotado;

    [Header("Vida")]
    [SerializeField] private EnemigoBarraVida barraVidaPrefab;
    [SerializeField] private Transform barraVidaPosicion;

    [Header("Rastros")]
    [SerializeField] private GameObject rastros;

    private EnemigoBarraVida _enemigoBarraVidaCeada;
    private EnemigoInteraccion _enemigoInteraccion;
    private EnemigoMovimiento _enemigoMovimiento;
    private SpriteRenderer _spriteRender;
    private BoxCollider2D _boxCollider2D;
    private IAController _controller;
    private EnemigoLoot _enemigoLoot;

    private void Awake()
    {
        _enemigoInteraccion = GetComponent<EnemigoInteraccion>();
        _enemigoMovimiento = GetComponent<EnemigoMovimiento>();
        _spriteRender = GetComponent<SpriteRenderer>();
        _boxCollider2D = GetComponent<BoxCollider2D>(); 
        _controller = GetComponent<IAController>();
        _enemigoLoot = GetComponent<EnemigoLoot>();
    }

    protected override void Start()
    {
        base.Start();
        CrearBarraVida();
    }
    private void CrearBarraVida()
    {
        _enemigoBarraVidaCeada = Instantiate(barraVidaPrefab, barraVidaPosicion);
        ActualizarBarraVida(Salud, saludMax);
    }

    protected override void ActualizarBarraVida(float vidaActual, float vidaMax)
    {
        _enemigoBarraVidaCeada.ModificarSalud(vidaActual, vidaMax);
    }

    protected override void PersonajeDerrotado()
    {
        DesactivarEnemigo();
        EventoEnemigoDerrotado?.Invoke(_enemigoLoot.ExpGanada);
        QuestsManager.Instance.AñadirProgreso("Mata10", 1);
        QuestsManager.Instance.AñadirProgreso("Mata25", 1);
        QuestsManager.Instance.AñadirProgreso("Mata50", 1);
    }
    private void DesactivarEnemigo()
    {
        rastros.SetActive(true);
        _spriteRender.enabled = false;
        _controller.enabled = false;
        _boxCollider2D.isTrigger = true;
        _enemigoInteraccion.DesactivarSpritesSeleccion();
        _enemigoMovimiento.enabled = false;
        _enemigoBarraVidaCeada.gameObject.SetActive(false);
    }
}
