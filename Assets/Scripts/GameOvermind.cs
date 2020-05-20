using System.Linq;
using UnityEngine;

public class GameOvermind : MonoSingleton<GameOvermind>
{
    public Spawner spawner;
    public MapController mapController;
    public bool friendlyFire = false;
    public Agent[] players;
    public IMouseTargetable mouseTarget;
    public MouseRaycaster mouseRaycaster;

    protected override void Awake()
    {
        base.Awake();
        players = FindObjectsOfType<Player>();
        Camera.main.GetComponent<CameraFollow>().SetTarget(players.First().transform);
    }

    private void Update()
    {
        mouseTarget = mouseRaycaster.target;
    }
}