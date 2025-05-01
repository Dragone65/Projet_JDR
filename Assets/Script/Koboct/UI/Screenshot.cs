using System.Runtime.InteropServices;
using UnityEngine;

namespace Koboct.UI
{
    public class Screenshot : MonoBehaviour
    {
        public Texture2D ScreenshotTexture;
 

        [DllImport("__Internal")]
        public static extern void DownloadFile(byte[] array, int byteLength, string fileName);
        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                CaptureAndDownloadScreenshot();
            }
                
        }

        public void CaptureAndDownloadScreenshot()
        {
            ScreenshotTexture = ScreenCapture.CaptureScreenshotAsTexture(3);
            byte[] texture = ScreenshotTexture.EncodeToPNG();
            DownloadFile(texture, texture.Length,  "fiche.png");
            Destroy(ScreenshotTexture);
        }
    }
}