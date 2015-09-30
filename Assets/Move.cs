using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour {


    bool up;
    bool down;
    bool left;
    bool right;

	// Use this for initialization
	void Start () {

        up = true;
        down = false;
        left = false;
        right = false;

	}
	
	// Update is called once per frame
	void Update () {
	
        if(Input.GetKeyDown(KeyCode.A))
        {
            left = true;
            
            up = false;
            down = false;
            right = false;
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            left = false;

            up = true;
            
            down = false;
            right = false;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            left = false;
            up = false;
            
            down = true;
            
            right = false;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            left = false;
            up = false;
            down = false;
            
            right = true;
        }

        if (left)
        {
            transform.Translate(new Vector3(-0.5f, 0, 0));
        }
        if (up)
        {
            transform.Translate(new Vector3(0, 0.5f, 0));
        }
        if (down)
        {
            transform.Translate(new Vector3(0, -0.5f, 0));
        }
        if (right)
        {
            transform.Translate(new Vector3(0.5f, 0, 0));
        }

	}
}
