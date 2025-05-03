using System.Runtime.InteropServices;
using UnityEngine;

namespace Koboct.UI
{
    public class Screenshot : MonoBehaviour
    {
        public Texture2D ScreenshotTexture;

        [DllImport("__Internal")]
        public static extern void DownloadFile(byte[] array, int byteLength, string fileName);


        public void CaptureAndDownloadScreenshot()
        {
            StartCoroutine(CaptureAtEndOfFrame());
        }

        private System.Collections.IEnumerator CaptureAtEndOfFrame()
        {
            yield return new WaitForEndOfFrame();

            ScreenshotTexture = ScreenCapture.CaptureScreenshotAsTexture();
            if (ScreenshotTexture == null)
            {
                Debug.LogError("CaptureScreenshotAsTexture() failed: texture is null");
                yield break;
            }

            byte[] texture = ScreenshotTexture.EncodeToPNG();
            DownloadFile(texture, texture.Length, "fiche.png");
            Destroy(ScreenshotTexture);
        }
    }
}