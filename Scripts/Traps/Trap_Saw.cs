using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap_Saw : MonoBehaviour
{
    private Animator anim;
    private SpriteRenderer sr;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float idleTime;
    [SerializeField] private Transform[] wayPoint;

    [SerializeField] private Vector3[] wayPointPosition; 

    public int wayPointIndex = 1;
    public int moveDir = 1;
    private bool canMove = true;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        UpdateWayPointInfo();

        transform.position = wayPointPosition[0];
    }

    private void UpdateWayPointInfo()
    {
        wayPoint = new Transform[gameObject.transform.childCount];

        for (int i = 0; i < wayPoint.Length; i++)
        {
            wayPoint[i] = gameObject.transform.GetChild(i);
        }

        wayPointPosition = new Vector3[wayPoint.Length];


        for (int i = 0; i < wayPointPosition.Length; i++)
        {
            wayPointPosition[i] = wayPoint[i].position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!canMove)
            return;

        transform.position = Vector2.MoveTowards(transform.position, wayPointPosition[wayPointIndex], moveSpeed * Time.deltaTime);

        if(Vector2.Distance(transform.position, wayPointPosition[wayPointIndex]) < .1f)
        {
            if(wayPointIndex == wayPointPosition.Length - 1 || wayPointIndex == 0)
            {
                moveDir = moveDir * -1;
                StartCoroutine(StopMovement());
            }

            wayPointIndex = wayPointIndex + moveDir;
        }
    }

    private IEnumerator StopMovement()
    {
        canMove = false;
    
        yield return new WaitForSeconds(idleTime);

        canMove = true;
        sr.flipX = !sr.flipX;

    }
}
