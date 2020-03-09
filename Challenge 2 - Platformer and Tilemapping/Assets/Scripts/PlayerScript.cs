using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    //Public Variables
    public float speed;

    public Text countText;
    public Text winText;
    public Text livesText;

    public AudioClip musicClipOne;
    public AudioClip musicClipTwo;
    public AudioSource musicSource;

    //Private Variables
    private Rigidbody2D rd2d;

    private int scoreValue;
    private int playerLives;
    //private int count;

    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>(); 
        //score.text = scoreValue.ToString();
        playerLives = 3;
        scoreValue = 0;
        winText.text = "";
        SetCountText();
        musicSource.loop = true;
        musicSource.clip = musicClipOne;
        musicSource.Play();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            if (Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 4), ForceMode2D.Impulse);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Coin")
        {
            scoreValue++;
            //score.text = scoreValue.ToString();
            Destroy(collision.collider.gameObject);
            SetCountText();
        }

    }

    /*
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            other.gameObject.SetActive(false);
            scoreValue++;
            SetCountText();
        }
        if (other.gameObject.CompareTag("Subtractor"))
        {
            other.gameObject.SetActive(false);
            playerLives--;
            SetCountText();
        }
    }
    */
    

    void SetCountText()
    {
        countText.text = "Points Earned: " + scoreValue.ToString();
        livesText.text = "Lives Left: " + playerLives.ToString();
        if (playerLives <= 0)
        {
            Destroy(this.gameObject);
            winText.text = "You Lose!";
        }
        else if (scoreValue >= 4)
        {
            Destroy(this);
            winText.text = "You Win!";
            musicSource.Stop();
            musicSource.loop = false;
            musicSource.clip = musicClipTwo;
            musicSource.Play();
        }
    }
}