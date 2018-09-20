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
    public Text debugText;

    private Rigidbody rb;
    //private int count;
    private int score;
    private int pickupSum;
    private int pickupTotal;
    private Material mat;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        score = 0;
        winText.text = "";
        pickupSum = 0;
        pickupTotal = GameObject.FindGameObjectsWithTag("Pick Up").Length;
        SetCountText();
        mat = gameObject.GetComponent<Renderer>().material;
    }
	
	// Update is called once per frame
	void Update () {
        float hueAngle = UnitAngleDEG(transform.position);
        if (Input.GetKeyUp(KeyCode.Escape)) Application.Quit();
        mat.color = Color.HSVToRGB(hueAngle / 360f, 1f/* TODO: Change this */, 0.5f/* TODO: Change this */);
        //debugText.text = hueAngle + " Degrees";
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

    /*private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive(false);
            score++;
            pickupSum++;
            SetCountText();
        }
        else if(other.gameObject.CompareTag("Penalty"))
        {
            other.gameObject.SetActive(false);
            score--;
            SetCountText();
        }
    }*/

    void SetCountText()
    {
        scoreText.text = "Score: " + score.ToString();
        countText.text = "Pickups: " + pickupSum.ToString();
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
