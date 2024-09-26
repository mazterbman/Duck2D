using System.Collections;
using UnityEngine;

public class PlayerControll : MonoBehaviour
{
    [SerializeField, Range(0, 25)] float moveSpeed;
    public KindMove kindMove = KindMove.Stay;
    public KindCharacter kindCharacter = KindCharacter.Chibick;
    
    [SerializeField] Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetInteger("State",0); // IDLE animations

        StartCoroutine(moveCharacter());
    }

    IEnumerator moveCharacter()
    {
        yield return null;

        while (true)
        {
            yield return null;
            switch (kindMove)
            {
                case KindMove.Stay:
                    animator.SetInteger("State", 0); // IDLE animations
                    break;
                case KindMove.Left:
                    moveLeft();
                    break;
                case KindMove.Right:
                    moveRight();
                    break;
                case KindMove.Up:
                    moveUp();
                    break;
                case KindMove.Down:
                    moveDown();
                    break;
            }
        }
    }

    void moveRight()
    {
        animator.SetInteger("State", 1); // Walk animations
        transform.localScale = new Vector3(transform.localScale.x > 0 ? transform.localScale.x * -1 : transform.localScale.x,
                                   transform.localScale.y,
                                   transform.localScale.z);
        switch (kindCharacter)
        {
            case KindCharacter.Chibick:
                //GetComponent<Rigidbody>().Move(new Vector3(Time.deltaTime * moveSpeed * -1, 0, 0), Quaternion.identity);
                //GetComponent<Rigidbody>().AddForce(new Vector3(moveSpeed * -1, 0, 0),ForceMode.Force);
                transform.Translate(Time.deltaTime * moveSpeed * -1, 0, 0);
                break;

            case KindCharacter.Goose:
                //GetComponent<Rigidbody>().Move(new Vector3(Time.deltaTime * moveSpeed, 0, 0), Quaternion.identity);
                transform.Translate(Time.deltaTime * moveSpeed, 0, 0);
                break;
        }

    }

    void moveLeft()
    {
        animator.SetInteger("State", 1); // Walk animations
        transform.localScale = new Vector3(transform.localScale.x > 0 ? transform.localScale.x : transform.localScale.x * -1,
                                   transform.localScale.y,
                                   transform.localScale.z);
        switch (kindCharacter)
        {
            case KindCharacter.Chibick:
                //GetComponent<Rigidbody>().Move(new Vector3(Time.deltaTime * moveSpeed, 0, 0), Quaternion.identity);
                transform.Translate(Time.deltaTime * moveSpeed, 0, 0);
                break;

            case KindCharacter.Goose:
                //GetComponent<Rigidbody>().Move(new Vector3(Time.deltaTime * moveSpeed * -1, 0, 0), Quaternion.identity);
                transform.Translate(Time.deltaTime * moveSpeed * -1, 0, 0);

                break;
        }
    }

    void moveUp()
    {
        animator.SetInteger("State", 1); // Walk animations
        switch (kindCharacter)
        {
            case KindCharacter.Chibick:
                //GetComponent<Rigidbody>().AddForce(new Vector3(0, 0,moveSpeed * -1), ForceMode.Force);
                //GetComponent<Rigidbody>().Move(new Vector3(0, 0, Time.deltaTime * moveSpeed * -1), Quaternion.identity);
                transform.Translate(0, 0, Time.deltaTime * moveSpeed * -1);
                break;

            case KindCharacter.Goose:
                //GetComponent<Rigidbody>().Move(new Vector3(0, 0, Time.deltaTime * moveSpeed), Quaternion.identity);
                transform.Translate(0, Time.deltaTime * moveSpeed, 0);
                break;
        }

    }

    void moveDown()
    {
        animator.SetInteger("State", 1); // Walk animations
        switch (kindCharacter)
        {
            case KindCharacter.Chibick:
                //GetComponent<Rigidbody>().AddForce(new Vector3(0, 0,moveSpeed), ForceMode.Force);
                //GetComponent<Rigidbody>().Move(new Vector3(0, 0, Time.deltaTime * moveSpeed), Quaternion.identity);
                transform.Translate(0, 0, Time.deltaTime * moveSpeed);
                break;

            case KindCharacter.Goose:
                //GetComponent<Rigidbody>().Move(new Vector3(0, Time.deltaTime * moveSpeed * -1, 0), Quaternion.identity);
                transform.Translate(0, Time.deltaTime * moveSpeed * -1, 0);
                break;
        }
    }

    public enum KindMove
    {
        Stay = 0,
        Left = 1,
        Right = 2,
        Up = 3,
        Down = 4,
    }

    public enum KindCharacter
    {
        Chibick = 0,
        Goose = 1,
    }
}
