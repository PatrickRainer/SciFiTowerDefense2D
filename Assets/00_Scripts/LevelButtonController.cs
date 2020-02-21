using UnityEngine;
using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine.UI;
using Doozy.Engine.UI;
using System;

public class LevelButtonController : MonoBehaviour
{
    [TitleGroup("References")]
    [SerializeField, Required]
    private Image star1;
    [SerializeField, Required]
    private Image star2;
    [SerializeField, Required]
    private Image star3;

    [TitleGroup("Settings")]
    [SerializeField, InlineButton("GetButtonName")]
    private string levelName;
    [SerializeField]
    private Color starNotActiveColor;
    [SerializeField]
    private Color starActiveColor;

    private Button myButton;
    private UIButton myDoozyButton;

    private void Awake()
    {
        myButton = GetComponentInParent<Button>();
        myDoozyButton = GetComponentInParent<UIButton>();
        star1.color = starNotActiveColor;
        star2.color = starNotActiveColor;
        star3.color = starNotActiveColor;
    }

    private void Start()
    {
        SetButtonActivity();
        SetStars();
    }
       
    private void SetButtonActivity()
    {
        if (LevelSaveLoadController.IsLevelLocked(levelName))
        {
            myDoozyButton.Interactable = false;
        }
        else
        {
            myDoozyButton.Interactable = true;
        }
    }
       
    private void SetStars()
    {
        int _stars = LevelSaveLoadController.GetStarsAchieved(levelName);

        switch (_stars)
        {   case 1:
                star1.color = starActiveColor;
                break;
            case 2:
                star1.color = starActiveColor;
                star2.color = starActiveColor;
                break;
            case 3:
                star1.color = starActiveColor;
                star2.color = starActiveColor;
                star3.color = starActiveColor;
                break;
            default:
                break;
        }

    }

    private void GetButtonName()
    {
        levelName = GetComponentInParent<UIButton>().ButtonName;
    }

}
