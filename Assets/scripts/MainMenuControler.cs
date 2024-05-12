using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuControler : MonoBehaviour
{
    public void Quit()
    {
        Application.Quit();
    }
    public void Inicio()
    {
        SceneManager.LoadScene("demo");
    }
}
