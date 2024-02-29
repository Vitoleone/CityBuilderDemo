using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [Header("Functional Buttons")]
    [SerializeField] public GameObject rotationLeftButton;
    [SerializeField] public GameObject rotationRightButton;
    [SerializeField] public GameObject movementButton;
    [SerializeField] public GameObject scaleUpButton;
    [SerializeField] public GameObject scaleDownButton;
    [SerializeField] public Button undoButton;

    [Header("Other UIs")]
    [SerializeField] public GameObject placementAlert;
    [SerializeField] public GameObject buildings;

    public bool allFunctionalActiveness = false;
    public Action checkButtonsActiveness;
    Color undoButtonColor;
    private void Start()
    {
        checkButtonsActiveness += CheckActiveness;
        undoButtonColor = undoButton.GetComponent<Image>().color;
        CheckActiveness();
    }


    public void SetFunctionalButtonsActivness(bool activeness)
    {
        Debug.Log("Buttonlar: " + activeness);
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
        }
        else
        {
           DeactivateUndoButton();
        }
    }
    public void DeactivateUndoButton()
    {
        undoButton.GetComponent<Image>().color = new Color(undoButtonColor.a, undoButtonColor.g, undoButtonColor.b, 0.3f);
        undoButton.interactable = false;
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
    public void ActivateMarkersButtons(bool canScale, bool canRotate, bool canMove)
    {
        scaleDownButton.SetActive(canScale);
        scaleUpButton.SetActive(canScale);
        rotationLeftButton.SetActive(canRotate);
        rotationRightButton.SetActive(canRotate);
        movementButton.SetActive(canMove);
    }
    private void OnDestroy()
    {
        checkButtonsActiveness -= CheckActiveness;
    }
}
