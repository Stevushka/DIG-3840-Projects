using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Reflection;

public class GameController : MonoBehaviour
{
    //Public variables
    public Text countText;
    public Text winText;
    public Text livesText;
    public Image uiRemote;

    //Defining Audio
    public AudioClip musicClipOne;
    public AudioClip musicClipTwo;
    public AudioSource musicSource;

    //Private variables
    private int scoreValue;
    private int playerLives;

    //Timer Stuff
    public Text uiText;
    public float mainTimer;
    private float timer;
    private bool canCount = true;
    private bool doOnce = false;

    //Calling other game objects and scripts
    private PlayerScript playerScript;
    private GameObject playerObject;
    private CameraScript cameraScript;
    private GameObject cameraObject;
    //private GameObject canvasObject;

    // Start is called before the first frame update
    void Start()
    {
        playerObject = GameObject.FindWithTag("Player");
        if (playerObject != null)
        {
            playerScript = playerObject.GetComponent<PlayerScript>();
        }
        if (playerScript == null)
        {
            Debug.Log("Cannot find 'player' script"); 
        }

        cameraObject = GameObject.FindWithTag("MainCamera");
        if (cameraObject != null)
        {
            cameraScript = cameraObject.GetComponent<CameraScript>();
        }
        if (cameraScript == null)
        {
            Debug.Log("Cannot find 'camera' script");
        }

        //canvasObject = GameObject.Find("Canvas");
        //uiRemote = canvasObject.GetComponent<Image>();
        //uiRemote.SetActive(false);
        uiRemote.enabled = !uiRemote.enabled;

        playerLives = 3;
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            scoreValue = 4;
            playerLives = 3;
        }
        else
        {
            scoreValue = 0;
        }
        winText.text = "";
        SetCountText();
        musicSource.loop = true;
        musicSource.clip = musicClipOne;
        musicSource.Play();
        timer = mainTimer;
    }

    // Update is called once per frame
    void Update()
    {
        //If the player runs out of time
        if (timer >= 0.0f && canCount)
        {
            timer -= Time.deltaTime;
            uiText.text = timer.ToString("F");
        }
        else if (timer <= 0.0f && !doOnce)
        {
            canCount = false;
            doOnce = true;
            uiText.text = "0.00";
            timer = 0.0f;
            TimeUp();
        }
    }

    void SetCountText()
    {
        countText.text = "Points Earned: " + scoreValue.ToString();
        livesText.text = "Lives Left: " + playerLives.ToString();
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            if (playerLives <= 0)
            {
                Destroy(this.gameObject);
                winText.text = "You Lose!";
            }
            else if (scoreValue >= 8)
            {
                Destroy(this);
                winText.text = "You Win!";
                musicSource.Stop();
                musicSource.loop = false;
                musicSource.clip = musicClipTwo;
                musicSource.Play();
            }
        }
        else
        {
            if (playerLives <= 0)
            {
                Destroy(playerObject);
                Destroy(cameraScript);
                winText.text = "You Lose!";
            }
            else if (scoreValue >= 4)
            {
                SceneManager.LoadScene(1);
            }
        }
    }

    public void AddScore()
    {
        scoreValue++;
        SetCountText();
    }

    public void TakeLife()
    {
        playerLives--;
        SetCountText();
    }

    public void TimeUp()
    {
        Destroy(playerScript);
        Destroy(cameraScript);
        Destroy(this);
        winText.text = "You Ran Out Of Time!";
    }

    public void HasRemote()
    {
        //uiRemote.SetActive(true);
        uiRemote.enabled = true;
    }
}
