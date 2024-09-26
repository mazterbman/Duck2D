using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonControll : UIManager
{
    [Space,Header("Components For ButtonControll")]
    [SerializeField] string nameScene = "MainMenuDuck2D";
    [SerializeField] SysButton sysButton;

    [Space]
    [SerializeField] string SceneName = "ColoringBook";

    [Space]
    [SerializeField] string nameForPreff = "CharacterSelected";
    [SerializeField] int indexButton;
    public void OnClk()
    {
        saveToPref(sysButton);
        SceneManager.LoadScene(nameScene,LoadSceneMode.Single);
    }

    public void savePreff()
    {
        PlayerPrefs.SetInt(nameForPreff, indexButton);
        Debug.Log($"PlayerPrefSaved  =  {indexButton}");
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneName);
    }

}
