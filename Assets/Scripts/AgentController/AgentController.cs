using Photon.Pun;

public class AgentController : MonoBehaviourPun {

    public Movement movement;
    public Weapon weapon;
	public GunTransform gunTransform;
	public Agent agent;

    public virtual void Attack() { }

	protected virtual void Awake() {
		weapon = GetComponentInChildren<Weapon>();
		movement = GetComponentInChildren<Movement>();
		agent = GetComponent<Agent>();
		if(agent != null) weapon.owner = agent;
	}
}
