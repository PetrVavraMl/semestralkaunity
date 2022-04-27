using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform bookPickedUp;
    public Transform bookPickedUpSound;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (TriggerEnd2.isChestOpened)
        {
            StartCoroutine(FadeOutAndLerp(GetComponent<SpriteRenderer>(), 2, collision));
            bookPickedUpSound.GetComponent<AudioSource>().Play();
        }

    }

    IEnumerator FadeOutAndLerp(SpriteRenderer renderer, float duration, Collider2D collision)
    {
        float time = 0;
        Vector2 endPosition = collision.transform.position;
        while (time < duration)
        {
            Color newColor = new Color(1, 1, 1, Mathf.Lerp(1, 0, time / duration));
            transform.position = Vector2.Lerp(transform.position, collision.transform.position, time / duration);
            renderer.color = newColor;
            time += Time.deltaTime;
            yield return null;
        }
        Destroy(this.gameObject);
    }
}
