using UnityEngine;

namespace Assets.GAME.Scripts.World.Player {

    [CreateAssetMenu(fileName = "Player Selector Config", menuName = "World/Player Selector  Config")]
    public class PlayerSelectorConfig : ScriptableObject {

        [field: SerializeField] public LayerMask CardLayer { get; private set; }

        [field: Min(.1f)][field: SerializeField] public float TimeoutDelay { get; private set; }
    }
}
