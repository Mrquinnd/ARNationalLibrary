using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImplementNFTStorage : MonoBehaviour
{
    string fullPath;
    public NFTStorage.NFTStorageClient NSC;
    public Button captureButton;
    public Button uploadButton;

    private void Start()
    {
        captureButton.onClick.AddListener(OnCaptureButtonClick);
        uploadButton.onClick.AddListener(OnUploadButtonClick);
    }

    public void OnCaptureButtonClick()
    {
        fullPath = Application.persistentDataPath + "/myImage " + System.DateTime.Now.ToString("yy-MM-dd") + ".png";
        ScreenCapture.CaptureScreenshot(fullPath);
    }

    public void OnUploadButtonClick()
    {
        NSC.UploadDataFromStringUnityWebrequest(fullPath);
    }
}
