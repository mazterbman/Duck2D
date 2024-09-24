using System.IO;
using UnityEngine;

public class RenderCamera : MonoBehaviour
{

    [Space]
    [SerializeField] Camera CameraForRender;
    [SerializeField] int width = 3840;
    [SerializeField] int height = 2160;
    [SerializeField] int depth = 16;

    Texture2D RTImage(Camera camera)
    {
        RenderTexture renderTexture = new RenderTexture(width, height, depth);
        Rect rect = new Rect(0, 0, width, height);
        Texture2D texture = new Texture2D(width, height, TextureFormat.RGBA32, false);

        camera.targetTexture = renderTexture;
        camera.Render();

        RenderTexture currentRenderTexture = RenderTexture.active;
        RenderTexture.active = renderTexture;
        texture.ReadPixels(rect, 0, 0);
        texture.Apply();

        camera.targetTexture = null;
        RenderTexture.active = currentRenderTexture;
        Destroy(renderTexture);

        return texture;
    }

    void SaveToFile(Texture2D texture)
    {
        byte[] bytes = ImageConversion.EncodeToPNG(texture);
        Object.Destroy(texture);

        File.WriteAllBytes(Application.streamingAssetsPath + "/SavedScreen.png", bytes);
    }

    public void GenerateTexture()
    {
        SaveToFile(RTImage(CameraForRender));
    }
}
