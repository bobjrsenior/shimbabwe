using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stats : MonoBehaviour
{
    public Text statsText;
    private GameObject player;
    private Transform playerTransform;
    private Rigidbody2D playerRigidbody2D;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerTransform = player.transform;
        playerRigidbody2D = player.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float altitude = ((int)(playerTransform.position.y * 100)) / 100.0f;
        float velocity = ((int)(playerRigidbody2D.velocity.y * 100)) / 100.0f;
        string stats = "Altitude: " + altitude + "\nVelocity: " + velocity;
        statsText.text = stats; 
    }
}
