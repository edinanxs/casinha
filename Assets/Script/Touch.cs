using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Touch : MonoBehaviour
{
    private float startPosX;
    private float startPosY;
    private float startPosZ;
    private bool isBeingHeld = false;


    private void Update()
    {

        if (isBeingHeld == true)
        {

            Vector3 mousePos;
            mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);

            this.gameObject.transform.localPosition = new Vector3(mousePos.x - startPosX, mousePos.y - startPosY, startPosZ);
        }

    }

    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {

            Vector3 mousePos;
            mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);

            startPosX = mousePos.x - this.transform.localPosition.x;
            startPosY = mousePos.y - this.transform.localPosition.y;
            startPosZ = this.transform.localPosition.z;
            isBeingHeld = true;
            Events.item = this.gameObject;
        }

    }

    private void OnMouseUp()
    {
        isBeingHeld = false;
    }
}
