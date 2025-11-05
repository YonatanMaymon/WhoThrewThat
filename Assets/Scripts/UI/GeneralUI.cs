using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GeneralUI : MonoBehaviour
{
    public static event Action onButtonClick;
    protected VisualElement root;
    private List<Button> allButtons;

    private void Awake()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        allButtons = root.Query<Button>(className: UIConsts.General.ButtonClass).ToList();

    }

    private void OnEnable()
    {
        foreach (Button button in allButtons)
        {
            button.clicked += OnButtonClick;
        }
    }

    private void OnButtonClick()
    {
        onButtonClick?.Invoke();
    }
    private void OnDisable()
    {
        foreach (Button button in allButtons)
        {
            button.clicked -= OnButtonClick;
        }
    }
}
