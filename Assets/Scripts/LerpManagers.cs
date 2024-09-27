using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LerpManagers : MonoBehaviour
{
    [SerializeField] Image m_Image;
    [SerializeField, Range(0, 5)] float timeChange = 1;
    [SerializeField, Range(0, 15)] float timeDealy = 5;
    [SerializeField, Range(0, 15)] float timeWait = 5;
    [SerializeField] KindStart kindStart = KindStart.Default;

    private void Start()
    {
        if (m_Image == null)
            m_Image = GetComponent<Image>();

        Color end = m_Image.color;
        end.a = 0;

        if (kindStart == KindStart.Default)
        {
            StartCoroutine(changeColor(m_Image, end, timeChange, timeDealy));
        }
        else if (kindStart == KindStart.New)
        {
            StartCoroutine(changeColorNew(m_Image, end, timeChange, timeDealy, timeWait));
        }
    }

    IEnumerator changeColorNew(Image img, Color colorEnd, float timeChange, float timeDelay = 0, float timeWait = 0)
    {
        float time = 0;
        Color colorStart = img.color;

        yield return new WaitForSeconds(timeDelay);

        while (time < timeWait)
        {
            yield return null;
            if (Input.touchCount != 0 || Input.anyKeyDown)
            {
                Debug.Log($"<color=red>Touch</color>");
                time = timeWait;
            }
            time += Time.deltaTime;
        }

        time = 0;

        while (time < timeChange)
        {
            yield return null;
            img.color = Color.Lerp(colorStart, colorEnd, time / timeChange);
            time += Time.deltaTime;
        }

        img.raycastTarget = false;
        img.color = colorEnd;
    }
    IEnumerator changeColor(Image img, Color colorEnd, float timeChange, float timeDelay = 0)
    {
        float time = 0;
        Color colorStart = img.color;

        yield return new WaitForSeconds(timeDelay);

        while (time < timeChange)
        {
            yield return null;
            img.color = Color.Lerp(colorStart,colorEnd, time/timeChange);
            time += Time.deltaTime;
        }

        img.raycastTarget = false;
        img.color = colorEnd;
    }
    
    enum KindStart
    {
        Default = 0,
        New = 1,
    }
    
}
