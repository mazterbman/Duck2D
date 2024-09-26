using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    [SerializeField] string tagPlayer = "Player";
    [SerializeField] GameObject FireWork;
    [SerializeField] GameObject BlockClk;

    [Space]
    [SerializeField] string SceneName = "MainMenuDuck2D";
    [SerializeField, Range(0, 5f)] float delay;

    private void Awake()
    {
        FireWork.SetActive(false);
        BlockClk.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == tagPlayer)
        {
            Debug.Log("EndGame");
            FireWork.SetActive(true);
            BlockClk.SetActive(true);
            StartCoroutine(nextSceneDelay());
        }
    }

    IEnumerator nextSceneDelay()
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(SceneName, LoadSceneMode.Single);
    }
}
