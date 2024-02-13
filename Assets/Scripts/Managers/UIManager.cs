using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] public GameObject functionalPanel;
    [SerializeField] public Button undoButton;
    [SerializeField] public Button redoButton;
    public Action checkButtonsActiveness;
    Color undoButtonColor, redoButtonColor;
    private void Start()
    {
        //Events
        SelectManager.instance.onSelectUnit += SetFunctionalButtonsActivness;
        checkButtonsActiveness += CheckActiveness;

        undoButtonColor = undoButton.GetComponent<Image>().color;
        redoButtonColor = redoButton.GetComponent<Image>().color;
        CheckActiveness();
    }
    private void OnDestroy()
    {
        SelectManager.instance.onSelectUnit -= SetFunctionalButtonsActivness;
        checkButtonsActiveness -= CheckActiveness;
    }

    public void SetFunctionalButtonsActivness(bool activeness)
    {
        functionalPanel.SetActive(activeness);
    }
    public void CheckActiveness()
    {
        Debug.Log(CommandScheduler.commands.Count);
        if (CommandScheduler.commands.Count > 0)
        {
            ActiveButton(undoButton,undoButtonColor);
            
        }
        else
        {
           DeactiveButton(undoButton, undoButtonColor);
        }
        if(CommandScheduler.commands.Count > 0)
        {
            ActiveButton(redoButton,redoButtonColor);
        }
        else
        {
            DeactiveButton(redoButton,redoButtonColor);
        }

    }

    void ActiveButton(Button button, Color Color)
    {
        button.GetComponent<Image>().color = new Color(Color.a, Color.g, Color.b, 1f);
        button.interactable = true;
    }
    void DeactiveButton(Button button, Color Color)
    {
        button.GetComponent<Image>().color = new Color(Color.a, Color.g, Color.b, 0.3f);
        button.interactable = false;
    }
}
