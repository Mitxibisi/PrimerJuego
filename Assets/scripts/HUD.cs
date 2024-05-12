using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUD : MonoBehaviour
{
    public TextMeshProUGUI puntos;
    public GameObject[] vidas;     
    public void ActualizarPuntos(int puntosTotales)
    {
        puntos.text = puntosTotales.ToString();
    }
    public void  DesactivarVida(int Indice)
    {
        vidas[Indice].SetActive(false);
    }
    public void ActivarVida(int Indice)
    {
        vidas[Indice].SetActive(true);
    }
}
