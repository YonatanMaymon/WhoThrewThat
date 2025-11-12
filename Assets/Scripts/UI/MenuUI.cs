using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using static UIConsts.Menu;

public class MenuUI : MonoBehaviour
{
    private VisualElement root, MenuContainer;
    private Button startButton, shopButton, settingsButton, exitButton;
    private Shop shop;


    private void Awake()
    {
        AssignVariables();
        shop.LoadItems();
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

        MenuContainer = root.Q<VisualElement>(MenuContainerName);
        startButton = root.Q<Button>(StartButtonName);
        shopButton = root.Q<Button>(ShopButtonName);
        settingsButton = root.Q<Button>(SettingsButtonName);
        exitButton = root.Q<Button>(ExitButtonName);
        shop = root.Q<Shop>(ShopName);

        if (MenuContainer == null || startButton == null || shopButton == null || settingsButton == null || exitButton == null)
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
        MenuContainer.RemoveFromClassList(MenuContainerHiddenClass);
        TintBackground();
    }
    private void TintBackground()
    {
        VisualElement mainContainer = root.Q<VisualElement>(MainContainerName);
        mainContainer.AddToClassList(TintClass);
    }
    private void OnDisable()
    {
        startButton.clicked -= OnStartClick;
        settingsButton.clicked -= OnSettingClick;
        shopButton.clicked -= OnShopClick;
        exitButton.clicked -= OnExitClick;
    }
}
