using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class DragAndDrop : MonoBehaviour
{
    bool moveAllowed;
    Collider2D col;
    public GameObject selectionEffect;
    public GameObject deathEffect;
    Camera _cam;
    Vector3 camStartPos;
    void Start()
    {
        _cam = Camera.main;
        camStartPos = _cam.gameObject.transform.position;
        col = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        MouseDragAndDrop(); //For PC play
        TouchDragAndDrop(); //For Mobile Play
    }
    void MouseDragAndDrop()
    {
        if (Input.GetMouseButton(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D touchCol = Physics2D.OverlapPoint(mousePos);
            if (col == touchCol)
            {
                Instantiate(selectionEffect, transform.position, Quaternion.identity);
                moveAllowed = true;
            }

            if (moveAllowed)
            {
                transform.position = new Vector2(mousePos.x, mousePos.y);
            }
        }

        if (Input.GetMouseButtonUp(0))
            moveAllowed = false;

    }
    void TouchDragAndDrop()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 touchPos = Camera.main.ScreenToWorldPoint(touch.position);

            if (touch.phase == TouchPhase.Began)
            {
                Collider2D touchCol = Physics2D.OverlapPoint(touchPos);
                if (col == touchCol)
                {
                    moveAllowed = true;
                }
            }

            if (touch.phase == TouchPhase.Moved)
            {
                if (moveAllowed)
                {
                    transform.position = new Vector2(touchPos.x, touchPos.y);
                }
            }

            if (touch.phase == TouchPhase.Ended)
            {
                moveAllowed = false;
            }
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(deathEffect == null)
        {
            Debug.LogWarning("Death effect is not assigned in the inspector.");
            return;
        }
        if (collision.tag == "Player")
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
            StartCoroutine(CameraShake(.1f));
        }
    }
    

    IEnumerator CameraShake(float mag)
    {
        float timer = 0.75f;

        while (timer > 0)
        {
            timer -= Time.deltaTime;
            float randomMag = Random.Range(-mag, mag);
            _cam.transform.position = new Vector3(camStartPos.x + randomMag, camStartPos.y + randomMag, camStartPos.z);
            yield return null;
        }

        _cam.transform.position = camStartPos;
        yield return new WaitForSeconds(0.5f);
        // After the camera shake, call the GameOver method
        GameManager.Instance.GameOver(); // Call the GameOver method from GameManager
    }
}
