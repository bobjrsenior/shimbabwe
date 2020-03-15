using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public Transform spawnPoint;
    public Vector3 spawnPointVec3;
    public Transform playerCamera;
    public float jumpForce;
    public float movementSpeed;
    public bool canJump;
    public int deathCount = 0;

    Color originalColor;

    private Rigidbody2D rigidbody2D;
    private SpriteRenderer spriteRenderer;

    public GameObject enemyPrefab;

    // Start is called before the first frame update
    public virtual void Start()
    {
        rigidbody2D = this.GetComponent<Rigidbody2D>();
        spriteRenderer = this.GetComponent<SpriteRenderer>();
        canJump = true;
        spawnPointVec3 = transform.position;

        originalColor = spriteRenderer.color;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(movementSpeed * Input.GetAxis("Horizontal") * Time.deltaTime, 0.0f, 0.0f);

        if(Input.GetButtonDown("Jump"))
            Jump();
    }

    public void Jump()
    {
        if(canJump)
        {
            rigidbody2D.AddForce(transform.up * jumpForce * Mathf.Sign(rigidbody2D.gravityScale));
            canJump = false;
        }
    }

    public virtual void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag.StartsWith("Terrain"))
        {
            canJump = true;
            spriteRenderer.color = col.gameObject.GetComponent<SpriteRenderer>().color;
        }
        if (col.gameObject.tag.Contains("Enemy") && gameObject.tag.Contains("Player") && (spawnPoint.position - transform.position).magnitude > 3.0f)
        {
            Vector3 curPosition = transform.position;
            if (spawnPoint != null)
                transform.position = spawnPoint.position;
            else
                transform.position = spawnPointVec3;

            deathCount++;
            Instantiate(enemyPrefab, curPosition, Quaternion.identity);
        }
    }

    public virtual void OnCollisionExit2D(Collision2D col)
    {

    }

    public virtual void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag.Contains("Mirror_Cam"))
        {
            playerCamera.position = new Vector3(playerCamera.position.x, playerCamera.position.y, -playerCamera.position.z);
            playerCamera.rotation *= Quaternion.AngleAxis(180, playerCamera.up);
        }
        if (col.gameObject.tag.Contains("Mirror_scale"))
        {
            rigidbody2D.gravityScale = -rigidbody2D.gravityScale;
        }
        if (col.gameObject.tag.Contains("Big_Jump"))
        {
            jumpForce *= 2.0f;
        }
        if(col.gameObject.tag.Contains("Slow_Movement"))
        {
            movementSpeed /= 2.0f;
        }
        if (col.gameObject.tag.Contains("Half_Gravity"))
        {
            rigidbody2D.gravityScale /= 2.0f;
        }
        if (col.gameObject.tag.Contains("Death"))
        {
            Vector3 curPosition = transform.position;
            if (spawnPoint != null)
                transform.position = spawnPoint.position;
            else
                transform.position = spawnPointVec3;

            deathCount++;
        }
        if (col.gameObject.tag.Contains("Goal") && gameObject.tag.Contains("Player"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

    }

    public virtual void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag.Contains("Mirror_Cam"))
        {
            playerCamera.position = new Vector3(playerCamera.position.x, playerCamera.position.y, -playerCamera.position.z);
            playerCamera.rotation *= Quaternion.AngleAxis(180, playerCamera.up);
        }
        if (col.gameObject.tag.Contains("Mirror_scale"))
        {
            rigidbody2D.gravityScale = -rigidbody2D.gravityScale;
        }
        if (col.gameObject.tag.Contains("Big_Jump"))
        {
            jumpForce /= 2.0f;
        }
        if (col.gameObject.tag.Contains("Slow_Movement"))
        {
            movementSpeed *= 2.0f;
        }
        if (col.gameObject.tag.Contains("Half_Gravity"))
        {
            rigidbody2D.gravityScale *= 2.0f;
        }
    }
}
