using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[RequireComponent(typeof(Collider2D))]
public class TowerSpot : MonoBehaviour
{
    [SerializeField, ReadOnly]
    private GameObject currentTower;
    [SerializeField, Required]
    private TowerProvider towerProvider;

    private void OnMouseUp()
    {
        //Debug.Log("mouse click detected");
        PlaceTower();            
    }

    public void PlaceTower()
    {
        if (currentTower == null)
        {
            GameObject prefab = towerProvider.GetFirstTower(LevelManager.GetCoins());
            if (prefab == null)
            {
                return;
            }

            currentTower = Instantiate(prefab, gameObject.transform);
            PayTower(currentTower);
        }
        else
        {
            GameObject prefab = towerProvider.GetNextTower(currentTower.GetComponent<Tower>(),LevelManager.GetCoins());
            if (prefab == null)
            {
                return;
            }

            Destroy(currentTower);
                       
            currentTower = Instantiate(prefab, gameObject.transform);
            PayTower(currentTower);
        }
    }

    private void PayTower(GameObject towerToPay)
    {
        Tower tow = towerToPay.GetComponent<Tower>();
        LevelManager.IncreaseCoins(-tow.Cost);
    }

    #region Editor programming
    [SerializeField]
    private float gizmoRange = 3;

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, gizmoRange);
    }
    #endregion
}
