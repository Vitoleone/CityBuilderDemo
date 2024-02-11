using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] public GameObject functionalPanel;
    [SerializeField] public SpriteRenderer undoButtonSprite;
    [SerializeField] public SpriteRenderer redoButtonSprite;

    private void Start()
    {
        SelectManager.instance.onSelectUnit += OpenFunctionalPanel;
    }
    private void OnDestroy()
    {
        SelectManager.instance.onSelectUnit -= OpenFunctionalPanel;
    }

    public void OpenFunctionalPanel(bool activeness)
    {
        functionalPanel.SetActive(activeness);
    }
    public void CheckActiveness()
    {
        //will check redo and undo stacks count if count <= 0 then decrease opacity of button
    }
}
