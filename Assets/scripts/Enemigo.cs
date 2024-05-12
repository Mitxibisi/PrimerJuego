using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo : MonoBehaviour
{
    public float cooldownAtaque;
    private bool puedeAtacar = true;
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!puedeAtacar) return;

            puedeAtacar = false;

            GameManager.Instance.PerderVida();

            other.gameObject.GetComponent<CharacterControler>().AplicarGolpe();

            Invoke("ReactivarAtaque", cooldownAtaque);
        }
    }
    void ReactivarAtaque()
    {
        puedeAtacar = true; 
    }
}
