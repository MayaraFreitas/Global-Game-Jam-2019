using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GerenciadorDoGame : MonoBehaviour
{
    public GameObject CanvasGB;
    public bool dieCanvas;
    public static GerenciadorDoGame instancia;

    void Awake()
    {
        instancia = this;

    }
    void Start()
    {
        if (CanvasGB != null)
        {
            CanvasGB.SetActive(false);
        }
    }

    void Update()
    {
        
    }

    public void FinalizarJogo(int? points = null)
    {
        print("> " + CanvasGB);
        if (CanvasGB != null)
        {
            string timeText = CameraFollow.instancia.TimeText.text;
            CameraFollow.instancia.TimeText.text = "";
            /*
            transform.

            Transform timeTransform = CanvasGB.gameObject.transform.Find("TimeResult");
            Transform pointsTransform = CanvasGB.gameObject.transform.Find("PointResult");

            timeText
                */

            CanvasGB.SetActive(true);
        }
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
