using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using Consts = UIConsts.Menu;

public class MenuUI : MonoBehaviour
{
    private VisualElement root, secondaryContainer, mainContainer;
    private Button startButton, shopButton, settingsButton, exitButton;


    private void Awake()
    {
        AssignVariables();
    }
    private void OnEnable()
    {
        startButton.clicked += OnStartClick;
        settingsButton.clicked += OnSettingClick;
        shopButton.clicked += OnShopClick;
        exitButton.clicked += OnExitClick;
    }

    private void Start()
    {
        StartCoroutine(DelayedShowMenu());
    }

    private void AssignVariables()
    {
        root = GetComponent<UIDocument>().rootVisualElement;

        secondaryContainer = root.Q<VisualElement>(Consts.SecondaryContainerName);
        mainContainer = root.Q<VisualElement>(Consts.MainContainerName);
        startButton = root.Q<Button>(Consts.StartButtonName);
        shopButton = root.Q<Button>(Consts.ShopButtonName);
        settingsButton = root.Q<Button>(Consts.SettingsButtonName);
        exitButton = root.Q<Button>(Consts.ExitButtonName);

        if (secondaryContainer == null || mainContainer == null || startButton == null || shopButton == null || settingsButton == null || exitButton == null)
            throw new InvalidOperationException("UI elements name is different the the ones defined in UIConsts");
    }



    private void OnStartClick()
    {
        SceneManager.LoadScene((int)Enums.SCENES.GAME);
    }

    private void OnShopClick()
    {
        throw new NotImplementedException();
    }

    private void OnSettingClick()
    {
        throw new NotImplementedException();
    }

    private void OnExitClick()
    {
        GameManager.instance.ExitGame();
    }

    private IEnumerator DelayedShowMenu()
    {
        yield return new WaitForSeconds(1f);
        secondaryContainer.RemoveFromClassList(Consts.SecondaryContainerHiddenClass);
        mainContainer.AddToClassList(Consts.TintClass);
    }
    private void OnDisable()
    {
        startButton.clicked -= OnStartClick;
        settingsButton.clicked -= OnSettingClick;
        shopButton.clicked -= OnShopClick;
        exitButton.clicked -= OnExitClick;
    }
}
