using UnityEditor;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject MainMenuPanel;
    [SerializeField] GameObject ChoosenCharactersPanel;

    [Space]
    public SysButton sysButtonCh = SysButton.None;
    public bool children = false;

    private void Awake()
    {
        if (children) return;

        onMainMenu();
    }

    private void Start()
    {
        if (children) return;

        sysButtonCh = loadFromPref();

        switch (sysButtonCh)
        {
            case SysButton.None:
                break;

            case SysButton.MainMenu:
                onMainMenu();
                break;

            case SysButton.ChoosenChar:
                onChoosenCharacters();
                break;
        }

        sysButtonCh = SysButton.None;
        saveToPref(sysButtonCh);
    }

    public void saveToPref(SysButton sysButton)
    {
        int saveInt = (int)sysButton;

        PlayerPrefs.SetInt("SysButton",saveInt);
        PlayerPrefs.Save();
    }
    public SysButton loadFromPref()
    {
        return (SysButton)PlayerPrefs.GetInt("SysButton", 0);
    }

    void onMainMenu()
    {
        if (MainMenuPanel != null) MainMenuPanel.SetActive(true);
        if (ChoosenCharactersPanel != null) ChoosenCharactersPanel.SetActive(false);
    }
    void onChoosenCharacters()
    {
        MainMenuPanel.SetActive(false);
        ChoosenCharactersPanel.SetActive(true);
    }

    public enum SysButton
    {
        None = 0,
        MainMenu = 1,
        ChoosenChar = 2,
    }
}
