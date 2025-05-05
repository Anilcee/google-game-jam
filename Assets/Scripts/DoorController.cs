using UnityEngine;
using System.Collections;

public class DoorController : MonoBehaviour
{
    private Animator animator;
    private Collider2D doorCollider;
    private bool isBusy = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        doorCollider = GetComponent<Collider2D>();
    }

    public void OperateDoor()
    {
        if(!isBusy) StartCoroutine(DoorOperation());
    }

    private IEnumerator DoorOperation()
    {
        isBusy = true;
        animator.SetTrigger("Open");
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        doorCollider.enabled = false;  
        yield return new WaitForSeconds(1.5f);
        animator.SetTrigger("Close");
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        doorCollider.enabled = true;
        isBusy = false;
    }
}