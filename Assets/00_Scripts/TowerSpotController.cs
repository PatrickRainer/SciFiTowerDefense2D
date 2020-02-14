using UnityEngine;
using System.Collections;
using Sirenix.OdinInspector;

//[RequireComponent(typeof(Collider2D))]
public class TowerSpotController : MonoBehaviour
{
    [ReadOnly]
    public GameObject currentTower;
    [SerializeField, Required]
    private TowerProviderV2 towerProvider;

    private GameObject myTowerSpotMenu;
    private GameObject myUpgradeMenu;

    public void PlaceLaserTower()
    {
        if (currentTower == null)
        {
            GameObject prefab = towerProvider.GetTower(TowerTypes.Laser, 0, LevelManager.GetCoins());
            if (prefab == null)
            {
                return;
            }

            currentTower = Instantiate(prefab, gameObject.transform);
            PayTower(currentTower);
        }
        else
        {
            Tower currentTowerData = currentTower.GetComponent<Tower>();

            GameObject prefab = towerProvider.GetTower(TowerTypes.Laser, currentTowerData.upgradeLevel + 1, LevelManager.GetCoins());
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
}
