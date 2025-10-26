using Assets.GAME.Scripts.Card;
using Assets.GAME.Scripts.RemoteSprites;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.GAME.Scripts.World.CardSpawn {

    public class CardSpawner : MonoBehaviour {

        [SerializeField] private CardMain _cardPrefab;
        [SerializeField] private CardSpawnerConfig _config;
        [SerializeField] private Transform _startPosition;
        [SerializeField] private SpritesHolder _spritesHolder;

        private List<CardSprite> _cardSprites = new List<CardSprite>();

        public List<CardMain> SpawnedCards { get; private set; } = new List<CardMain>();

        public void Initialize() {
            ClearSpawnedCards();

            GenerateCardSprites();
            SpawnCards();
        }

        private void ClearSpawnedCards() {
            for (int i = 0; i < SpawnedCards.Count; i++) Destroy(SpawnedCards[i].gameObject);

            SpawnedCards.Clear();
        }

        private void GenerateCardSprites() {
            _cardSprites.Clear();
            int spriteId = 0;

            for (int i = 0; i < _config.Rows * _config.Columns; i++) {
                _cardSprites.Add(new CardSprite(spriteId, _spritesHolder.CardBackSprites[spriteId]));

                Debug.Log("card id add " + spriteId);

                spriteId++;

                if (spriteId >= _spritesHolder.CardBackSprites.Count) spriteId = 0;
            }

            //Stir
            for (int i = 0; i < _cardSprites.Count; i++) {
                CardSprite cardSpriteTemp = _cardSprites[i];

                int randomIndex = UnityEngine.Random.Range(i, _cardSprites.Count);
                _cardSprites[i] = _cardSprites[randomIndex];

                _cardSprites[randomIndex] = cardSpriteTemp; 
            }
        }

        private void SpawnCards()  {
            Vector2 cardSize = GetCardSize(_cardPrefab.gameObject);

            //offset setup
            float totalWidth = _config.Columns * cardSize.x + (_config.Columns - 1) * _config.SpacingX;
            float totalHeight = _config.Rows * cardSize.y + (_config.Rows - 1) * _config.SpacingY;
            Vector2 gridOffset = new Vector2(totalWidth / 2f - cardSize.x / 2f, totalHeight / 2f - cardSize.y / 2f);

            //Spawn
            int spriteID = 0;

            for (int y = 0; y < _config.Rows; y++) {

                for (int x = 0; x < _config.Columns; x++) {

                    Vector2 spawnPos = new Vector2(
                        _startPosition.position.x + x * (cardSize.x + _config.SpacingX),
                        _startPosition.position.y - y * (cardSize.y + _config.SpacingY)
                    );

                    spawnPos -= gridOffset;

                    CardMain card = Instantiate(_cardPrefab, spawnPos, Quaternion.identity, transform);
                    card.Initialize(
                        _cardSprites[spriteID].ID, 
                        _spritesHolder.CardFrontSprite, 
                        _cardSprites[spriteID].BackSprite);

                    spriteID++;
                    SpawnedCards.Add(card);
                }
            }
        }

        private Vector2 GetCardSize(GameObject prefab) {
            BoxCollider2D box = prefab.GetComponent<BoxCollider2D>();

            if (box != null) {
                Vector3 scale = prefab.transform.localScale;
                return new Vector2(box.size.x * Mathf.Abs(scale.x), box.size.y * Mathf.Abs(scale.y));
            }
            else {
                return Vector2.one;
            }
        }
    }
}
