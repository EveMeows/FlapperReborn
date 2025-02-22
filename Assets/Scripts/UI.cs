using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    #region Editor
    [SerializeField] private GameObject _scoreContainer;

    [SerializeField] private Vector2 _imageSize;
    [SerializeField] private Vector3 _imageScale;
    #endregion

    public void OnPlayerScoreChanged(int score)
    {
        // Eliminate the children.
        if (_scoreContainer.transform.childCount > 0)
        {
            foreach (Transform child in _scoreContainer.transform)
            {
                Destroy(child.gameObject);
            }
        }

        // This would usually be looping backwards
        // But we checked "reverse arrangement" in the Horizontal Layout Group
        // Which means it'll look right in-game.
        while (score > 0)
        {
            int num = score % 10;

            GameObject obj = new GameObject($"Decimal ({num})");
            obj.transform.SetParent(_scoreContainer.transform);

            Image image = obj.AddComponent<Image>();

            // Set image size and scale
            RectTransform rect = image.GetComponent<RectTransform>();
            rect.sizeDelta = _imageSize;
            rect.localScale = _imageScale;

            // Set sprite
            Sprite sprite = Resources.Load<Sprite>($"Sprites/UI/Numbers/Large/{num}");
            if (sprite == null)
            {
                Debug.LogError($"Cannot find resource: Sprites/UI/Numbers/Large/{num}");
                return;
            }

            // We set preserveAspect
            // If we don't do this, the image will look wonky
            // Also weirdly stretched...
            image.preserveAspect = true;

            image.sprite = sprite;

            score /= 10;
        }
    }
}
