using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using Doozy.Engine.UI;

public class SocketUIController : MonoBehaviour
{
    [SerializeField, Required]
    private GameObject TowerSpotMenu;
    [SerializeField, Required]
    private GameObject UpgradeMenu;
    [SerializeField, Required]
    private Button upgradeButton;
    [SerializeField, Required]
    private SocketController myTowerSpot;

    private void OnMouseUp()
    {
        ShowMenu();
    }

    private void OnGUI()
    {
        HandleUpgradeButtonInteractable();
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

    private void HandleUpgradeButtonInteractable()
    {
        //  Upgrade Button Activity
        if (myTowerSpot.currentTower != null)
        {
            if (myTowerSpot.currentTower.GetComponent<Tower>().upgradeLevel >= 2)  //Hack: Could be Performance issue
            {
                upgradeButton.interactable = false;
            }
            else
            {
                upgradeButton.interactable = true;
            }
        }
    }




}
