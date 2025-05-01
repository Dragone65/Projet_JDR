using UnityEditor;
using UnityEngine;


public class BuildAssetBundles
{
    [MenuItem("Bundle/Build")]
    static void BuildAllAssetBundles()
    {
        // Specify the output directory for the AssetBundle files
        string assetBundleDirectory = "Assets/StreamingAssets";
        if (!System.IO.Directory.Exists(assetBundleDirectory))
        {
            System.IO.Directory.CreateDirectory(assetBundleDirectory);
        }

        // Build the AssetBundles
        BuildPipeline.BuildAssetBundles(assetBundleDirectory, 
            BuildAssetBundleOptions.None, 
            BuildTarget.WebGL);
        
        Debug.Log("AssetBundles have been built and placed in the StreamingAssets folder.");
    }
}