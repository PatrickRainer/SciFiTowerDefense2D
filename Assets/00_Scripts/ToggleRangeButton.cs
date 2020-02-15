using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleRangeButton : MonoBehaviour
{
    public void ToggleAllRanges()
    {
        VisualRangeController[] vrcs = GameObject.FindObjectsOfType<VisualRangeController>();

        foreach (VisualRangeController vrc in vrcs)
        {
            vrc.ToggleVisualRange();
        }
    }
}
