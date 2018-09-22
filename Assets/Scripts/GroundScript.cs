using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundScript : MonoBehaviour {

    public GameObject player;
    public GameObject[] obstacles;
    public float speed;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    private void CallReadies()
    {
        for(int i = 0; i < obstacles.Length; i++)
        {
            obstacles[i].GetComponent<MovingBlock>().OnReady();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.CompareTag("Player"))
        {
            player.GetComponent<PlayerController>().SetGroundScale(8.35f);
            CallReadies();
        }
    }
}
