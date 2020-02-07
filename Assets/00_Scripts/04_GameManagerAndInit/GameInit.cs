using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameInit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DG.Tweening.DOTween.Init();
        SceneManager.LoadScene(1);
    }

}
