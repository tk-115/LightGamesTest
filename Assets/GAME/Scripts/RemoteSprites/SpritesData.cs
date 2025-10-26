using System;
using System.Collections.Generic;

namespace Assets.GAME.Scripts.RemoteSprites {

    [Serializable]
    public class SpritesData {

        public string MainCardUrl;
        public List<string> BackCardsUrls = new List<string>();

        public SpritesData() { }
    }
}
