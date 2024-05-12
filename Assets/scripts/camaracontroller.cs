using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camaracontroller : MonoBehaviour
{

    public Transform personaje;

    private float tamañoCamera;
    private float alturaPantalla;

    // Start is called before the first frame update
    void Start()
    {
        tamañoCamera = Camera.main.orthographicSize;
        alturaPantalla = tamañoCamera * 2;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Calcularpersonajecamara()
    {
        int pantallaPersonaje = (int)(personaje.position.y / alturaPantalla);
        float alturaCamara = (pantallaPersonaje * alturaPantalla) + tamañoCamera;

        transform.position = new Vector3(transform.position.x, alturaCamara, transform.position.z);
    }
}
