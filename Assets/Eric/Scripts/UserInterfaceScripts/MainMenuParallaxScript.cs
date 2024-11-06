using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuParallaxScript : MonoBehaviour
{
    [SerializeField] private bool invertParallaxDirection = false;


    //script by Mirza and altered by me
    [Header("Position Parallax")]
    public float positionParallax = 1.0f;
    public Vector2 positionParallaxScale = Vector2.one;

    [Space]

    public float positionLerpSpeed = 10.0f;



    private void Awake()
    {
        Time.timeScale = 1.0f;
    }


    void Update()
    {
        print("sigma");

        // Remap to [0.0, 1.0].
        // Mouse position across screen.

        Vector2 normalizedMousePosition = new(Mathf.Clamp01(Input.mousePosition.x / Screen.width), Mathf.Clamp01(Input.mousePosition.y / Screen.height));

        // Remap to [-1.0, 1.0].

        normalizedMousePosition -= Vector2.one * 0.5f;
        normalizedMousePosition *= 2.0f;

        // Position.

        Vector2 targetPosition = Vector2.zero;

        if (!invertParallaxDirection)
        {
            targetPosition = -normalizedMousePosition * (positionParallaxScale * positionParallax);
        }
        else
        {
            targetPosition = normalizedMousePosition * (positionParallaxScale * positionParallax);
        }

        transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition, positionLerpSpeed * Time.deltaTime);

    }
}
