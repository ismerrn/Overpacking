using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjetoSeleccionable : MonoBehaviour
{

    public bool objectIsSelected = false;
    // Start is called before the first frame update
    void Start()
    {
        //objectIsSelected = true;
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetMouseButtonDown(0))
        {
            Vector3 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //f (cursorPos)
            objectIsSelected = true; 
        }

        if (objectIsSelected) 
        { 
        Vector3 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(cursorPos.x, cursorPos.y, 0);
        }
    }
}
