using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets.GAME.Scripts.RemoteSprites{

    public class SpritesHolder : MonoBehaviour {

        private const string SpritesDataFileUrl = "https://drive.usercontent.google.com/download?id=1USpT9oaoFNP9Ou25Mbp-s1vYKQV4sND0&export=download&authuser=0";

        public event Action OnLoadCompleteEvent;
        public event Action<string> OnLoadFailedEvent;

        public Sprite CardFrontSprite { get; private set; }
        public List<Sprite> CardBackSprites { get; private set; } = new List<Sprite>();

        private SpritesData _spritesData = null;

        public async Task Initialize() {
            if (await LoadSpritesDataAsync() == true) 
                LoadSprites();
            else 
                OnLoadFailedEvent?.Invoke("Remote sprites data not available! Check internet connection");
        }

        private async void LoadSprites() {
            CardFrontSprite = await LoadSpriteAsync(_spritesData.MainCardUrl);

            if (CardFrontSprite != null) {

                for (int i = 0; i < _spritesData.BackCardsUrls.Count; i++) {

                    Sprite cardBackSprite = await LoadSpriteAsync(_spritesData.BackCardsUrls[i]);

                    if (cardBackSprite != null) {
                        CardBackSprites.Add(cardBackSprite);
                    } else {
                        OnLoadFailedEvent?.Invoke("Back card sprite index = " + i + " not loaded! Check url");
                        return;
                    }
                }

                OnLoadCompleteEvent?.Invoke();
            }
            else {
                OnLoadFailedEvent?.Invoke("Front card sprite not loaded! Check url");
            }
        }

        public async Task<bool> LoadSpritesDataAsync() {

            using (UnityWebRequest www = UnityWebRequest.Get(SpritesDataFileUrl)) {
                var operation = www.SendWebRequest();

                //wait for request complete
                while (operation.isDone == false) await Task.Yield();

                if (www.result == UnityWebRequest.Result.Success) {

                    string jsonText = www.downloadHandler.text;
                    _spritesData = JsonUtility.FromJson<SpritesData>(jsonText);

                    return true;
                }
                else {
                    Debug.LogError("Failed to load sprite data file: " + www.error);
                    return false;
                }
            }
        }

        private async Task<Sprite> LoadSpriteAsync(string imageUrl) {

            using (UnityWebRequest www = UnityWebRequestTexture.GetTexture(imageUrl)) {
                var operation = www.SendWebRequest();

                while (operation.isDone == false) await Task.Yield();

                if (www.result == UnityWebRequest.Result.Success) {

                    Texture2D texture = DownloadHandlerTexture.GetContent(www);
                    Sprite sprite = Sprite.Create(texture,
                        new Rect(0, 0, texture.width, texture.height),
                        new Vector2(.5f, .5f));
                    
                    return sprite;
                }
                else {
                    Debug.LogError("Failed to load image: " + www.error);
                    return null;
                }
            }
        }
    }
}
