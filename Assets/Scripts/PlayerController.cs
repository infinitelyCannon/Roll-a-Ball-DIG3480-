using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

    public float speed;
    public Text scoreText;
    public Text winText;
    public Text countText;
    public GameObject groundOne = null;
    public Text timer = null;

    private Rigidbody rb;
    private GameObject Manager;
    private int score;
    private int pickupSum;
    private int pickupTotal;
    private Material mat;
    private float groundScale = 9.25f;
    private float time = 75f;
    private bool hasWon = false;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        winText.text = "";
        pickupSum = 0;
        mat = gameObject.GetComponent<Renderer>().material;
        Manager = GameObject.Find("GameManager");
        score = Manager.GetComponent<GameSingleton>().GetScore();
        if(SceneManager.GetActiveScene().name == "Main")
        {
            pickupTotal = 53;
        }
        else
        {
            pickupTotal = 101;
        }
        SetCountText();
    }

    public void SetGroundScale(float value)
    {
        groundScale = value;
    }

    public void LowerScore()
    {
        score--;
        SetCountText();
    }

    IEnumerator LoadNextScene()
    {
        yield return new WaitForSeconds(3.5f);
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("Second");

        while (!asyncOperation.isDone)
        {
            yield return null;
        }
    }
	
	// Update is called once per frame
	void Update () {
        float hueAngle = UnitAngleDEG(transform.position);
        float min;

        if (Input.GetKeyUp(KeyCode.Escape)) Application.Quit();
        mat.color = Color.HSVToRGB(hueAngle / 360f, Mathf.Abs(transform.position.x / groundScale), Mathf.Abs(transform.position.z / groundScale));

        if (Input.GetKeyUp(KeyCode.Space))
        {
            Debug.Log(pickupTotal);
        }

        if(transform.position.y <= -110f)
        {
            transform.position = new Vector3(0, -89.5f, 0);
        }

        if(timer != null && !hasWon)
        {
            time -= Time.deltaTime;
            min = Mathf.Floor(time / 60f);
            if (time <= 0f)
            {
                timer.text = "0:00";
                speed = 0f;
                SetCountText();
            }
            else
            {
                if(!hasWon)
                    timer.text = Mathf.Floor(min).ToString() + ":" + ((time - (min * 60)) >= 10f ? "" : "0") + Mathf.Floor(time - (min * 60)).ToString();
            }
        }
 	}

    private float UnitAngleDEG(Vector3 position)
    {
        Vector3 unitVector = Vector3.Normalize(position);
        float refAngle = Mathf.Atan(unitVector.z / unitVector.x) * Mathf.Rad2Deg;

        if (unitVector.z >= 0f && unitVector.x >= 0f)
            return refAngle;
        else if (unitVector.z >= 0f && unitVector.x < 0f)
            return 180f - Mathf.Abs(refAngle);
        else if (unitVector.z < 0f && unitVector.x < 0f)
            return 180f + Mathf.Abs(refAngle);
        else
            return 360f - Mathf.Abs(refAngle);
    }

    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical);

        rb.AddForce(movement * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive(false);
            score += other.gameObject.GetComponent<Rotator>().GetScore();
            pickupSum++;
            SetCountText();
        }
        else if(other.gameObject.CompareTag("Penalty"))
        {
            other.gameObject.SetActive(false);
            score--;
            SetCountText();
        }
    }

    void SetCountText()
    {
        scoreText.text = "Score: " + score.ToString();
        countText.text = "Pickups: " + pickupSum.ToString();
        if(pickupSum >= 13)
        {
            if(groundOne != null)
                groundOne.SetActive(false);
        }
        if(pickupSum >= pickupTotal)
        {
            if(SceneManager.GetActiveScene().name == "Main")
            {
                winText.text = "You Finished with a score of: " + score + "\nPrepare for the time challenge!";
                Manager.GetComponent<GameSingleton>().UpdateScore(score);
                StartCoroutine("LoadNextScene");
            }
            else
            {
                hasWon = true;
                if(time <= 0f)
                {
                    winText.text = "You Lose";
                }
                else
                {
                    winText.text = "You Finished with a score of: " + score;
                }
            }
        }
        if(time <= 0f && SceneManager.GetActiveScene().name == "Second" && !hasWon)
        {
            winText.text = "You Lose";
        }
    }
}
