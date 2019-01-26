using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GerenciadorDoGame : MonoBehaviour
{
    public GameObject dieCanvas;

    void Start()
    {
        dieCanvas.SetActive(false);
    }

    void Update()
    {
        
    }

    public void FinalizarJogo()
    {
        dieCanvas.SetActive(true);
    }

    public void ChangeScene(string nomeDaCena)
    {
        Application.LoadLevel(nomeDaCena);
    }

    public void CloseGame()
    {
        Application.Quit();
    }
}
