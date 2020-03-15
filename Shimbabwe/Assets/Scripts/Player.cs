using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public Transform spawnPoint;
    public Transform playerCamera;
    public float jumpForce;
    public float movementSpeed;
    public bool canJump;

    Color originalColor;

    private Rigidbody2D rigidbody2D;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = this.GetComponent<Rigidbody2D>();
        spriteRenderer = this.GetComponent<SpriteRenderer>();
        canJump = true;

        originalColor = spriteRenderer.color;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(movementSpeed * Input.GetAxis("Horizontal") * Time.deltaTime, 0.0f, 0.0f);

        if(Input.GetButtonDown("Jump") && canJump)
        {
            rigidbody2D.AddForce(transform.up * jumpForce * Mathf.Sign(rigidbody2D.gravityScale));
            canJump = false;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag.StartsWith("Terrain"))
        {
            canJump = true;
            spriteRenderer.color = col.gameObject.GetComponent<SpriteRenderer>().color;
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {

    }

    void OnTriggerEnter2D(Collider2D col)
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
            transform.position = spawnPoint.position;
        }
        if (col.gameObject.tag.Contains("Goal"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    void OnTriggerExit2D(Collider2D col)
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
