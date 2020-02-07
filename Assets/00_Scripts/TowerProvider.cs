using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;


[CreateAssetMenu]
public class TowerProvider : ScriptableObject
{
    [SerializeField, AssetsOnly, 
        DetailedInfoBox("The List with towers has to be filled in row. So for example: Tower 1.1, Tower 1.2, Tower 2.1, Tower 2.2, etc.", "warning")]
    private List<GameObject> availableTowers = new List<GameObject>();

    public GameObject GetFirstTower(int coins)
    {
        if (IsEnoughMoneyProvided(availableTowers[0], coins))
        {
            return availableTowers[0];
        }
        else
        {
            return null;
        }
    }

    public GameObject GetNextTower(Tower _tower, int coins)
    {
        int curTowerIndex = GetIndex(_tower.ID);

        if (curTowerIndex >= availableTowers.Count-1 )
        {
            return null;
        }

        if (IsEnoughMoneyProvided(availableTowers[curTowerIndex + 1],coins))
        {
            return availableTowers[curTowerIndex + 1];
        }
        else
        {
            return null;
        }  
    }

    private int GetIndex(string _ID)
    {
        foreach (GameObject go in availableTowers)
        {
            if (_ID == go.GetComponent<Tower>().ID)
            {
                return availableTowers.IndexOf(go);
            }
        }

        // if no GameObject found
        return -1;
    }

    private bool IsEnoughMoneyProvided(GameObject towerGo, int coins)
    {
        Tower tower = towerGo.GetComponent<Tower>();

        if (coins >= tower.Cost)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}

