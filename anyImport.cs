using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


//Used by metadata class for storing attributes
public class Attributes
{
    //The type or name of a given trait
    public string trait_type;
    //The value associated with the trait_type
    public string value;
}

//Used for storing NFT metadata from standard NFT json files
public class Metadata
{
    //List storing attributes of the NFT
    public List<Attributes> attributes { get; set; }
    //Description of the NFT
    public string description { get; set; }
    //An external_url related to the NFT (often a website)
    public string external_url { get; set; }
    //image stores the NFTs URI for image NFTs
    public string image { get; set; }
    //Name of the NFT
    public string name { get; set; }
}

//Interacting with blockchain
public class anyImport : MonoBehaviour
{
    //The chain to interact with, using Polygon here
    public string chain = "ethereum";
    //The network to interact with (mainnet, testnet)
    public string network = "mainnet";
    //Contract to interact with, contract below is "Project: Pigeon Smart Contract"
    public string contract = "0x60E4d786628Fea6478F785A6d7e704777c86a7c6";
    //Token ID to pull from contract
    public string tokenId = "19762";
    //Used for storing metadata
    Metadata metadata;

    private void Start()
    {
        //Starts async function to get the NFT image
        GetNFTImage();
    }

    async private void GetNFTImage()
    { 

        //Perform webrequest to get JSON file from URI
        using (UnityWebRequest webRequest = UnityWebRequestTexture.GetTexture("https://nftstorage.link/ipfs/QmfGFBV3fdF58Eox3scVrNnTeZ3R2eZ6DK5PHU5VwFFprF"))

        //Performs another web request to collect the image related to the NFT
        {
            //Sends webrequest
            await webRequest.SendWebRequest();
            //Gets the image from the web request and stores it as a texture
            Texture texture = DownloadHandlerTexture.GetContent(webRequest);
            //Sets the objects main render material to the texture
            GetComponent<MeshRenderer>().material.mainTexture = texture;
        }
        
    }
}
