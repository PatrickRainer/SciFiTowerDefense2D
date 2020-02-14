using UnityEngine;
using System.Collections;
using Sirenix.OdinInspector;
using System.Collections.Generic;

public enum TowerTypes { Laser, Plasma, Atom}

[CreateAssetMenu]
public class TowerProviderV2 : ScriptableObject
{
    [SerializeField]
    private List<GameObject> laserTowers = new List<GameObject>();
    [SerializeField]
    private List<GameObject> plasmaTowers = new List<GameObject>();
    [SerializeField]
    private List<GameObject> atomTowers = new List<GameObject>();

    public GameObject GetTower(TowerTypes type, int upgradeLevel, int coins)
    {
        switch (type)
        {
            case TowerTypes.Laser:
                if (IsEnoughMoneyProvided(laserTowers[upgradeLevel],coins))
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
