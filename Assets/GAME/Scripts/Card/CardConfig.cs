using UnityEngine;

namespace Assets.GAME.Scripts.Card {

    [CreateAssetMenu(fileName = "Card Config", menuName = "Card/Card Config")]
    public class CardConfig : ScriptableObject {

        [field: Min(.1f)][field: SerializeField] public float SpawnDelay { get; private set; }

        [field: Min(.1f)][field: SerializeField] public float SelectDelay { get; private set; }

        [field: Min(.1f)][field: SerializeField] public float HideDelay { get; private set; }
    }
}
