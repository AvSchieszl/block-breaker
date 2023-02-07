using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] AudioClip breakSound = default;
    [SerializeField] GameObject hitFX = default;
    [SerializeField] int maxHits;
    [SerializeField] Sprite[] damageSprites;

    Level level;
    GameSession gameStatus;

    int spriteIndex = 0;

    private void Start()
    {
        CountBreakableBlocks();
    }

    private void CountBreakableBlocks()
    {
        level = FindObjectOfType<Level>();
        gameStatus = FindObjectOfType<GameSession>();
        if (tag == "Breakable")
        {
            level.CountBlocks();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (tag == "Breakable")
        {
            HandleHit();
        }
    }

    private void HandleHit()
    {
        maxHits--;
        if (maxHits <= 0)
        {
            DestroyBlock();
        }
        else
        {
            ShowNextHitSprite();
        }
    }

    private void ShowNextHitSprite()
    {
        if(damageSprites[spriteIndex] != null)
        {
            GetComponent<SpriteRenderer>().sprite = damageSprites[spriteIndex];
        }
        else
        {
            Debug.LogError("Block sprite is missing from array: " + gameObject.name);
        }
        spriteIndex++;
    }

    private void DestroyBlock()
    {
        gameStatus.AddToScore();
        AudioSource.PlayClipAtPoint(breakSound, Camera.main.transform.position);
        Destroy(gameObject);
        level.BlockDestroyed();
        TriggerHitFX();
    }

    private void TriggerHitFX()
    {
        GameObject sparkles = Instantiate(hitFX, transform.position, transform.rotation);
        Destroy(sparkles, 2f);
    }
}
