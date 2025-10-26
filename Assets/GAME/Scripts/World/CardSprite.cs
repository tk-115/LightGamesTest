using UnityEngine;

namespace Assets.GAME.Scripts.World {

    public class CardSprite {

        public int ID;
        public Sprite BackSprite;

        public CardSprite(int iD, Sprite backSprite) {
            ID = iD;
            BackSprite = backSprite;
        }
    }
}
