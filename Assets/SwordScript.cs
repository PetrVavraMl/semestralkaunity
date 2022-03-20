using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator animatorPlayer;
    public AnimatorOverrideController overrideController;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (TriggerEnd.isChestOpened)
        {
            StartCoroutine(FadeOutAndLerp(GetComponent<SpriteRenderer>(), 1, collision));
            animatorPlayer.runtimeAnimatorController = overrideController;
            CombatLogic.attackRange = 2.5f;
            CombatLogic.attackDamage = 50;
        }
        
    }

    IEnumerator FadeOutAndLerp(SpriteRenderer renderer, float duration,Collider2D collision)
    {
        float time = 0;
        Vector2 endPosition = collision.transform.position;
        while (time < duration)
        {
            Color newColor = new Color(1, 1, 1, Mathf.Lerp(1, 0, time / duration));
            transform.position = Vector2.Lerp(transform.position, collision.transform.position,  time / duration);
            renderer.color = newColor;
            time += Time.deltaTime;
            yield return null;
        }
        Destroy(this.gameObject);
    }
}
