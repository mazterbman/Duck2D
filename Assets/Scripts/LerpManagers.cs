using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LerpManagers : MonoBehaviour
{
    [SerializeField] Image m_Image;

    private void Start()
    {
        if (m_Image == null)
            m_Image = GetComponent<Image>();

        Color end = m_Image.color;
        end.a = 0;

        StartCoroutine(changeColor(m_Image,end,1,5));
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

        img.color = colorEnd;
    }
}
