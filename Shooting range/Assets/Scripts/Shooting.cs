using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    private Vector3 mousePos;
    private bool shoot = true;
    void Update()
    {
        if(Input.GetKey(KeyCode.Mouse0) && shoot)
        {
            shoot = false;
            float x = Input.mousePosition.x - Screen.width * 0.5f;
            float y = Input.mousePosition.y - Screen.height * 0.5f;
            mousePos = new Vector3(x * Time.deltaTime, y * Time.deltaTime, 0);
            transform.LookAt(mousePos);
            RaycastHit hit;
            Ray ray = new Ray(transform.position, transform.TransformDirection(Vector3.forward));
            if(Physics.Raycast(ray, out hit) && hit.transform.gameObject.CompareTag("Enemy"))
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                hit.transform.gameObject.SetActive(false);
                WorldController.instance.AddEnemy();
            }

        }
        shoot = true;
    }
}
