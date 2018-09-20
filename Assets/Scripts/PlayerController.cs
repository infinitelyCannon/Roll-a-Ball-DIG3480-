using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    public float speed;
    public Text scoreText;
    public Text winText;
    public Text countText;
    public GameObject groundOne;

    private Rigidbody rb;
    private int count;
    private int score;
    private int pickupSum;
    private int pickupTotal;
    private Material mat;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        count = 0;
        score = 0;
        winText.text = "";
        pickupSum = 0;
        pickupTotal = GameObject.FindGameObjectsWithTag("Pick Up").Length;
        SetCountText();
        mat = gameObject.GetComponent<Renderer>().material;
    }
	
	// Update is called once per frame
	void Update () {
        float zPos = transform.position.z;
        float xPos = transform.position.x;
        if (Input.GetKeyUp(KeyCode.Escape)) Application.Quit();
        mat.color = Color.HSVToRGB(Mathf.Atan(zPos / xPos) / (2f * Mathf.PI), Mathf.Cos(xPos), Mathf.Sin(zPos));
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
            score++;
            pickupSum++;
            count++;
            SetCountText();
        }
        else if(other.gameObject.CompareTag("Penalty"))
        {
            other.gameObject.SetActive(false);
            score--;
            count++;
            SetCountText();
        }
    }

    void SetCountText()
    {
        scoreText.text = "Score: " + score.ToString();
        countText.text = "Pickups: " + count.ToString();
        if(pickupSum >= 10)
        {
            groundOne.SetActive(false);
        }
        if(pickupSum >= pickupTotal)
        {
            winText.text = "You Finished with a score of: " + score;
        }
    }
}
