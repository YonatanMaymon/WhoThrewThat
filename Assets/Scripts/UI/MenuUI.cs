using System;
using System.Collections;
using System.IO;
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
        foreach (var item in shop.items)
        {
            item.SubscribeToUpgradeClick(OnUpgradeClick);
        }
        shop.SubscribeToBackClick(HideShop);
    }

    private void Start()
    {
        DataManager dataManager = DataManager.instance;
        shop.UpdateShop(dataManager.statsLevels, dataManager.coinAmount);

        StartCoroutine(DelayedShowMenu(1));
    }

    private void OnUpgradeClick(ShopItem item)
    {
        DataManager dataManager = DataManager.instance;
        if (dataManager == null)
            throw new FileLoadException("DataManager not Loaded for some reason");
        dataManager.UpgradeStat(item);
        shop.UpdateShop(dataManager.statsLevels, dataManager.coinAmount);
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
    private void HideShop()
    {
        shopContainer.AddToClassList(ShopHiddenClass);
        StartCoroutine(DelayedShowMenu(0.3f));
    }

    private void OnSettingClick()
    {
        throw new NotImplementedException();
    }

    private void OnExitClick()
    {
        GameManager.instance.ExitGame();
    }

    private IEnumerator DelayedShowMenu(float sec)
    {
        yield return new WaitForSeconds(sec);
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
        if (!mainContainer.ClassListContains(TintClass))
            mainContainer.AddToClassList(TintClass);
    }
    private void OnDisable()
    {
        startButton.clicked -= OnStartClick;
        settingsButton.clicked -= OnSettingClick;
        shopButton.clicked -= OnShopClick;
        exitButton.clicked -= OnExitClick;
        foreach (var item in shop.items)
        {
            item.UnsubscribeFromUpgradeClick();
        }
        shop.UnsubscribeFromBackClick(HideShop);
    }
}
