using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextoAnimacion : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI da�oTexto;

    public void EstablecesTexto(float cantidad, Color color)
    {
        da�oTexto.text = cantidad.ToString();
        da�oTexto.color = color;
    }
}
