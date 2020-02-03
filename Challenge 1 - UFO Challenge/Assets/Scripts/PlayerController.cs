using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public Text countText;
    public Text winText;

    private Rigidbody2D rb2d;
    private int count;
    //private int pickupCount;
    private int playerLives;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        count = 0;
        //pickupCount = 0;
        playerLives = 3;
        winText.text = "";
        SetCountText();
    }

    void Update()
    {
        
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        rb2d.AddForce(movement * speed);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count++;
            //pickupCount++;
            SetCountText();
        }
        if (other.gameObject.CompareTag("Subtractor"))
        {
            other.gameObject.SetActive(false);
            playerLives--;
            SetCountText();
        }
    }

    void SetCountText()
    {
        countText.text = "Total Points Earned: " + count.ToString() + "\nLives Left: " + playerLives.ToString();
        if(SceneManager.GetActiveScene().buildIndex == 1)
        {
            if (playerLives <= 0)
            {
                Destroy(this.gameObject);
                //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                winText.text = "Well, that was anti-climactic, you lose! I was really rooting for you.";
            }
            else if (count >= 8)
            {
                Destroy(this);
                winText.text = "You pretty much won. Steven Fisher made the game, so give him a good grade, I guess.";
            }
        }
        else
        { 
            if(playerLives <= 0)
            {

                Destroy(this.gameObject);
                //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                winText.text = "You Lose. How Unfortunate.";
            }
            else if(count >= 12)
            {
                SceneManager.LoadScene(1);
            }
        }
    }
}
