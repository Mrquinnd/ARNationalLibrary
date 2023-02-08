using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace OpenAI
{
    

    public class Dall : MonoBehaviour
    {
        [SerializeField] private InputField inputField;
        [SerializeField] private Button button;
        [SerializeField] private GameObject cube;
        private OpenAIApi openai = new OpenAIApi();
        private string apiKey;

        

        private void Start()
        {
             // Load the authentication JSON file
            string filePath = Application.streamingAssetsPath + "/auth.json";
            string authJson = File.ReadAllText(filePath);
            Auth auth = JsonConvert.DeserializeObject<Auth>(authJson);
            apiKey = auth.ApiKey;

            button.onClick.AddListener(SendImageRequest);
        }
    


        private async void SendImageRequest()
        {
            button.enabled = false;
            inputField.enabled = false;
            
            var response = await openai.CreateImage(new CreateImageRequest
            {
                Prompt = inputField.text,
                Size = ImageSize.Size256,

            });

            if (response.Data != null)
            {
                using(var request = new UnityWebRequest(response.Data[0].Url))
                {
                    request.downloadHandler = new DownloadHandlerBuffer();
                    request.SendWebRequest();

                    while (!request.isDone) await Task.Yield();

                    Texture2D texture = new Texture2D(2, 2);
                    texture.LoadImage(request.downloadHandler.data);
                    var material = new Material(Shader.Find("Diffuse"));
                    material.mainTexture = texture;
                    cube.GetComponent<MeshRenderer>().material = material;
                }
            }
            else
            {
                Debug.LogWarning("No image was created from this prompt.");
            }

            button.enabled = true;
            inputField.enabled = true;
        }
    }
      public class Auth
    {
        public string ApiKey { get; set; }
    }
}


