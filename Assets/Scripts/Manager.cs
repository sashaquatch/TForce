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
        generateItems();
    }
	
	// Update is called once per frame
	void Update ()
    {
        cleanItems();
        generateItems();
    }

    void generateItems()
    {
        while (items.Count < 3)
        {
            GameObject newItem = (GameObject)Instantiate(Resources.Load("Item"));
            newItem.GetComponent<Item>().xPos = Random.Range(0, 10);
            newItem.GetComponent<Item>().zPos = Random.Range(0, 10);
            items.Add(newItem);
        }
    }

    void cleanItems()
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i] == null)
                items.RemoveAt(i);
        }
    }
}
