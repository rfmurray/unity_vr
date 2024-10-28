using System.IO;
using UnityEngine;
using UnityEngine.Rendering;

public class MainScript : MonoBehaviour
{
    const int imsize = 256; // size of region to capture
    Rect readRect;          // rectangle specifying region to capture
    Texture2D tex;          // texture where captured region will be stored
    bool captureRequested = false, captureWaiting = false;

    void Start()
    {
        // get coordinates of region to capture
        int startx = (Screen.width / 2) - (imsize / 2);
        int starty = (Screen.height / 2) - (imsize / 2);
        readRect = new Rect(startx, starty, imsize, imsize);

        // create texture object where capture will be stored
        tex = new Texture2D(imsize, imsize, TextureFormat.RGB24, mipChain: false);

        // add callback for end of rendering
        RenderPipelineManager.endCameraRendering += OnEndCameraRendering;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            captureRequested = captureWaiting = true;

        if (!captureRequested || captureWaiting)
            return;

        // convert captured region to Color32 array
        Color32[] pix = tex.GetPixels32();

        // save pixels to data file
        using (StreamWriter writer = new StreamWriter("data.txt", append: false))
            for (int i = 0; i < pix.Length; i++)
                writer.WriteLine($"{pix[i].r}, {pix[i].g}, {pix[i].b}");

        // record that the request is done
        captureRequested = false;
    }

    void OnEndCameraRendering(ScriptableRenderContext context, Camera camera)
    {
        if (camera != Camera.main)  // this is the 'Main Camera' object; may need to change it for VR
            return;

        if (!(captureRequested && captureWaiting))
            return;

        // copy pixels from framebuffer into texture
        tex.ReadPixels(readRect, 0, 0, recalculateMipMaps: false);
        captureWaiting = false;
    }

    void OnDestroy()
    {
        RenderPipelineManager.endCameraRendering -= OnEndCameraRendering;
    }
}
