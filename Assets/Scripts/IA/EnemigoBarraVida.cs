using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemigoBarraVida : MonoBehaviour
{
    [SerializeField] private Image barraVida;

    private float saludActual;
    private float saludMax;


    void Update()
    {
        barraVida.fillAmount = Mathf.Lerp(barraVida.fillAmount, saludActual /saludMax, 10 + Time.deltaTime);
    }

    //obtener la referencia de estos valores del enemigo
    public void ModificarSalud(float psaludActual, float pSaludMax)
    {
        saludActual = psaludActual;
        saludMax = pSaludMax;
    }
}
