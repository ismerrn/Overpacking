using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class ObjetoSeleccionable : MonoBehaviour
{

    public bool objectIsSelected = false;


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
            transform.parent = null;
            foreach (Transform child in transform)
            {
                RaycastHit hit;
            if (Physics.Raycast(child.position, transform.forward, out hit, 20f, layerMask))
            {
                if (hit.transform.GetComponent<TileBehaviour>() != null && !hit.transform.GetComponent<TileBehaviour>().tileIsFree)
                {
                    hit.transform.GetComponent<TileBehaviour>().tileIsFree = true;
                }
            }
            }
        }
        else if (GameHandler.emptyCursor == false && GameHandler.selectedObject == this)
        {
            bool canPlace = true;
            foreach (Transform child in transform)
            {
                Debug.Log(child.position);
                RaycastHit hit;
                 if (Physics.Raycast(child.position, transform.forward, out hit, 20f, layerMask))
                 {

                    if(!hit.transform.GetComponent<TileBehaviour>().tileIsFree )
                    {
                        Debug.Log("esta entrando en el if !hit");
                        canPlace = false;
                        break;
                    }
                 }
                 else
                 {
                    Debug.Log("esta entrando en el else");
                    canPlace = false;
                    break;
                 }
                                
            }
            if (canPlace)
            {
                Debug.Log("esta entrando en canPlace");
                
                objectIsSelected = false;
                GameHandler.emptyCursor = true;
                GameHandler.selectedObject = null;
                GameObject mainChild = null;
                foreach (Transform child in transform)
                { 
                    if (child.CompareTag("MainHigo"))
                    {
                        mainChild = child.gameObject;
                        break;
                    }
                }
                RaycastHit hit;
                if (Physics.Raycast(mainChild.transform.position, transform.forward, out hit, 20f, layerMask))
                { 
                    transform.position = new Vector3(hit.transform.position.x, hit.transform.position.y, -10);
                    transform.parent = hit.transform.parent;

                }
                foreach (Transform child in transform)
                {
                    RaycastHit hit2;
                    if (Physics.Raycast(child.position, transform.forward, out hit2, 20f, layerMask))
                    { 
                    hit2.transform.GetComponent<TileBehaviour>().tileIsFree = false;
                    }
                }
            }




        }
    }




}
