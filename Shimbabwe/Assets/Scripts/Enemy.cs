using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Player
{
    public AudioClip[] audioSources;
    public Transform player;
    public float maxVisibility = 10.0f;
    public GameObject deathFlag;

    public AudioSource audioSource;

    // Start is called before the first frame update
    public virtual void Start()
    {
        base.Start();

        audioSource = GetComponent<AudioSource>();
        audioSource.clip = audioSources[Random.Range(0, audioSources.Length)];
        audioSource.Play();

        movementSpeed *= Random.Range(0.95f, 1.05f);

        player = GameObject.FindGameObjectWithTag("Player").transform;
        Instantiate(deathFlag, transform.position, deathFlag.transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 distance = player.position - transform.position;
        if(distance.magnitude < maxVisibility)
        {
            transform.Translate(movementSpeed * Mathf.Sign(distance.x) * Time.deltaTime, 0.0f, 0.0f);
        }
    }

    public virtual void OnCollisionExit2D(Collision2D col)
    {
        base.OnCollisionExit2D(col);

        if (col.gameObject.tag.StartsWith("Terrain"))
        {
            Jump();
        }
    }
}
