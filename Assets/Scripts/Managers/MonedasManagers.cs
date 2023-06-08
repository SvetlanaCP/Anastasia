using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class MonedasManagers : Singleton<MonedasManagers>
{
    [SerializeField] private int monedasTest;
    public int MonedasTotales { get; set; }

    private string KEY_MONEDAS = "MisMonedas";

    private void Start()
    {
        PlayerPrefs.DeleteKey(KEY_MONEDAS);
        CargarMonedas();
    }

    private void CargarMonedas()
    {
        MonedasTotales = PlayerPrefs.GetInt(KEY_MONEDAS, monedasTest);
    }
    public void AñadirMonedas(int cantidad)
    {
        MonedasTotales += cantidad;
        PlayerPrefs.SetInt(KEY_MONEDAS, MonedasTotales);
        PlayerPrefs.Save();
    }

    public void RemoverMonedas(int cantidad)
    {
        if (cantidad > MonedasTotales)
        {
            return;
        }

        MonedasTotales -= cantidad;
        PlayerPrefs.SetInt(KEY_MONEDAS, MonedasTotales);
        PlayerPrefs.Save();
    }
}
