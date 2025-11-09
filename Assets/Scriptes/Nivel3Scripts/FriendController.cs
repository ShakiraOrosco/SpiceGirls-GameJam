using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FriendController : MonoBehaviour
{
    public List<Transform> pathPoints = new List<Transform>();
    public float speed = 2f;
    public float stopDistance = 0.1f;
    public Animator animator; // animator con bool "isWalking"
    public AudioClip notificationSound; // opcional, o reproducir desde manager

    private int currentIndex = 0;
    private bool isMoving = false;

    void Start()
    {
        if (animator == null) animator = GetComponent<Animator>();
    }

    public void StartWalking()
    {
        if (pathPoints.Count == 0) return;
        isMoving = true;
        currentIndex = 0;
        if (animator != null) animator.SetBool("isWalking", true);
        StartCoroutine(MoveRoutine());
    }

    IEnumerator MoveRoutine()
    {
        while (isMoving && currentIndex < pathPoints.Count)
        {
            Transform target = pathPoints[currentIndex];
            while (Vector3.Distance(transform.position, target.position) > stopDistance)
            {
                Vector3 dir = (target.position - transform.position).normalized;
                transform.position += dir * speed * Time.deltaTime;
                transform.forward = Vector3.Lerp(transform.forward, dir, Time.deltaTime * 10f);
                yield return null;
            }

            // reached point
            currentIndex++;
            yield return new WaitForSeconds(0.01f); // pequeña pausa si quieres
        }

        // reached final point
        isMoving = false;
        if (animator != null) animator.SetBool("isWalking", false);
        OnReachedEnd();
    }

    void OnReachedEnd()
    {
        // Reproduce sonido y notifica
        GameEventManager.Instance.FriendReachedEnd(transform.position, notificationSound);
        Destroy(gameObject); // destruye a la amiga
    }
}
