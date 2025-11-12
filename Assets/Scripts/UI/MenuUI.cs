using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using static UIConsts.Menu;

public class MenuUI : MonoBehaviour
{
    private VisualElement root, menuContainer, origamiManImage, shopContainer, settingsContainer;
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

        menuContainer = root.Q<VisualElement>(MenuContainerName);
        origamiManImage = root.Q<VisualElement>(OrigamiManImageName);
        shopContainer = root.Q<VisualElement>(ShopContainerName);
        settingsContainer = root.Q<VisualElement>(SettingsContainerName);
        startButton = root.Q<Button>(StartButtonName);
        shopButton = root.Q<Button>(ShopButtonName);
        settingsButton = root.Q<Button>(SettingsButtonName);
        exitButton = root.Q<Button>(ExitButtonName);
        shop = root.Q<Shop>(ShopName);
    }



    private void OnStartClick()
    {
        SceneManager.LoadScene((int)Enums.SCENES.GAME);
    }

    private void OnShopClick()
    {
        HideMenu();
        shopContainer.RemoveFromClassList(ShopHiddenClass);
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
        ShowMenu();
        TintBackground();
    }

    private void ShowMenu()
    {
        menuContainer.RemoveFromClassList(MenuContainerHiddenClass);
        origamiManImage.RemoveFromClassList(OrigamiManHiddenClass);
    }

    private void HideMenu()
    {
        menuContainer.AddToClassList(MenuContainerHiddenClass);
        origamiManImage.AddToClassList(OrigamiManHiddenClass);
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
