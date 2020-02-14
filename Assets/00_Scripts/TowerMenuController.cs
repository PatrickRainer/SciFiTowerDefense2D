using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Doozy.Engine.UI;

public class TowerMenuController : MonoBehaviour
{
    [SerializeField, Required]
    private GameObject TowerSpotMenu;
    [SerializeField, Required]
    private GameObject UpgradeMenu;
    [SerializeField, Required]
    private TowerSpotController myTowerSpot;

    private void OnMouseUp()
    {
        ShowMenu();
    }

    public void ShowMenu()
    {
        if (myTowerSpot.currentTower == null)
        {
            TowerSpotMenu.SetActive(true);
        }
        else
        {
            UpgradeMenu.SetActive(true);
        }
    }

    public void HideAllMenus()
    {
        TowerSpotMenu.SetActive(false);
        UpgradeMenu.SetActive(false);
    }


}
