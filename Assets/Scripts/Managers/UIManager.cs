using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] public GameObject rotationLeftButton;
    [SerializeField] public GameObject rotationRightButton;
    [SerializeField] public GameObject movementButton;
    [SerializeField] public GameObject scaleUpButton;
    [SerializeField] public GameObject scaleDownButton;
    [SerializeField] public Button undoButton;
    [SerializeField] public Button redoButton;
    [SerializeField] public GameObject placementAlert;
    [SerializeField] public GameObject buildings;
    public bool allFunctionalActiveness = false;
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
        rotationLeftButton.SetActive(activeness);
        rotationRightButton.SetActive(activeness);
        movementButton.SetActive(activeness);
        scaleUpButton.SetActive(activeness);
        scaleDownButton.SetActive(activeness);
        allFunctionalActiveness = activeness;
    }
    public void CheckActiveness()
    {
        if (CommandScheduler.commands.Count > 0)
        {
            ActiveButton(undoButton,undoButtonColor);
            ActiveButton(redoButton, redoButtonColor);

        }
        else
        {
           DeactiveButton(undoButton, undoButtonColor);
            DeactiveButton(redoButton, redoButtonColor);
        }

    }
    public void ControlPlacementAlertActiveness(bool activeState)
    {
        placementAlert.SetActive(activeState);
    }
    public void ShowOnlyRotationAndScaleButtons(bool activeness)
    {
        rotationLeftButton.SetActive(activeness);
        rotationRightButton.SetActive(activeness);
        scaleDownButton.SetActive(activeness);
        scaleUpButton.SetActive(activeness);
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
