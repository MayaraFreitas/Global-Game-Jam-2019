using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraFollow : MonoBehaviour
{
    public GameObject player;
    private Vector3 offset; //Private variable to store the offset distance between the player and camera

    public Text TimeText;
    public float ClockTime = 0.0f;

    public static CameraFollow instancia;

    void Awake()
    {
        instancia = this;


    }
    public void Start () 
    {
        // Getting the distance between the player's position and camera's position.
        print(string.Format("transform.position:{0} | player.transform.position: {1}", new object[] { transform.position, player.transform.position }));
        offset = transform.position - player.transform.position;
        print("offset: " + offset);
        transform.position = player.transform.position;
    }

    public void Update()
    {
        Clock();
    }

    public void LateUpdate () 
    {
        //print("POSICAO: " + (player.transform.position + new Vector3(0, offset.y, offset.x)).ToString());
        transform.position = player.transform.position + new Vector3(0, (offset.y * 0.2f), offset.x);
    }

    public void Clock()
    {
        ClockTime += Time.deltaTime;

        int seconds = (int)(ClockTime % 60);
        int minutes = (int)((ClockTime / 60) % 60);
        int hours = (int)((ClockTime / 3600) % 60);

        TimeText.text = string.Format("{0:0}:{1:00}:{2:00}", new object[] { hours, minutes, seconds });
    }
}
