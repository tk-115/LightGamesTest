using TMPro;
using UnityEngine;

namespace Assets.GAME.Scripts.View {

    public class GameplayScreenView : ScreenViewBase {

        public const string PairsCollectedLocaleText = "Pairs collected: ";

        [SerializeField] private TextMeshProUGUI _pairsCollectedText;

        public void SetPairsCollected(int count) =>
            _pairsCollectedText.text = PairsCollectedLocaleText + count.ToString();
    }
}
