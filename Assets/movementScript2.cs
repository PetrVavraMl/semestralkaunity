using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movementScript2 : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector2 movement = new Vector2();
    Rigidbody2D rigidBody;
    public float movementSpeed = 15;
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        MoveCharacter(movement);
    }
    private void GetInput()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    public void MoveCharacter(Vector2 movementVector)
    {
        movementVector.Normalize();
        rigidBody.velocity = movementVector * movementSpeed;
    }

}
