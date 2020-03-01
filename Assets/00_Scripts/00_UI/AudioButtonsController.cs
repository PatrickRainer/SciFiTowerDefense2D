using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.UI;
using System;
using UnityEngine.Events;

public class AudioButtonsController : MonoBehaviour
{
    private Button myButton;

    private void Awake()
    {
        myButton = GetComponent<Button>();
    }

    public void Mute()
    {
        AudioListener.pause = true;
    }

    public void UnMute()
    {
        AudioListener.pause = false;
    }

    private void OnDisable()
    {
        myButton.onClick.RemoveAllListeners();
    }


}
