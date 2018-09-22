using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rotator : MonoBehaviour {

    private Material mat;
    private float offset;

    private void Start()
    {
        mat = gameObject.GetComponent<Renderer>().material;
        offset = Random.Range(0f, 10f);
    }

    // Update is called once per frame
    void Update () {
        transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
        
        if(SceneManager.GetActiveScene().name == "Second")
        {
            mat.color = Color.HSVToRGB(Mathf.Abs(Mathf.Sin(Time.timeSinceLevelLoad + offset)), 1f, 0.5f);
        }
	}

    public int GetScore()
    {
        if(SceneManager.GetActiveScene().name == "Second")
        {
            return Mathf.RoundToInt(Random.Range(1f, 10f));
        }
        else
        {
            return 1;
        }
    }
}
