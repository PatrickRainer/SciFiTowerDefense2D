using UnityEngine;
using System.Collections;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using System;

[CreateAssetMenu]
public class TowerProviderV2 : ScriptableObject
{
    [SerializeField, AssetsOnly]
    private List<GameObject> laserTowers = new List<GameObject>();
    [SerializeField, AssetsOnly]
    private List<GameObject> plasmaTowers = new List<GameObject>();
    [SerializeField, AssetsOnly]
    private List<GameObject> atomTowers = new List<GameObject>();

    public GameObject GetTower(TowerTypes type, int upgradeLevel, int coins)
    {
        switch (type)
        {
            case TowerTypes.Laser:
                if (IsEnoughMoneyProvided(laserTowers[upgradeLevel], coins))
                {
                    return laserTowers[upgradeLevel];
                }
                else
                {
                    return null;
                }

            case TowerTypes.Plasma:
                if (IsEnoughMoneyProvided(plasmaTowers[upgradeLevel], coins))
                {
                    return plasmaTowers[upgradeLevel];
                }
                else
                {
                    return null;
                }

            case TowerTypes.Atom:
                if (IsEnoughMoneyProvided(atomTowers[upgradeLevel], coins))
                {
                    return atomTowers[upgradeLevel];
                }
                else
                {
                    return null;
                }

            default:
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

        switch (type)
        {
            case TowerTypes.Laser:
                if (IsEnoughMoneyProvided(laserTowers[currentTower.upgradeLevel+1], coins))
                {
                    return laserTowers[currentTower.upgradeLevel + 1];
                }
                else
                {
                    return null;
                }
            case TowerTypes.Plasma:
                if (IsEnoughMoneyProvided(plasmaTowers[currentTower.upgradeLevel + 1], coins))
                {
                    return plasmaTowers[currentTower.upgradeLevel + 1];
                }
                else
                {
                    return null;
                }
            case TowerTypes.Atom:
                if (IsEnoughMoneyProvided(atomTowers[currentTower.upgradeLevel + 1], coins))
                {
                    return atomTowers[currentTower.upgradeLevel + 1];
                }
                else
                {
                    return null;
                }
            default:
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
            switch (tower.type)
            {
                case TowerTypes.Laser:
                    if (laserTowers.Count <= tower.upgradeLevel + 1)
                    {
                        return false;
                    }
                    if (laserTowers[tower.upgradeLevel + 1] != null)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }

            case TowerTypes.Plasma:
                if (plasmaTowers.Count <= tower.upgradeLevel + 1)
                {
                    return false;
                }
                if (plasmaTowers[tower.upgradeLevel + 1] != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            case TowerTypes.Atom:
                if (atomTowers.Count <= tower.upgradeLevel + 1)
                {
                    return false;
                }
                if (atomTowers[tower.upgradeLevel + 1] != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            default:
                    return false;
            }
    }

    
    [ResponsiveButtonGroup("Testing")]
    private void TestIsUpgradeAvailaible()
    {
        GameObject go = GetTower(TowerTypes.Atom, 2, 500);
        Debug.Log(IsUpgradeAvailable(go.GetComponent<Tower>()));
    }

    [ResponsiveButtonGroup("Testing")]
    private void TestUpgradeTower()
    {
        GameObject go = GetTower(TowerTypes.Atom, 1, 500);
        GameObject upgrade = GetUpgrade(go.GetComponent<Tower>(), 500);

        Debug.Log(upgrade.GetComponent<Tower>().title);
    }
}

