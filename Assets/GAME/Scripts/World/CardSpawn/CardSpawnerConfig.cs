using UnityEngine;

namespace Assets.GAME.Scripts.World.CardSpawn{

    [CreateAssetMenu(fileName = "Card Spawner Config", menuName = "World/Card Spawner Config")]
    public class CardSpawnerConfig : ScriptableObject {

        [field: Min(1)][field: SerializeField] public int Rows { get; private set; }

        [field: Min(1)][field: SerializeField] public int Columns { get; private set; }

        [field: SerializeField] public float SpacingX { get; private set; }

        [field: SerializeField] public float SpacingY { get; private set; }
    }
}
