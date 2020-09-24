using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BuildAsssetBundles : MonoBehaviour
{
#if UNITY_EDITOR
    [MenuItem("Bundles/Build AssetBundles")]
    static void BuildAllAssetBundles()
    {
        BuildPipeline.BuildAssetBundles("Assets/Bundles", BuildAssetBundleOptions.None, BuildTarget.iOS);
    }
#endif
    // 번들 다운 받을 서버의 주소(필자는 임시방편으로 로컬 파일 경로 쓸 것임) public string BundleURL;
    // 번들의 version public int version;

    void Start()
    {
        StartCoroutine (DownloadAndCache());
    }
    IEnumerator DownloadAndCache()
    {
        string BundleURL = "file:///Users/ace-noone/Unity/Unity_Project(Ace1)/Unity_Project(Ace2_Tetirs3D_Solid)Refactoring/Assets/Bundles/";
        // cache 폴더에 AssetBundle을 담을 것이므로 캐싱시스템이 준비 될 때까지 기다림
        while (!Caching.ready) yield return null;
        // 에셋번들을 캐시에 있으면 로드하고, 없으면 다운로드하여 캐시폴더에 저장합니다.
        using (WWW www = WWW.LoadFromCacheOrDownload(BundleURL, 0))
        {
            yield return www;

            if (www.error != null)
                throw new Exception("WWW 다운로드에 에러가 생겼습니다.:" + www.error);

            AssetBundle bundle = www.assetBundle;

            AssetBundleRequest request = bundle.LoadAssetAsync("Sphere");
            GameObject temp = Instantiate(request.asset) as GameObject;

            bundle.Unload(false);
            www.Dispose();
        }

        // using문은 File 및 Font 처럼 컴퓨터 에서 관리되는 리소스들을 쓰고 나서 쉽게 자원을 되돌려줄수 있도록 기능을 제공
    }
}
