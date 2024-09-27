using UnityEngine;
using static PlayerControll;

public class PointScript : MonoBehaviour
{
    public LabirinthManager LabirinthManager;
    public Transform NextPoint;
    public KindMove KindMove = KindMove.Stay;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            LabirinthManager.lastPoint = gameObject;
        }
    }
}
