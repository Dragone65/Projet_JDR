using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

namespace Koboct
{
    public class AssetBundleSceneLoader : MonoBehaviour
    {
        [Header("AssetBundle Settings")]
        // The URL to download the AssetBundle
        public string sceneName; // Name of the scene contained in the AssetBundle

        public string localAssetBundlePath; // Optional: Local fallback path for testing in editor

        private AssetBundle loadedBundle;

        /// <summary>
        /// Start method: Downloads the AssetBundle at runtime.
        /// </summary>
        private IEnumerator Start()
        {
            // Allow optional testing from a local directory if specified
            string url = Application.streamingAssetsPath + "/" + localAssetBundlePath;

            Debug.Log($"Starting download from URL: {url}");

            // Download the AssetBundle
            UnityWebRequest www = UnityWebRequestAssetBundle.GetAssetBundle(url);
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error downloading AssetBundle: " + www.error);
            }
            else
            {
                // Load the AssetBundle
                loadedBundle = DownloadHandlerAssetBundle.GetContent(www);
                Debug.Log("AssetBundle successfully downloaded and loaded.");
            }
        }

        /// <summary>
        /// Public method to launch the scene from the loaded AssetBundle.
        /// </summary>
        public void LaunchScene()
        {

            if (loadedBundle == null)
            {
                Debug.LogError("AssetBundle has not been loaded! Cannot launch the scene.");
                return;
            }

            // Check if the scene exists in the AssetBundle
            string[] scenePaths = loadedBundle.GetAllScenePaths();
            bool sceneExists = false;

            foreach (string path in scenePaths)
            {
                if (path.EndsWith(sceneName + ".unity"))
                {
                    sceneExists = true;
                    break;
                }
            }

            if (sceneExists)
            {
                Debug.Log($"Launching scene: {sceneName}");
                StartCoroutine(LoadSceneFromBundle());
            }
            else
            {
                Debug.LogError($"Scene '{sceneName}' not found in the AssetBundle!");
            }
        }

        /// <summary>
        /// Loads the scene asynchronously from the AssetBundle.
        /// </summary>
        private IEnumerator LoadSceneFromBundle()
        {
            yield return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
            Debug.Log($"Scene '{sceneName}' successfully loaded.");
        }

        private void OnDestroy()
        {
            // Clean up the AssetBundle when the object is destroyed
            if (loadedBundle != null)
            {
                loadedBundle.Unload(false);
            }
        }
    }
}