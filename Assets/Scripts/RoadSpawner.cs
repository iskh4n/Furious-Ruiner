using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadSpawner : MonoBehaviour
{
    private float newZ= 258.525f;
    public GameObject yol1;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" )
        {
            gameObject.GetComponent<Collider>().enabled = false; // Collider'ı devre dışı bırak
            yol1.transform.position += new Vector3(0, 0, newZ*3);
            Debug.Log("yol collider calisiyor, player geçti");
            Invoke(nameof(EnableCollider), 2f); // 2 
        }
    }
    private void EnableCollider()
    {
        gameObject.GetComponent<Collider>().enabled = true; // Collider'ı tekrar etkin hale getir
        Debug.Log("collider aktif oldu");

    }
}
