using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using static PlayerControll;

public class LabirinthManager : MonoBehaviour
{
    [SerializeField] GameObject CharacterUI;
    [SerializeField] GameObject charactersParent;
    [SerializeField] GameObject character;
    [SerializeField] PlayerControll playerControll;
    [SerializeField] int indexCharacter;

    [Space]
    [SerializeField] Texture2D characterTexture;
    [SerializeField] Material characterMaterial;

    [Space]
    [SerializeField] Transform pointStart;

    [Space]
    [SerializeField] bool canClk = true;

    [Space]
    [SerializeField] GameObject pointsParent;
    [SerializeField] List<GameObject> points = new List<GameObject>();
    public GameObject lastPoint;

    [Space]
    public bool ENDGame = false;
    [SerializeField] float speedAvto = 2;
    [SerializeField] float timerAvto = 70;

    private void Start()
    {
        indexCharacter = readIndexCharacter();
        onCharacter(indexCharacter);

        for (int i = 0; i < pointsParent.transform.childCount; i++)
        {
            //pointsParent.transform.GetChild(i).gameObject.AddComponent<PointScript>().LabirinthManager = this;
            points.Add(pointsParent.transform.GetChild(i).gameObject);
        }

        StartCoroutine(timer());
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
            CharacterUI.transform.GetChild(i).gameObject.SetActive(false);
            charactersParent.transform.GetChild(i).gameObject.SetActive(false);
        }

        CharacterUI.transform.GetChild(index).gameObject.SetActive(true);
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

    IEnumerator timer()
    {
        yield return null;
        float time = 0;

        while (time < timerAvto)
        {
            yield return null;
            time += Time.deltaTime;
        }

        if (ENDGame) yield break;

        StartCoroutine(trackPos());
    }


    IEnumerator trackPos()
    {
        PointScript ptScp = lastPoint.GetComponent<PointScript>();

        while (ptScp.NextPoint)
        {
            yield return null;

            Transform transNow = ptScp.transform;
            Transform transNext = ptScp.NextPoint.transform;
            Transform transChar = character.transform;

            float runTime = Vector3.Distance(transChar.position, transNow.position) / speedAvto;
            float time = 0;

            Vector3 startPos = transChar.position;
            Vector3 endPos = transNow.position;
            endPos.z = startPos.z;

            playerControll.kindMove = ptScp.KindMove;

            while (time < runTime)
            {
                yield return null;
                transChar.position = Vector3.Lerp(startPos, endPos, time / runTime);
                time += Time.deltaTime;
            }

            runTime = Vector3.Distance(transChar.position, transNext.position) / speedAvto;
            time = 0;

            startPos = transChar.position;
            endPos = transNext.position;
            endPos.z = startPos.z;

            while (time < runTime)
            {
                yield return null;
                transChar.position = Vector3.Lerp(startPos, endPos, time / runTime);
                time += Time.deltaTime;
            }

            transChar.position = endPos;
            lastPoint = ptScp.NextPoint.gameObject;

            ptScp = lastPoint.GetComponent<PointScript>();
        }

        playerControll.kindMove = KindMove.Stay;
    }
}
