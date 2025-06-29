using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{
    public Text scoreText;
    private static int score;

    private AudioSource audioSource;

    public AudioClip coinSound;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == MyTag.CoinTag)
        {
            IncrimentScore(1);
            audioSource.PlayOneShot(coinSound);
            Destroy(collision.gameObject);
        }
    }

    public void IncrimentScore(int score)
    {
        ScoreScript.score += score;
        scoreText.text = "x" + ScoreScript.score.ToString();
        print(score);
    }
}
