using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class coins : MonoBehaviour
{
    private Animator animator;
    public AudioClip SonidoMoneda;
    public int valor = 1;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            AudioManager.Instance.ReproducirSonido(SonidoMoneda);
            GameManager.Instance.SumarPuntos(valor);
            Destroy(this.gameObject);
        }
    }
}