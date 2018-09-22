using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBlock : MonoBehaviour {

    public enum ObstacleType
    {
        Type1,
        Type2
    };

    public bool canHurt;
    public ObstacleType type;

    private Animator anim = null;

	// Use this for initialization
	void Start () {
		
	}

    public void OnReady()
    {
        switch (type)
        {
            case ObstacleType.Type1:
            case ObstacleType.Type2:
                if (anim == null) anim = gameObject.GetComponent<Animator>();
                anim.SetTrigger("Begin");
                break;
            default:
                break;

        }
    }
	
	// Update is called once per frame
	void Update () {

	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && type == ObstacleType.Type1){
            other.gameObject.GetComponent<PlayerController>().LowerScore();
        }
    }
}
