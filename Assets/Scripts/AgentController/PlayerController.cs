using Photon.Pun;
using UnityEngine;

public class PlayerController : AgentController
{
    private RaycastHit hit;
    private AgentAnimationController animController;

    private void Start()
    {
        animController = GetComponent<AgentAnimationController>();
    }

    private void Update()
    {
        if (!photonView.IsMine && PhotonNetwork.IsConnected)
            return;

        if (Input.GetKey(KeyCode.W))
            movement?.MoveForwards();
        if (Input.GetKey(KeyCode.S))
            movement?.MoveBackwards();
        if (Input.GetKey(KeyCode.D))
            movement?.MoveRight();
        if (Input.GetKey(KeyCode.A))
            movement?.MoveLeft();
        if (Input.GetKeyUp(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            weapon.Fire();
            animController.Shoot();
        }

        gunTransform?.SetTarget(new Vector3(GameOvermind.instance.mouseRaycaster.planeHit.x,
            gunTransform.transform.position.y, GameOvermind.instance.mouseRaycaster.planeHit.z));
    }
}