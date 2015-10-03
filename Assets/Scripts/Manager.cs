using UnityEngine;
using System.Collections;
using System.Collections.Generic;   //To use list

public class Manager : MonoBehaviour
{
    List<GameObject> items;

	// Use this for initialization
	void Start ()
    {
        items = new List<GameObject>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (items.Count < 1)
        {
            GameObject newItem = (GameObject)Instantiate(Resources.Load("Item"));
            newItem.GetComponent<Item>().xPos = Random.Range(0, 5);
            newItem.GetComponent<Item>().zPos = Random.Range(0, 5);
            items.Add(newItem);
        }
    }
}
