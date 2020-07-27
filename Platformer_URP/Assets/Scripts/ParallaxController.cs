using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxController : MonoBehaviour
{
    public Transform[] parallaxElements;
    public float[] parallaxScales;
    public float smoothing;

    private Transform cam;
    private Vector3 previousCamPos;

    private void Awake()
    {
        cam = Camera.main.transform;
    }

    private void Start()
    {
        previousCamPos = cam.position;

        parallaxScales = new float[parallaxElements.Length];

        for (int i = 0; i < parallaxElements.Length; i++)
        {
            parallaxScales[i] = parallaxElements[i].position.z * -1;
        }
    }

    private void Update()
    {
        for (int i = 0; i < parallaxElements.Length; i++)
        {
            float parallax = (previousCamPos.x - cam.position.x) * parallaxScales[i];

            float backgroundTargetPosX = parallaxElements[i].position.x + parallax;

            Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX, parallaxElements[i].position.y, parallaxElements[i].position.z);

            parallaxElements[i].position = Vector3.Lerp(parallaxElements[i].position, backgroundTargetPos, smoothing * Time.deltaTime);
        }

        previousCamPos = cam.position;
    }

}
