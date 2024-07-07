using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjetoSeleccionable : MonoBehaviour
{

    public bool objectIsSelected = false;
    public Transform childTest;
    private float sphereCastRadius = 8.0f;
    private Vector3 halfBox = new Vector3(8,8,8);

    public LayerMask layerMask;
    // Start is called before the first frame update
    void Start()
    {
        //objectIsSelected = true;
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if(Input.GetMouseButtonDown(0))
        {
            Vector3 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //f (cursorPos)
            objectIsSelected = true; 
        }
        */

        if (objectIsSelected) 
        { 
        Vector3 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(cursorPos.x, cursorPos.y, -10);
        }
    }

    private void OnMouseDown()
    { if (GameHandler.emptyCursor)
        {
            objectIsSelected = true;
            GameHandler.emptyCursor = false;
            GameHandler.selectedObject = this;
        }
        else if (GameHandler.emptyCursor == false && GameHandler.selectedObject == this)
        {
            childTest = transform.GetChild(0);
            RaycastHit hit;
            //if (Physics.SphereCast(childTest.position, sphereCastRadius, -transform.forward, out hit, 20f, layerMask))
            //if (Physics.BoxCast(transform.position, halfBox, transform.forward,out hit, Quaternion.identity, 20f, layerMask))
            if (Physics.Raycast(childTest.position, transform.forward, out hit, 20f, layerMask))
            {
                if(hit.transform.GetComponent<TileBehaviour>().tileIsFree)
                { 
                    Debug.Log("Soltar objeto");
                    objectIsSelected = false;
                    GameHandler.emptyCursor = true;
                    GameHandler.selectedObject = null;
                    transform.position = hit.transform.position;    
                }

            }
        }
    }




}
