using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Doozy.Engine.UI;

public class TowerSpotMenu : MonoBehaviour
{
    [SerializeField, Required]
    private GameObject menu;

    private void OnMouseUp()
    {
        if (menu.activeSelf)
        {
            menu.SetActive(false);
        }
        else
        {
            menu.SetActive(true);
        }
    }
}
