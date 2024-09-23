using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimerControll : UIManager
{
    public static TimerControll instance;

    [Space, Header("Components For TimerControll")]
    [SerializeField] string nameForPreff;
    [SerializeField] int valueFromPreff;
    [SerializeField] int defaultValue = 60;
    [SerializeField] int TMPVal;

    [Space]
    bool wasChanged = false;

    [Space]
    [SerializeField] string nameScene = "MainMenuDuck2D";
    [SerializeField] SysButton sysButton;

    Coroutine timerCor;

    new int loadFromPref()
    {
        return PlayerPrefs.GetInt(nameForPreff,defaultValue);
    }
    void saveToPref()
    {
        PlayerPrefs.SetInt(nameForPreff,valueFromPreff);
    }

    private void Update()
    {
        if (Input.touchCount != 0 || Input.anyKeyDown)
        {
            Debug.Log("<color=green>Touch</color>");
            wasChanged = true;
        }
    }

    private void Awake()
    {
        if (!instance) instance = this;
        else 
        { 
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
        valueFromPreff = loadFromPref(); 
    }

    private void Start()
    {
        timerCor = StartCoroutine(startTimer());
    }

    IEnumerator startTimer()
    {
        float time = 0;

        while (time < valueFromPreff) 
        {
            yield return null;
            time += Time.deltaTime;
            TMPVal = (int)time;

            if (wasChanged) 
            { 
                time = 0; 
                wasChanged = false; 
            }
        }

        saveToPref(sysButton);
        SceneManager.LoadScene(nameScene, LoadSceneMode.Single);
        timerCor = StartCoroutine(startTimer());
        yield break;
    }

    public void changeValueTimer(int value)
    {
        StopCoroutine(timerCor);
        timerCor = null;

        valueFromPreff = value;
        saveToPref();
        timerCor = StartCoroutine(startTimer());
    }
}
