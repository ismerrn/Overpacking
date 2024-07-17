using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawerButtonBehavior : MonoBehaviour
{
    public int drawerIndex;

    private void OnMouseDown()
    {
        GameObject.FindWithTag("Drawer").GetComponent<DrawerBehavior>().MakeDrawerAcvite(drawerIndex);
    }
}
