using UnityEngine;
using System.Collections;
using System.Collections.Generic;   //To use list

public class Manager : MonoBehaviour
{
    public int itemCount;                   //Number of items.
    private List<GameObject> items;
    private List<GameObject> snakeParts;    //Snake parts for spawning comparison
    private List<GameObject> snakeHeads;    //Snake heads for spawning comparison

	// Use this for initialization
	void Start ()
    {
        items = new List<GameObject>();
        snakeParts = new List<GameObject>();
        snakeHeads = new List<GameObject>();
        generateItems();
    }
	
	// Update is called once per frame
	void Update ()
    {
        cleanItems();
        generateItems();
    }

    //Generate new items
    void generateItems()
    {
        //If there less than 3 items, fill new spawning comparison lists.
        if (items.Count < itemCount)
        {
            snakeParts = new List<GameObject>();
            snakeHeads = new List<GameObject>();
            GameObject[] allObjects = (GameObject[])FindObjectsOfType(typeof(GameObject));
            for(int i = 0; i < allObjects.Length; i++)
            {
                if(allObjects[i].GetComponent<SnekPartMove>() != null)
                {
                    snakeParts.Add(allObjects[i]);
                }
                if(allObjects[i].GetComponent<SnekHead>() != null)
                {
                    snakeHeads.Add(allObjects[i]);
                }
            }
        }

        //While there are less than 3 items, generate a new item
        while (items.Count < itemCount)
        {
            GameObject newItem = (GameObject)Instantiate(Resources.Load("Item"));   //New item to spawn
            bool occupied = true;                                                   //True if spawn location is occupied

            //Loop spawn occupation check
            while (occupied)
            {
                occupied = false;

                //New location
                newItem.GetComponent<Item>().xPos = Random.Range(0, 10);
                newItem.GetComponent<Item>().zPos = Random.Range(0, 10);

                //Check if location is occupied by snake parts.
                for(int i = 0; i < snakeParts.Count; i++)
                {
                    if(
                        newItem.GetComponent<Item>().xPos == snakeParts[i].GetComponent<SnekPartMove>().xPos &&
                        newItem.GetComponent<Item>().zPos == snakeParts[i].GetComponent<SnekPartMove>().zPos)
                    {
                        occupied = true;
                        break;
                    }
                }

                //Check if location is occupied by snake heads.
                if(occupied == false)
                {
                    for (int i = 0; i < snakeHeads.Count; i++)
                    {
                        if (
                            newItem.GetComponent<Item>().xPos == snakeHeads[i].GetComponent<SnekHead>().xPos &&
                            newItem.GetComponent<Item>().zPos == snakeHeads[i].GetComponent<SnekHead>().zPos)
                        {
                            occupied = true;
                            break;
                        }
                    }
                }

                //Check if location is occupied by other items.
                if(occupied == false)
                {
                    for(int i = 0; i < items.Count; i++)
                    {
                        if (
                            newItem.GetComponent<Item>().xPos == items[i].GetComponent<Item>().xPos &&
                            newItem.GetComponent<Item>().zPos == items[i].GetComponent<Item>().zPos)
                        {
                            occupied = true;
                            break;
                        }
                    }
                }
            }

            newItem.transform.position = new Vector3(   //Fix for spawn flash
                newItem.GetComponent<Item>().xPos,
                .5f,
                newItem.GetComponent<Item>().zPos);
            items.Add(newItem); //Add new item
        }
    }

    //Remove all null-missing items.
    void cleanItems()
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i] == null)
                items.RemoveAt(i);
        }
    }
}
