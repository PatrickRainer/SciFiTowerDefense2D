using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class VisualRangeController : MonoBehaviour
{
    private Tower myTower;
    [SerializeField, Required]
    private GameObject visualRange;
       
    void Start()
    {
        myTower = GetComponent<Tower>();

        Vector3 RangeSize = new Vector3(myTower.fireRange, myTower.fireRange);
        visualRange.transform.localScale = RangeSize;
    }

    public void ToggleVisualRange()
    {
        visualRange.SetActive(!visualRange.activeInHierarchy);
    }

    [ResponsiveButtonGroup]
    private void SetRangeSize()
    {
        myTower = GetComponent<Tower>();

        Vector3 RangeSize = new Vector3(myTower.fireRange, myTower.fireRange);
        visualRange.transform.localScale = RangeSize;
    }
}
