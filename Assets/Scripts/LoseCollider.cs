using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseCollider : MonoBehaviour
{
    [SerializeField] GameObject gameOverCanvas;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameOver();
    }

    private void GameOver()
    {
        FindObjectOfType<GameSession>().enabled = false;
        Time.timeScale = 0f;
        FindObjectOfType<PaddleController>().enabled = false;
        GameObject gameOver = Instantiate(gameOverCanvas, transform.position, transform.rotation);
    }
}
