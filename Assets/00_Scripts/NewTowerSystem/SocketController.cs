using UnityEngine;
using System.Collections;
using Sirenix.OdinInspector;


[System.Serializable]
public class SocketData
{

}

[RequireComponent(typeof(Collider2D))]
public class SocketController : MonoBehaviour
{
    [SerializeField]
    private SocketUIController UIController;
    [SerializeField,Required]
    private LaserTowerProvider laserProvider;
    [SerializeField, Required]
    private AtomTowerProvider atomProvider;
    [SerializeField, Required]
    private FlameTowerProvider flameProvider;

    [ReadOnly]
    public GameObject currentTower = null;

    private void OnMouseUp()
    {
        UIController.ShowMenu();
    }

    public void PlaceLaserTower()
    {
        GameObject tw = laserProvider.GetTower(0, LevelManager.GetCoins());

        if (tw != null)
        {
            currentTower = Instantiate(tw, transform);
            PayTower(currentTower);
        }
    }

    public void PlaceAtomTower()
    {
        GameObject tw = atomProvider.GetTower(0, LevelManager.GetCoins());

        if (tw != null)
        {
            currentTower = Instantiate(tw, transform);
            PayTower(currentTower);
        }
    }

    public void PlaceFlameTower()
    {
        GameObject tw = flameProvider.GetTower(0, LevelManager.GetCoins());

        if (tw != null)
        {
            currentTower = Instantiate(tw, transform);
            PayTower(currentTower);
        }
    }

    public void UpgradeTower()
    {
        Tower tw = currentTower.GetComponent<Tower>();
        
        switch (tw.type)
        {
            case TowerTypes.Laser:
                GameObject newTW = laserProvider.GetUpgrade(tw, LevelManager.GetCoins());
                Destroy(currentTower);
                currentTower = Instantiate(newTW,transform);
                break;
            case TowerTypes.Flamer:
                newTW = flameProvider.GetUpgrade(tw, LevelManager.GetCoins());
                Destroy(currentTower);
                currentTower = Instantiate(newTW, transform);
                break;
            case TowerTypes.Atom:
                newTW = atomProvider.GetUpgrade(tw, LevelManager.GetCoins());
                Destroy(currentTower);
                currentTower = Instantiate(newTW, transform);
                break;
            default:
                break;
        }
    }

    private void PayTower(GameObject towerToPay)
    {
        Tower tow = towerToPay.GetComponent<Tower>();
        LevelManager.IncreaseCoins(-tow.Cost);
    }
}
