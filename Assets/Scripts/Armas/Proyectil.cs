using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proyectil : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private float velocidad;

    public PersonajeAtaque PersonajeAtaque { get; private set; }

    private Rigidbody2D _rigiBody2D;
    private Vector2 direccion;
    private EnemigoInteraccion enemigoObjeto;

    private void Awake()
    {
        _rigiBody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (enemigoObjeto == null)
        {
            return;
        }
        MoverProyectil();
    }
    private void MoverProyectil()
    {
        direccion = enemigoObjeto.transform.position - transform.position;
        float angulo = Mathf.Atan2(direccion.y, direccion.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.AngleAxis(angulo, Vector3.forward);
        _rigiBody2D.MovePosition(_rigiBody2D.position + direccion.normalized * velocidad * Time.fixedDeltaTime);
    }

    public void IniciarProyetil(PersonajeAtaque ataque)
    {
        PersonajeAtaque = ataque;
        enemigoObjeto = ataque.EnemigoObjetivo;
    }

    //para borrar el proyectil cuando llega al enemigo
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemigo"))
        {
            float daño = PersonajeAtaque.ObtenerDaño();
            EnemigoVida enemigoVida = enemigoObjeto.GetComponent<EnemigoVida>();
            enemigoVida.RecibirDaño(daño);
            PersonajeAtaque.EventoEnemigoDañado?.Invoke(daño, enemigoVida);
            gameObject.SetActive(false);
        }
        
    }
}
