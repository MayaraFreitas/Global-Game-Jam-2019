using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GerenciadorDoGame : MonoBehaviour
{
    public GameObject canvasGO;

    // Start is called before the first frame update
    void Start()
    {
        canvasGO.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FinalizarJogo()
    {
        canvasGO.SetActive(true);
    }

    public void AlterarCena(string nomeDaCena)
    {
        Application.LoadLevel(nomeDaCena);
    }

    public void FecharAplicativo()
    {
        Application.Quit();
    }
}
