using UnityEngine;
using System.Collections;

public class mainmenu : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Button0"))
        {

            Application.LoadLevel("Test01");
        }
    }
}
