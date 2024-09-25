using System.IO;
using UnityEngine;
using UnityEngine.Rendering;

public class LabirinthManager : MonoBehaviour
{
    [SerializeField] GameObject charactersParent;
    [SerializeField] GameObject character;
    [SerializeField] int indexCharacter;

    [Space]
    [SerializeField] Texture2D characterTexture;
    [SerializeField] Material characterMaterial;

    [Space]
    [SerializeField] Transform pointStart;
    [SerializeField] Transform pointEnd;

    private void Start()
    {
        indexCharacter = readIndexCharacter();
        onCharacter(indexCharacter);
    }

    private int readIndexCharacter()
    {
        return PlayerPrefs.GetInt("CharacterSelected", 0);
    }

    private void setTextureOnMaterial()
    {
        byte[] bytes = File.ReadAllBytes(Application.streamingAssetsPath + "/SavedScreen.png");

        characterTexture = new Texture2D(1,1);
        characterTexture.LoadImage(bytes);
        characterTexture.Apply();

        characterMaterial.mainTexture = characterTexture;
    }

    private void onCharacter(int index)
    {
        for (int i = 0; i < charactersParent.transform.childCount; i++) 
        {
            charactersParent.transform.GetChild(i).gameObject.SetActive(false);
        }

        character = charactersParent.transform.GetChild(index).gameObject;
        character.transform.position = pointStart.position;
        setTextureOnMaterial();

        character.SetActive(true);
    }
}
