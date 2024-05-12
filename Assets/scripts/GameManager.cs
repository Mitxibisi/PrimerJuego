using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int PuntosTotales { get; private set; }
    private int puntosTotales;
    public static GameManager Instance { get; private set; }
    public HUD hud;
    public bool Dead;
    private int vidas = 3;
    public GameObject gameOverUI;
    public GameObject paused;
    private int esc;

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        Comprobarpausa();

        if (gameOverUI.activeInHierarchy || paused.activeInHierarchy)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.Log("Mas de un Game Manager en escena");
        }
    }

    public void SumarPuntos(int puntosASumar)
    {
        puntosTotales += puntosASumar;
        hud.ActualizarPuntos(puntosTotales);
    }
    public void PerderVida()
    {
        if (!Dead)
        {
            vidas -= 1;

            if (vidas == 0)
            {
                GameOver();
            }
            hud.DesactivarVida(vidas);
        }
        else { Debug.Log("SinErrorAlMorir"); }
    }
    public bool RecuperarVida()
    {
        if (vidas == 0)
        {
            return false;
        }
        else
        {
            if (vidas == 3)
            {
                return false;
            }

            else
            {
                hud.ActivarVida(vidas);
                vidas += 1;
                return true;
            }
        }
    }
    public void GameOver()
    {
        Dead = true;
        gameOverUI.SetActive(true);
    }
    public void Restart()
    {
        Dead = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void StarT()
    {
        SceneManager.LoadScene(0);
    }
    public void Mainmenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void Final()
    {
        SceneManager.LoadScene("Creditos");
    }
    private void Comprobarpausa()
    {
        if (!Dead)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (esc == 0)
                {
                    Time.timeScale = 0f;
                    esc++;
                    paused.SetActive(true);
                }
                else
                {
                    Time.timeScale = 1f;
                    esc--;
                    Debug.Log(esc);
                    paused.SetActive(false);
                }
            }
        }
        else { }
    }
}