using UnityEngine;
using System.Collections;

public class ButtonScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void LoadFirstLevel()
    {
        //load the first level
        Application.LoadLevel(1);
    }

    public void LoadMainMenu()
    {
        //load the main menu
        Application.LoadLevel(0);
    }

    public void LoadScore()
    {
        //load the score screen
        Application.LoadLevel(2);
    }
}
