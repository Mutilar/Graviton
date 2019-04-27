using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour {

    public float top;
    void Update()
    {
        this.transform.Translate(0, 5f * Time.deltaTime, 0);
        if (this.transform.position.y > 10)
            this.transform.position = new Vector2(0, -5f);
        top = this.transform.position.y;
    }

}
