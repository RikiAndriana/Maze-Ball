using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTouchController : MonoBehaviour
{

    [SerializeField, Range(0, 20)] float filterFactor = 1;
    [SerializeField, Range(0, 10)] float dragFactor = 1;
    [SerializeField, Range(0, 2)] float zoomFactor = 1;

    [Tooltip("equal camera y position")]
    [SerializeField] float minCamPos = 70;
    [SerializeField] float maxCamPos = 170;
    [SerializeField] Collider topCollider;
    float distance;
    void Start()
    {
        distance = this.transform.position.y;
    }

    Vector3 toucBeganWorldPos;
    Vector3 cameraBeganWorldPos;
    void Update()
    {
        if (Input.touchCount == 0)
            return;

        var touch0 = Input.GetTouch(0);

        // Debug.DrawLine(this.transform.position, Camera.main.ScreenToWorldPoint(
        //     new Vector3(touch0.position.x, touch0.position.y, distance)));

        // atur posisi sekarang sesuai perubahan dari posisi began
        if (Input.touchCount == 1 && touch0.phase == TouchPhase.Moved)
        {
            // ambil touchworld position pada sebelum bergerak / frame sebelumnya
            var touchPrevPos = touch0.position - touch0.deltaPosition;
            var touchPrevWorldPos = Camera.main.ScreenToWorldPoint(
                new Vector3(touchPrevPos.x, touchPrevPos.y, distance));

            // posisi touch (world space) saat ini
            var touchMovedWorldPos = Camera.main.ScreenToWorldPoint(
                new Vector3(touch0.position.x, touch0.position.y, distance));
            Debug.Log(touchMovedWorldPos);

            // perbedaan posisi (world space) dari frame sebelumnya
            var delta = touchMovedWorldPos - touchPrevWorldPos;

            var targetPos = this.transform.position - delta * dragFactor;

            // clamp targetPos
            targetPos = new Vector3(
                Mathf.Clamp(targetPos.x, topCollider.bounds.min.x, topCollider.bounds.max.x),
                targetPos.y,
                Mathf.Clamp(targetPos.z, topCollider.bounds.min.z, topCollider.bounds.max.z)
            );

            this.transform.position = targetPos;
        }

        if (Input.touchCount < 2)
            return;

        var touch1 = Input.GetTouch(1);

        // zoom
        if (touch0.phase == TouchPhase.Moved || touch1.phase == TouchPhase.Moved)
        {
            var touch0PrevPos = touch0.position - touch0.deltaPosition;
            var touch1PrevPos = touch1.position - touch1.deltaPosition;
            var prevDistance = Vector3.Distance(touch0PrevPos, touch1PrevPos);
            var currentDistance = Vector3.Distance(touch0.position, touch1.position);
            var delta = currentDistance - prevDistance;

            this.transform.position -= new Vector3(0, delta * zoomFactor, 0);
            // batasi zoom
            this.transform.position = new Vector3(
                this.transform.position.x,
                Mathf.Clamp(this.transform.position.y, minCamPos, maxCamPos),
                this.transform.position.z
            );
            distance = this.transform.position.y;
        }
    }
}
