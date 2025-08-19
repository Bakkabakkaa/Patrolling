using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class Patrolling : MonoBehaviour
{
    [SerializeField]
    private int numberOfPoints;
    
    [SerializeField]
    private float speed = 3f;

    [SerializeField] 
    private float waitingTime = 1.5f;
    
    private GameObject _plane;
    private List<GameObject> _points = new List<GameObject>();
    private int _currentNumber = 0;
    private float _waitTimer = 0f;


    private void Start()
    {
        _plane = GameObject.Find("Plane");
        Vector3 planeSize = _plane.GetComponent<Renderer>().bounds.size;
        float width = planeSize.x;
        float length = planeSize.z;
        
        
        for (int i = 0; i < numberOfPoints; i++)
        {
            float x = Random.Range(-(width / 2), width / 2);
            float z = Random.Range(-(length / 2), length / 2);
            Vector3 pointPosition = new Vector3(x, _plane.transform.position.y + 0.1f, z);
            GameObject point = new GameObject("Point " + i);
            point.transform.position = pointPosition;
            
            _points.Add(point);
        }
    }

    private void Update()
    {
        if (_waitTimer > 0)
        {
            _waitTimer -= Time.deltaTime;
            return;
        }

        Vector3 target = _points[_currentNumber].transform.position;
        target.y = transform.position.y;
        
        transform.position = Vector3.MoveTowards(
            transform.position, 
            target,
            speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target) < 0.05f)
        {
            _currentNumber = (_currentNumber + 1) % _points.Count;
            _waitTimer = waitingTime;
        }
    }
}
