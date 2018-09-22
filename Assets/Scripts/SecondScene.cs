using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondScene : MonoBehaviour {

    public GameObject player;
    public GameObject pickUp;

    public int total;
    private int iter = 0;

	// Use this for initialization
	void Start () {
        player.GetComponent<PlayerController>().SetGroundScale(28f);
        StartCoroutine("Setup");
	}

    IEnumerator Setup()
    {
        float offset = 1f;

        while(iter <= total)
        {
            Object.Instantiate(pickUp, new Vector3(offset, 0.5f, Mathf.Sin(Mathf.Cos(offset / 2f) * 3f) * Mathf.PI), Quaternion.identity);
            offset += 0.57f;
            iter++;
            yield return null;
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
