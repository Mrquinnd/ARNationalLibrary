using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Threading.Tasks;

namespace OpenAI
{
    public class CreateImageEditsAI : MonoBehaviour
    {
        [SerializeField] private InputField inputField;
        [SerializeField] private Button button;
        [SerializeField] public Image image;

        private OpenAIApi openai = new OpenAIApi();

        private void Start()
        {
            button.onClick.AddListener(SendImageEditRequest);
        }

        private async void SendImageEditRequest()
        {
            image.sprite = null;
            button.enabled = false;
            inputField.enabled = false;

            var response = await openai.CreateImageEdit(new CreateImageEditRequest
            {
                Image = Application.dataPath + "/pool_empty.png",
                Mask = Application.dataPath + "/pool_flamingo.png",
                Prompt = inputField.text,
                N = 1,
                Size = "256x256",
            });

            if (response.Data != null)
            {
                using (var request = new UnityWebRequest(response.Data[0].Url))
                {
                    request.downloadHandler = new DownloadHandlerBuffer();
                    request.SendWebRequest();

                    while (!request.isDone) await Task.Yield();

                    Texture2D texture = new Texture2D(2, 2);
                    texture.LoadImage(request.downloadHandler.data);
                    var sprite = Sprite.Create(texture, new Rect(0, 0, 256, 256), Vector2.zero, 1f);
                    image.sprite = sprite;
                }
            }
            else
            {
                Debug.LogWarning("No image was edited from this prompt.");
            }

            button.enabled = true;
            inputField.enabled = true;
        }
    }
}
