using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using consts = Consts.Menu;

public class MenuUI : MonoBehaviour
{
    public static event Action onButtonClick;
    private VisualElement root;
    private VisualElement secondaryContainer;
    private VisualElement mainContainer;
    private Button startButton;
    private List<Button> allButtons;


    private void Awake()
    {
        root = GetComponent<UIDocument>().rootVisualElement;

        secondaryContainer = root.Q<VisualElement>(consts.SecondaryContainerName);
        mainContainer = root.Q<VisualElement>(consts.MainContainerName);
        startButton = root.Q<Button>(consts.StartButtonName);
        allButtons = root.Query<Button>(className: consts.ButtonClass).ToList();
    }
    private void OnEnable()
    {
        startButton.clicked += OnStartClick;
        foreach (Button button in allButtons)
        {
            button.clicked += OnButtonClick;
        }
    }
    private void Start()
    {
        StartCoroutine(DelayedShowMenu());
    }

    private void OnButtonClick()
    {
        onButtonClick?.Invoke();
    }

    private void OnStartClick()
    {
        SceneManager.LoadScene((int)Enums.SCENES.GAME);
    }

    private IEnumerator DelayedShowMenu()
    {
        yield return new WaitForSeconds(1f);
        secondaryContainer.RemoveFromClassList(consts.SecondaryContainerHiddenClass);
        mainContainer.AddToClassList(consts.TintClass);
    }
    private void OnDisable()
    {
        startButton.clicked -= OnStartClick;
        foreach (Button button in allButtons)
        {
            button.clicked -= OnButtonClick;
        }
    }
}
