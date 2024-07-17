using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawerBehavior : MonoBehaviour
{
    public int activeDrawerIndex;
    public GameObject[] drawers;
    public GameObject[] buttonsBG;
    public GameObject[] buttonsIcon;
    private int previousActiveDrawerIndex;
    void Start()
    {
        activeDrawerIndex = 0;
        previousActiveDrawerIndex = 0;
    }

    void Update()
    {
        
    }

    public void MakeDrawerAcvite(int index)
    {
        if (index!= activeDrawerIndex)
        {
            activeDrawerIndex = index;
            drawers[index].SetActive(true);
            buttonsBG[index].transform.localScale = new Vector3(1.2f, 1.2f, 1);
            buttonsIcon[index].transform.position += new Vector3(2f, 0, 0);

            for (int i = 0; i < drawers.Length; i++) 
            {
                if (i!=index)
                {   
                    drawers[i].SetActive(false);
                    buttonsBG[i].transform.localScale = new Vector3 (1,1,1);
                }
            }
            buttonsIcon[previousActiveDrawerIndex].transform.position += new Vector3(-2f, 0, 0);
            previousActiveDrawerIndex = activeDrawerIndex;
        }
    }
}
