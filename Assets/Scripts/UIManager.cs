using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] Text _scoreText;
    [SerializeField] Image _livesImage;
    [SerializeField] Sprite[] _livesSprites;
    PlayerController player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();

        _scoreText.text = "Score: " + 0;
    }

    public void UpdateScore(int playerScore)
    {
        _scoreText.text = "Score: " + playerScore.ToString();
    }

    public void UpdateLives(int currentLives)
    {
        _livesImage.sprite = _livesSprites[currentLives];
    }
}
