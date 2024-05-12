using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrullar : MonoBehaviour
{

    [SerializeField] private float velocidadMovimiento;
    [SerializeField] private Transform[] puntosMovimiento;
    [SerializeField] private float distanciaMinima;

    private int siguientePase = 0;
    private SpriteRenderer spriteRenderer;
    private DistanciaJugador distanciaJugador;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        distanciaJugador = FindObjectOfType<DistanciaJugador>();
    }
    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, puntosMovimiento[siguientePase].position, velocidadMovimiento * Time.deltaTime);
        if (distanciaJugador.distancia >= 10)
        {
            if (Vector2.Distance(transform.position, puntosMovimiento[siguientePase].position) < distanciaMinima)
            {
                siguientePase++;
                if (siguientePase >= puntosMovimiento.Length)
                {
                    siguientePase = 0;
                }
                Girar();
            }
        }
    }
    private void Girar()
    {
        if(transform.position.x < puntosMovimiento[siguientePase].position.x)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX= false;
        }
    }
}
