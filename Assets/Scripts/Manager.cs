using UnityEngine;
using System.Collections;
using System.Collections.Generic;   //To use list

public class Manager : MonoBehaviour
{
    public int itemCount;                               //Number of items.
    public int powerupCount;                            //Number of powerups.
    public int xSize;                                   //X size of map
    public int zSize;                                   //Z size of map
    public bool easyMode;                               //Stops items from spawning on map edges

    //--> !ADD NEW POWERUPS HERE! <--
    public GameObject itemPrefab;                       //Prefab for Item
    public GameObject multPrefab;                       //Prefab for Multiple eat powerup
    public GameObject speePrefab;                       //Prefab for Speed powerup
    public GameObject rapiPrefab;                       //Prefab for Rapid powerup
	public GameObject multShotPrefab;
	public GameObject minePrefab;
	public GameObject crazyPrefab;

    private List<int[]> occupied;                       //List of occupied locations
    private List<GameObject> items;                     //List of existing items
    private Dictionary<GameObject, int> pupDictionary;  //Dictionary of possible powerups. POWERUP GAMEOBJECT, RANDOM GENERATOR VALUE
    private List<GameObject> powerups;                  //List of existing powerups

	// Use this for initialization
	void Start ()
    {
        //Lists and dictionaries
        items = new List<GameObject>();
        occupied = new List<int[]>(); 
        pupDictionary = new Dictionary<GameObject, int>();
        powerups = new List<GameObject>();

        //--> !ADD NEW POWERUPS TO DICTIONARY HERE! <--
        pupDictionary.Add(multPrefab, 50);
		pupDictionary.Add(multShotPrefab,55 );
		pupDictionary.Add(minePrefab, 65);
        pupDictionary.Add(speePrefab, 85);
        pupDictionary.Add(rapiPrefab, 95);
		pupDictionary.Add(crazyPrefab, 100);

        //Initial generation
        generateItems();
        generatePups();
    }
	
	// Update is called once per frame
	void Update ()
    {
        cleanItems();
        generateItems();
        cleanPups();
        generatePups();
    }

    //Generate new items
    private void generateItems()
    {
        //If there less than 3 items, fill new spawning comparison lists.
        if (items.Count < itemCount)
        {
            fillOccupiedLocations();
        }

        //While there are less than 3 items, generate a new item
        while (items.Count < itemCount)
        {
            GameObject newItem = (GameObject)Instantiate(itemPrefab);   //New item to spawn

            //Get a new coordinate and set the new item's position to it.
            int[] coor = getClearLocation();
            newItem.GetComponent<Item>().xPos = coor[0];
            newItem.GetComponent<Item>().zPos = coor[1];

            //Add the new position to the list of occupied locations
            occupied.Add(new int[2]);
            occupied[occupied.Count - 1][0] = coor[0];
            occupied[occupied.Count - 1][1] = coor[1];

            //Fix for spawn flash
            newItem.transform.position = new Vector3(
                newItem.GetComponent<Item>().xPos,
                .5f, 
                newItem.GetComponent<Item>().zPos);

            //Add new item
            items.Add(newItem);
        }
    }

    //Generate new powerups
    private void generatePups()
    {
        //If there less than 3 powerups, fill new spawning comparison lists.
        if (powerups.Count < powerupCount)
        {
            fillOccupiedLocations();
        }

        //While there are less than 3 powerups, generate a new powerup
        while (powerups.Count < powerupCount)
        {
            float pupRand = Random.Range(0, 100);
            GameObject newPup = null;   //New powerup to spawn

            foreach (KeyValuePair<GameObject, int> pair in pupDictionary)
            {
                if(pair.Value > pupRand)
                {
                    newPup = (GameObject)Instantiate(pair.Key);
                    break;
                }
            }

            //Get a new coordinate and set the new powerup's position to it.
            int[] coor = getClearLocation();
            newPup.GetComponent<Item>().xPos = coor[0];
            newPup.GetComponent<Item>().zPos = coor[1];

            //Add the new position to the list of occupied locations
            occupied.Add(new int[2]);
            occupied[occupied.Count - 1][0] = coor[0];
            occupied[occupied.Count - 1][1] = coor[1];

            //Fix for spawn flash
            newPup.transform.position = new Vector3(
                newPup.GetComponent<Item>().xPos,
                .5f,
                newPup.GetComponent<Item>().zPos);

            //Add new powerup
            powerups.Add(newPup);
        }
    }


    //Fills a list of occupied locations
    private void fillOccupiedLocations()
    {
        occupied = new List<int[]>();
        GameObject[] allObjects = (GameObject[])FindObjectsOfType(typeof(GameObject)); 
        for (int i = 0; i < allObjects.Length; i++)
        {
            //If object is a snake part, add its position to occupied
            if(allObjects[i].GetComponent<SnekPartMove>() != null)
            {
                occupied.Add(new int[2]);
                occupied[occupied.Count - 1][0] = allObjects[i].GetComponent<SnekPartMove>().xPos;
                occupied[occupied.Count - 1][1] = allObjects[i].GetComponent<SnekPartMove>().zPos;
            }

            //If object is a snake head, add its position to occupied
            else if (allObjects[i].GetComponent<SnekHead>() != null)
            {
                occupied.Add(new int[2]);
                occupied[occupied.Count - 1][0] = allObjects[i].GetComponent<SnekHead>().xPos;
                occupied[occupied.Count - 1][1] = allObjects[i].GetComponent<SnekHead>().zPos;
            }

            //If object is an item, add its position to occupied
            else if (allObjects[i].GetComponent<Item>() != null)
            {
                occupied.Add(new int[2]);
                occupied[occupied.Count - 1][0] = allObjects[i].GetComponent<Item>().xPos;
                occupied[occupied.Count - 1][1] = allObjects[i].GetComponent<Item>().zPos;
            }
        }
    }
      
    //Gets an unoccupied location on the grid
    private int[] getClearLocation()
    {
        //New coordinates
        int[] coor = new int[2];

        //True if spawn location is occupied
        bool isOccupied = true;

        //Loop spawn occupation check
        while (isOccupied)
        {
            isOccupied = false;

            //New location
            if (easyMode)
            {
                coor[0] = Random.Range(1, xSize - 1);
                coor[1] = Random.Range(1, zSize - 1);
            }
            else
            {
                coor[0] = Random.Range(0, xSize);
                coor[1] = Random.Range(0, zSize);
            }

            //Check if location is occupied by snake parts.
            for (int i = 0; i < occupied.Count; i++)
            {
                if (
                    coor[0] == occupied[i][0] &&
                    coor[1] == occupied[i][1])
                {
                    isOccupied = true;
                    break;
                }
            }
        }

        return coor;
    }

    //Remove all null-missing items.
    private void cleanItems()
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i] == null)
                items.RemoveAt(i);
        }
    }

    //Remove all null-missing items.
    private void cleanPups()
    {
        for (int i = 0; i < powerups.Count; i++)
        {
            if (powerups[i] == null)
                powerups.RemoveAt(i);
        }
    }
}
