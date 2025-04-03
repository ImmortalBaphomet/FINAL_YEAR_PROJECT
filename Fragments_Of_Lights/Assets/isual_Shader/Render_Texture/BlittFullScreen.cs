using UnityEngine;
using UnityEngine.UI;


public class BlittFullScreen : MonoBehaviour
{
    public Material fullscreenMaterial; // Assign your Fullscreen Shader Graph Material
    public RenderTexture renderTexture; // Assign your Render Texture
    public RawImage rawImage; 

    private void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        Graphics.Blit(src, renderTexture, fullscreenMaterial);
        Graphics.Blit(renderTexture, dest);
    }


    private void Update()
    {
        if (rawImage != null)
        {
            rawImage.texture = renderTexture;
        }
    }
}
