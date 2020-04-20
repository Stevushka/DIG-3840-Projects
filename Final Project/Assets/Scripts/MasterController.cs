using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterController : MonoBehaviour
{
    void Update()
    {
        //If the player quits
        if (Input.GetKey("escape"))
            Application.Quit();
    }
}
