using System.Collections;
using UnityEngine;

public class DecaliDestroy : MonoBehaviour
{
    [SerializeField] string tagPlayer = "Player";
    [SerializeField] Animator animator;
    [SerializeField] float timeOffset = 0.5f;
    bool End = false;

    IEnumerator Start()
    {
        if (animator == null)
            animator = GetComponent<Animator>();

        animator.SetInteger("State", 2); //None Animated
        yield return new WaitForSeconds(Random.Range(0, timeOffset));

        animator.SetInteger("State", 0); //IDLE Animated
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == tagPlayer && !End)
        {
            animator.SetInteger("State", 1); //Destroy Animated
            GetComponent<BoxCollider>().enabled = false;
            GetComponent<AudioSource>().Play();
            End = true;
        }
    }
}
