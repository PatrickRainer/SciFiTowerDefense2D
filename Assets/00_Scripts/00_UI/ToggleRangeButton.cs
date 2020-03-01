using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleRangeButton : MonoBehaviour
{
    public void ToggleAllRanges()
    {
        if (VisualRangeController.isAnyRangeVisible())
        {
            foreach (VisualRangeController vrc in VisualRangeController.visualRanges)
            {
                vrc.HideRange();
            }
        }
        else
        {
            foreach (VisualRangeController vrc in VisualRangeController.visualRanges)
            {
                vrc.ShowRange();
            }
        }
    }
}
