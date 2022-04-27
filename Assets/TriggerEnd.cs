using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEnd : MonoBehaviour
{
    // Start is called before the first frame update
    private bool isInChestZone;
    public static bool isChestOpened;
    public SpriteRenderer rendererChestOpen;
    public SpriteRenderer rendererChestClosed;
    public SpriteRenderer rendererSword;
    public GameObject objectSword;
    public ParticleSystem particleSystemSword;
    public Animator animatorPortal;
    public Transform chestOpenSound;
    

    void Start()
    {
        isInChestZone = false;
    }


    void Update()
    {
        //Pokud je hráè v okolí bedny a zmáèkne E spustí otevøe se bedna a spustí animace
        //if (Input.GetKey(KeyCode.E) && isInChestZone)
        //{
            
        //}
    }

    private void OpenChest()
    {
        //zakáže pohyb hráèi, pohne sprite smìrem nahoru a zmìní jeho prùhlednost pomocí coroutine
        if (!isChestOpened)
        {
            chestOpenSound.GetComponent<AudioSource>().Play();
            rendererChestClosed.enabled = false;
            rendererChestOpen.enabled = true;
            rendererSword.enabled = true;
            StartCoroutine(LerpPosition(3, objectSword));
            StartCoroutine(Fade(rendererSword, 1));
            Color newColor = new Color(1, 1, 1, 0);
            rendererSword.color = newColor;
            isChestOpened = true;
            animatorPortal.SetBool("portalStart",true);
        }
        
        

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        isInChestZone = true;
        Debug.Log("END!!");
        MovementScript.isRunning = false;
        OpenChest();
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        isInChestZone = false;
    }

    //posune sprite smìrem nahoru, zakáže a povolí hráèi pohyb 
    IEnumerator LerpPosition(float duration, GameObject gameObject)
    {
        MovementScript.canMove = false;
        float time = 0;
        Vector3 startPosition = gameObject.transform.position;
        startPosition.z = -5;
        Vector3 endPosition = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 0.8f, -5);
        while (time < duration)
        {
            gameObject.transform.position = Vector3.Lerp(startPosition, endPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        MovementScript.canMove = true;
    }

    //zmìna prùhlednosti spritu, spuštìní particlù 
    IEnumerator Fade(SpriteRenderer renderer, float duration)
    {
        float time = 0;
        float alpha = renderer.color.a;
        while (time < duration)
        {
            Color newColor = new Color(1, 1, 1, Mathf.Lerp(0, 1, time / duration));
            renderer.color = newColor;
            time += Time.deltaTime;
            yield return null;
        }
        particleSystemSword.Play();
        
    }

    
}
