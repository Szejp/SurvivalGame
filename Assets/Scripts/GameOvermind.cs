using System.Collections.Generic;
using System.Linq;
using Game.Scripts.Spawning;
using UnityEngine;

public class GameOvermind : MonoSingleton<GameOvermind>
{
    public Spawner spawner;
    public MapController mapController;
    public bool friendlyFire = false;
    public List<Agent> players;
    public IMouseTargetable mouseTarget;
    public MouseRaycaster mouseRaycaster;

    protected override void Awake()
    {
        base.Awake();
        players.Add(FindObjectOfType<Player>());
        Camera.main.GetComponent<CameraFollow>().SetTarget(players.First().transform);
    }

    private void Update()
    {
        mouseTarget = mouseRaycaster.target;
    }
}