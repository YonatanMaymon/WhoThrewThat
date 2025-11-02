using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using consts = Consts.Menu;

public class Menu : MonoBehaviour
{
    private VisualElement root;
    private VisualElement secondaryContainer;
    private VisualElement mainContainer;
    private Button startButton;


    private void Awake()
    {
        root = GetComponent<UIDocument>().rootVisualElement;

        secondaryContainer = root.Q<VisualElement>(consts.SecondaryContainerName);
        mainContainer = root.Q<VisualElement>(consts.MainContainerName);
        startButton = root.Q<Button>(consts.StartButtonName);
    }
    private void OnEnable()
    {
        startButton.clicked += OnStartClick;
    }

    private void OnStartClick()
    {
        SceneManager.LoadScene((int)Enums.SCENES.GAME);
    }

    private void Start()
    {
        StartCoroutine(DelayedShowMenu());
    }

    private IEnumerator DelayedShowMenu()
    {
        yield return new WaitForSeconds(1f);
        secondaryContainer.RemoveFromClassList(consts.SecondaryContainerHiddenClass);
        mainContainer.AddToClassList(consts.TintClass);
    }
}
