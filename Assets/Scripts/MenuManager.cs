using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour
{
    public GameObject instructions;
    public GameObject controls;
    
	public void EnableDisableInstructions()
    {
        if (instructions.activeSelf == true)
        {
            instructions.SetActive(false);
        }
        else
        {
            instructions.SetActive(true);
            controls.SetActive(false);
        }
    }

    public void EnableDisableControls()
    {
        if (controls.activeSelf == true)
        {
            controls.SetActive(false);
        }
        else
        {
            controls.SetActive(true);
            instructions.SetActive(false);
        }
    }
}
