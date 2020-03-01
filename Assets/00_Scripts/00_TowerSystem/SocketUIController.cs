using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using Doozy.Engine.UI;
using UnityEngine.EventSystems;

public class SocketUIController : MonoBehaviour
{
    [SerializeField, Required]
    private GameObject socketMenu;
    [SerializeField, Required]
    private GameObject upgradeMenu;
    [SerializeField, Required]
    private Button upgradeButton;
    [SerializeField, Required]
    private SocketController myTowerSpot;

    public static List<GameObject> socketUIs = new List<GameObject>();

    private void Start()
    {
        RotateUI(socketMenu);
        RotateUI(upgradeMenu);
        
    }

    private void OnEnable()
    {
        RegisterUIs();
    }

    private void OnDisable()
    {
        UnRegisterUIs();
    }

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
        HideAllSocketMenus();

        if (myTowerSpot.currentTower == null)
        {
            socketMenu.SetActive(true);
        }
        else
        {
            upgradeMenu.SetActive(true);
        }
    }

    public void HideMyMenus()
    {
        socketMenu.SetActive(false);
        upgradeMenu.SetActive(false);
    }

    [ResponsiveButtonGroup]
    public static void HideAllSocketMenus()
    {
        foreach (GameObject item in socketUIs) // BUG: Null exception after restarting the level
        {
            item.SetActive(false);
        }
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

    [ResponsiveButtonGroup]
    private void RotateUI(GameObject go)
    {
        //Debug.Log(socketMenu.transform.rotation);
        go.transform.rotation = Quaternion.Euler(0, 0, 0);
        //Debug.Log(socketMenu.transform.rotation);
    }

    private void RegisterUIs()
    {
        socketUIs.Add(socketMenu);
        socketUIs.Add(upgradeMenu);
    }

    private void UnRegisterUIs()
    {
        socketUIs.Remove(socketMenu);
        socketUIs.Remove(upgradeMenu);
    }

}
