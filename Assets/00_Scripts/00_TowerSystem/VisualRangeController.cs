using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System;

public class VisualRangeController : MonoBehaviour
{
    private Tower myTower;
    [SerializeField, Required]
    private GameObject visualRange;

    public static List<VisualRangeController> visualRanges = new List<VisualRangeController>();
       
    void Start()
    {
        myTower = GetComponent<Tower>();

        Vector3 RangeSize = new Vector3(myTower.fireRange, myTower.fireRange);
        visualRange.transform.localScale = RangeSize;

        if (visualRange != null)
        {
            RegisterVisualRange();
        }
    }
       
    private void OnDestroy()
    {
        UnregisterVisualRange();
    }

    private void UnregisterVisualRange()
    {
        visualRanges.Remove(this);
    }

    private void RegisterVisualRange()
    {
        visualRanges.Add(this);
    }

    public void ToggleVisualRange()
    {
        visualRange.SetActive(!visualRange.activeInHierarchy);
    }

    public void ShowRange()
    {
        visualRange.SetActive(true);
    }

    public void HideRange()
    {
        visualRange.SetActive(false);
    }

    [ResponsiveButtonGroup]
    private void SetRangeSize()
    {
        myTower = GetComponent<Tower>();

        Vector3 RangeSize = new Vector3(myTower.fireRange, myTower.fireRange);
        visualRange.transform.localScale = RangeSize;
    }

    public static bool isAnyRangeVisible()
    {
        bool value = false;

        foreach (VisualRangeController vr in visualRanges)
        {
            if (vr.visualRange.activeInHierarchy)
            {
                value = true;
            }
        }
        return value;
    }

}
