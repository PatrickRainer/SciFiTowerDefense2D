using UnityEngine;
using System.Collections;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using System;

public abstract class TowerProvider : ScriptableObject
{
    [SerializeField, AssetsOnly]
    protected List<GameObject> towers = new List<GameObject>();

    public GameObject GetTower(int upgradeLevel, int coins)
    {
        if (IsEnoughMoneyProvided(towers[upgradeLevel], coins))
        {
            return towers[upgradeLevel];
        }
        else
        {
            return null;
        }
    }

    public GameObject GetUpgrade(Tower currentTower, int coins)
    {
        TowerTypes type = currentTower.type;

        if (!IsUpgradeAvailable(currentTower))
        {
            return null;
        }

        if (IsEnoughMoneyProvided(towers[currentTower.upgradeLevel+1], coins))
        {
            return towers[currentTower.upgradeLevel + 1];
        }
        else
        {
            return null;
        }
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

    private bool IsUpgradeAvailable(Tower tower)
    {
                    if (towers.Count <= tower.upgradeLevel + 1)
                    {
                        return false;
                    }
                    if (towers[tower.upgradeLevel + 1] != null)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
    }

    
    [ResponsiveButtonGroup("Testing")]
    private void TestIsUpgradeAvailaible()
    {
        GameObject go = GetTower(2, 500);
        Debug.Log(IsUpgradeAvailable(go.GetComponent<Tower>()));
    }

    [ResponsiveButtonGroup("Testing")]
    private void TestUpgradeTower()
    {
        GameObject go = GetTower(1, 500);
        GameObject upgrade = GetUpgrade(go.GetComponent<Tower>(), 500);

        Debug.Log(upgrade.GetComponent<Tower>().title);
    }
}

