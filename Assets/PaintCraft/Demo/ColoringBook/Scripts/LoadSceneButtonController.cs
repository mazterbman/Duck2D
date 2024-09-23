using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace PaintCraft.Demo.ColoringBook
{
    public class LoadSceneButtonController : MonoBehaviour
    {
        [SerializeField] string NameScene = "PageSelect";
        private void Start()
        {
            GetComponent<Button>().onClick.AddListener(
                () =>
                {
                    SceneManager.LoadScene(NameScene);
                });
        }
    }
}