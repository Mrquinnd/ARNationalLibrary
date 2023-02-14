using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Screenshot : MonoBehaviour
{

    public GameObject bgAvatar;
    //public int width
    //public int height

    public float ratioToScreen;
    public GameObject nft;
    public GameObject cid;

    public Button screenshotButton;
    public Button uploadButton;

    public Button uploadJsonButton;


    //public GameObject cid JSON;

    private string fullPath;
    private string jsonPath;
    public NFTStorage.NFTStorageClient NSC;
    private byte[] bytes;
    private string encodedImage;
private void Start()
    {
        screenshotButton.onClick.AddListener(TakeScreenshot);
        uploadButton.onClick.AddListener(UploadToNFTStorage);
        uploadJsonButton.onClick.AddListener(UploadJsonToNFTStorage);

    }

    public void TakeScreenshot(){
        StartCoroutine(CoroutineScreenshot());
        Debug.Log("TakeScreenshot method called");

    }
    private IEnumerator CoroutineScreenshot(){
        Debug.Log("TakeScreenshot method called");

        yield return new WaitForEndOfFrame();
        int x = (int)bgAvatar.transform.position.x;
        int y = (int)bgAvatar.transform.position.y;
        Vector3 pos = Camera.main.WorldToScreenPoint(bgAvatar.transform.position);
        Debug.Log(pos.x + " " + pos.y);
        int screenWidth = Screen.width;
        int screenHeight = Screen.width;
        int width = (int)(screenWidth * ratioToScreen);
        int height = (int)(screenHeight * ratioToScreen);

        Texture2D texture = new Texture2D(width, height, TextureFormat.ARGB32, false);
        texture.ReadPixels(new Rect(pos.x - width/2, pos.y - height/2, width, height), 0, 0);
        texture.Apply();
        bytes = texture.EncodeToPNG();
        fullPath = Application.persistentDataPath + "/screenshot1.png";
        System.IO.File.WriteAllBytes(fullPath, bytes);

        Image img = nft.GetComponent<Image>();
        img.sprite = Sprite.Create(texture, new Rect(0,0, texture.width, texture.height), new Vector2(0.5f, 0.5f));

    }
    public void UploadToNFTStorage(){
        Debug.Log("Start to upload to NFT Storage");
        //encodedImage = Syetem.Convert to base64 string
        if(fullPath != null || fullPath != ""){

            //await NSC upload
            NSC.UploadDataFromStringUnityWebrequest(fullPath);
            Debug.Log("Uploaded to NFT Storage");
        }

    }

    public void UploadJsonToNFTStorage(){
        Debug.Log("Generating JSON file");
        GenerateJson();
        Debug.Log("Start to upload JSON to NFT storage");
        if(jsonPath != null || jsonPath != ""){
            //await
            NSC.UploadDataFromStringUnityWebrequest(jsonPath);
            Debug.Log("Uploaded JSON to NFT Storage");

        }

    }
        [SerializeField] private NFTData nftData = new NFTData();

        public void GenerateJson(){
            //Get image link from gameobject
            Text cidText = cid.GetComponent<Text>();
            nftData.image = "ipfs://" + cidText.text;
            Debug.Log(nftData.image);
            string jsonData = JsonUtility.ToJson(nftData);
            System.IO.File.WriteAllText(Application.persistentDataPath + "/nftData.json", jsonData);
            jsonPath = Application.persistentDataPath + "/nftData.json";

        }

        [System.Serializable]
        public class NFTData{
        public string name = "NLNZ ARTEFACT";
        public string description = "A gamified experience for experiencing historical artefacts.";
        public string image;

    }

    
}
