using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform1Script : MonoBehaviour
{
    // Start is called before the first frame update
    private bool isMoving;
    private float speed = 3f;
    void Start()
    {
        isMoving = false;
    }

    private void FixedUpdate()
    {
        if (isMoving)
        {
            transform.position = new Vector3(transform.position.x + (speed * Time.fixedDeltaTime), transform.position.y, 0);

        }
    }

    //p�i kolizi s hr��em se pust� �asova�, kter� se po skon�en� s�m zavol� znovu
    //�asova� n�sob� rychlost -1 a tud� m�n� sm�r platformy
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isMoving)
        {
            StartCoroutine(WaitSeconds(3));
        }
        isMoving = true;


        collision.transform.SetParent(transform);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        collision.transform.SetParent(null);
    }

    IEnumerator WaitSeconds(float duration)
    {

        yield return new WaitForSeconds(duration);
        speed = speed * -1;
        StartCoroutine(WaitSeconds(duration));

    }

   


}
