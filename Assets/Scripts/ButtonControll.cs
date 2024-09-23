using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonControll : UIManager
{
    [Space,Header("Components For ButtonControll")]
    [SerializeField] string nameScene = "MainMenuDuck2D";
    [SerializeField] SysButton sysButton;

    public void OnClk()
    {
        saveToPref(sysButton);
        SceneManager.LoadScene(nameScene,LoadSceneMode.Single);
    }
}
