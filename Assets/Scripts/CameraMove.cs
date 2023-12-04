using DG.Tweening;
using UnityEngine;


public class CameraMove : MonoBehaviour
{
      private Camera c;
      
      private int maxView = 12;
      private int minView = 1;
      



    private void Start()
     {
        c = this.GetComponent<Camera>();
        DOTween.SetTweensCapacity(tweenersCapacity: 800, sequencesCapacity: 200);
        
    }


    private void Update()
    {


        Move();


    }
    private void Move()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.DOLocalMove(transform.position + new Vector3(0, 0.5f, 0), 0.1f);


        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.DOLocalMove(transform.position + new Vector3(0, -0.5f, 0), 0.1f);

        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.DOLocalMove(transform.position + new Vector3(-0.5f, 0, 0), 0.1f);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.DOLocalMove(transform.position + new Vector3(0.5f, 0, 0), 0.1f);
        }
        if (Input.GetKey(KeyCode.E))
        {
            if (c.orthographicSize < maxView)
            {
                c.orthographicSize -= 0.015f;


            }


        }
        if (Input.GetKey(KeyCode.Q))
        {
            if (c.orthographicSize > minView)
            {
                c.orthographicSize += 0.015f;
            }
            //Debug.Log(c.fieldOfView);
        }
    }
}
