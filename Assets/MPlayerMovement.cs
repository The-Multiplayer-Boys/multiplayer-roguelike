using Mirror;
using UnityEngine;

public class Player : NetworkBehaviour
{
    [SyncVar] public float movementSpeed = 5f;

    private Rigidbody2D rigidBody;

    #region Server


    [Command]
    private void CmdMove(Vector2 movementDirection)
    {
        this.movementDirection = movementDirection;
        rigidBody.velocity = movementDirection * movementSpeed;
    }

    #endregion

    #region Client

    public override void OnStartClient()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (hasAuthority)
        {
            ProcessInputs();
        }

        if (isServer)
        {
            RpcPlayAnimations();
        }
    }

    private void FixedUpdate()
    {
        rigidBody.velocity = movementDirection * movementSpeed;
    }

    private void ProcessInputs()
    {
        movementDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        rigidBody.velocity = movementDirection * movementSpeed;
        CmdMove(movementDirection);
    }

    [ClientRpc]
    private void RpcPlayAnimations()
    {
    }

    #endregion
}