using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    //Public Variables
    public float speed;

    public AudioClip coinSound;
    public AudioClip hurtSound;
    public AudioSource soundSource;

    //Private Variables
    private Rigidbody2D rd2d;
    private GameController gameController;
    private bool facingRight = true;
    private bool isOnGround;
    private bool hasRemote = false;

    //maybe?
    private float dirX;
    private Vector3 localScale;

    Animator anim;



    // Start is called before the first frame update
    void Start()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
        if (gameController == null)
        {
            Debug.Log("Cannot find 'GameController' script");
        }
        
        rd2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        localScale = transform.localScale;
    }

    void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal") * speed;

        if((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) && rd2d.velocity.y == 0)
        {
            rd2d.AddForce(new Vector2(0, 8), ForceMode2D.Impulse);
            anim.SetInteger("State", 2);
        }
        /*
        if(Mathf.Abs(dirX) > 0 && rb2d.velocity.y == 0)
        {

        }

        if(rd2d.velocity.y == 0)
        {
            //not jumping nor falling
        }

        if(rd2d.velocity.y > 0)
        {
            //jumping
            anim.SetInteger("State", 2);
        }

        if(rd2d.velocity.y < 0)
        {
            //falling
            anim.SetInteger("State", 2);
        }
        */

        //GOOD
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            anim.SetInteger("State", 1);
        }

        if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.A))
        {
            anim.SetInteger("State", 0);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            anim.SetInteger("State", 1);
        }

        if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.D))
        {
            anim.SetInteger("State", 0);
        }


    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        if (facingRight == false && hozMovement > 0)
        {
            Flip();
        }
        else if (facingRight == true && hozMovement < 0)
        {
            Flip();
        }
        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));
    }

    private void OnCollisionExit2d(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            isOnGround = false;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground" && isOnGround)
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                rd2d.AddForce(new Vector2(0, 4), ForceMode2D.Impulse);
                anim.SetInteger("State", 2);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Coin")
        {
            gameController.AddScore();
            soundSource.clip = coinSound;
            soundSource.Play();
            Destroy(collision.collider.gameObject);
        }
        if (collision.collider.tag == "Enemy")
        {
            if(!hasRemote)
            {
                gameController.TakeLife();
                soundSource.clip = hurtSound;
                soundSource.Play();
            }
            Destroy(collision.collider.gameObject);
        }
        if (collision.collider.tag == "Remote")
        {
            gameController.HasRemote();
            hasRemote = true;
            Destroy(collision.collider.gameObject);
        }

    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector2 Scaler = transform.localScale;
        Scaler.x = Scaler.x * -1;
        transform.localScale = Scaler;
    }
}