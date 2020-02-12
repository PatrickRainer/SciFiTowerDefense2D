using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class SoundManager : MonoBehaviour
{

    public static void MuteAllSound()
    {
        AudioListener.pause = true;
    }

    public static void UnMuteAllSound()
    {
        AudioListener.pause = false;
    }
}
