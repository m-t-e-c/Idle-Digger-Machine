using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public static class Extensions
{
    /// <summary>
    /// Remap a value to given range
    /// </summary>
    /// <param name="value"></param>
    /// <param name="min"></param>
    /// <param name="max"></param>
    /// <param name="newMin"></param>
    /// <param name="newMax"></param>
    /// <returns></returns>
    public static float RemapFloat(this float value, float min, float max, float newMin, float newMax)
    {
        if (Mathf.Approximately(max, min))
        {
            return value;
        }

        return newMin + (value - min) * (newMax - newMin) / (max - min);
    }

    /// <summary>
    /// Remap a value to 0-1 range
    /// </summary>
    /// <param name="value"></param>
    /// <param name="min"></param>
    /// <param name="max"></param>
    /// <returns></returns>
    public static float Remap01(this float value, float min, float max)
    {
        return value.RemapFloat(min, max, 0f, 1f);
    }

    /// <summary>
    /// Convert RenderTexture to Texture2D
    /// </summary>
    /// <param name="rTex"></param>
    /// <returns></returns>
    public static Texture2D toTexture2D(this RenderTexture rTex)
    {
        Texture2D tex = new Texture2D(rTex.width, rTex.height, TextureFormat.RGB24, false);
        var old_rt = RenderTexture.active;
        RenderTexture.active = rTex;

        tex.ReadPixels(new Rect(0, 0, rTex.width, rTex.height), 0, 0);
        tex.Apply();

        RenderTexture.active = old_rt;
        return tex;
    }

    /// <summary>
    /// Take a "screenshot" of a camera's Render Texture.
    /// </summary>
    /// <param name="camera"></param>
    /// <returns>s</returns>
    public static Texture2D RTImage(this Camera camera)
    {
        // The Render Texture in RenderTexture.active is the one
        // that will be read by ReadPixels.
        var currentRT = RenderTexture.active;
        RenderTexture.active = camera.targetTexture;

        // Render the camera's view.
        camera.Render();

        // Make a new texture and read the active Render Texture into it.
        Texture2D image = new Texture2D(camera.targetTexture.width, camera.targetTexture.height);
        image.ReadPixels(new Rect(0, 0, camera.targetTexture.width, camera.targetTexture.height), 0, 0);
        image.Apply();

        // Replace the original active Render Texture.
        RenderTexture.active = currentRT;
        return image;
    }
}
