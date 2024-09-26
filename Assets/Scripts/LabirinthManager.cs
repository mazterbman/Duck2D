using System.IO;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using static PlayerControll;

public class LabirinthManager : MonoBehaviour
{
    [SerializeField] GameObject charactersParent;
    [SerializeField] GameObject character;
    [SerializeField] PlayerControll playerControll;
    [SerializeField] int indexCharacter;

    [Space]
    [SerializeField] Texture2D characterTexture;
    [SerializeField] Material characterMaterial;

    [Space]
    [SerializeField] Transform pointStart;
    [SerializeField] Transform pointEnd;

    [Space]
    [SerializeField] bool canClk = true;

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
        playerControll = character.GetComponent<PlayerControll>();
        setTextureOnMaterial();

        character.SetActive(true);
    }


    public void MoveUp()
    {
        MoveCharacter(KindMove.Up);
    }
    public void MoveDown()
    {
        MoveCharacter(KindMove.Down);
    }
    public void MoveLeft()
    {
        MoveCharacter(KindMove.Left);
    }
    public void MoveRight()
    {
        MoveCharacter(KindMove.Right);
    }
    void MoveCharacter(KindMove kindMove)
    {
        if (!canClk) return;

        playerControll.kindMove = kindMove;
        canClk = false;
    }

    public void StopCharacter()
    {
        if (canClk) return;
        canClk = true;

        playerControll.kindMove = KindMove.Stay;
    }
}
