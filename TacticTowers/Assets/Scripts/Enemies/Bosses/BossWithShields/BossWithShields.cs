using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class BossWithShields : Boss
{
    [Header("BossShields")]
    [SerializeField] private List<GameObject> shieldPoints;
    [SerializeField] private List<GameObject> shields;
    [SerializeField] private List<ShieldSide> shieldSides;
    [SerializeField] private GameObject ropesParent;
    [SerializeField] private float floatingSpeed;
    [SerializeField] private float floatingSpread;
    [SerializeField] private float rotationDelay;
    private int currentShieldPositionIndex = -1;
    private List<Vector3> floatingDestinations = new List<Vector3>();
    private Transform[] moveDestinationTransforms = new Transform[3];
    
    [SerializeField] private float shieldMoveSpeed;
    [SerializeField] private float shieldRotationSpeed;
    
    void Start()
    {
        base.Start();
        
        for (var i = 0; i < shields.Count; i++)
        {
            floatingDestinations.Add(SelectRandomFloatingPosition());
        }
        StartCoroutine(RotateShields());
    }
    
    void Update()
    {
        if (isDead) 
            return;
        
        base.Update();
        
        for (var i = 0; i < shields.Count; i++)
        {
            FloatShields(i);
            MoveShields(i);
            RotateShields(i);
        }
        
        UpdateHp();
        /*
        if (Input.GetKeyDown(KeyCode.Alpha4))
            SetNewShieldPositions(0);
        if (Input.GetKeyDown(KeyCode.Alpha5))
            SetNewShieldPositions(1);
        if (Input.GetKeyDown(KeyCode.Alpha6))
            SetNewShieldPositions(2);
        if (Input.GetKeyDown(KeyCode.Alpha7))
            SetNewShieldPositions(3);*/
    }

    private void SetNewShieldPositions(int sideIndex)
    {
        var pickedIndexes = new List<int>();
        
        SetRandomMoveDestination(pickedIndexes, shieldSides[sideIndex].middlePoint.transform);
        SetRandomMoveDestination(pickedIndexes, shieldSides[sideIndex].rightPoint.transform);
        SetRandomMoveDestination(pickedIndexes, shieldSides[sideIndex].leftPoint.transform);
    }

    private void SetRandomMoveDestination(List<int> pickedIndexes, Transform point)
    {
        var destIndex = Random.Range(0, 3);
        
        while (pickedIndexes.Contains(destIndex))
        {
            destIndex = Random.Range(0, 3);
        }
        
        pickedIndexes.Add(destIndex);
        moveDestinationTransforms[destIndex] = point;
    }

    private void MoveShields(int shieldIndex)
    {
        if (!NeedToMoveShield(shieldIndex)) 
            return;

        shieldPoints[shieldIndex].transform.position = Vector3.MoveTowards(shieldPoints[shieldIndex].transform.position,
            moveDestinationTransforms[shieldIndex].position, shieldMoveSpeed * Time.deltaTime);
        
    }

    private void RotateShields(int shieldIndex)
    {
        if (!NeedToRotateShield(shieldIndex)) 
            return;
        
        shields[shieldIndex].transform.rotation = Quaternion.RotateTowards(shields[shieldIndex].transform.rotation,
            moveDestinationTransforms[shieldIndex].rotation, shieldRotationSpeed * Time.deltaTime);
    }

    private void FloatShields(int shieldIndex)
    {
        var dest = shieldPoints[shieldIndex].transform.position + floatingDestinations[shieldIndex];
        
        if (shields[shieldIndex].transform.position == dest)
        {
            floatingDestinations[shieldIndex] = SelectRandomFloatingPosition();
        }

        shields[shieldIndex].transform.position =
            Vector3.MoveTowards(shields[shieldIndex].transform.position, dest, floatingSpeed * Time.deltaTime);
    }

    private Vector3 SelectRandomFloatingPosition()
    {
        var additionalVec = new Vector3(Random.Range(0, floatingSpread), Random.Range(0, floatingSpread));
        return additionalVec;
    }

    private bool NeedToMoveShield(int shieldIndex)
    {
        return shieldPoints[shieldIndex].transform.position != moveDestinationTransforms[shieldIndex].position;
    }
    
    private bool NeedToRotateShield(int shieldIndex)
    {
        var areEqualish = Quaternion.Angle(shields[shieldIndex].transform.rotation,
            moveDestinationTransforms[shieldIndex].transform.rotation) < 0.001f;
        return !areEqualish;
    }

    private int PickNewShieldPosition()
    {
        var range = Enumerable.Range(0, shieldSides.Count).Where(i => i != currentShieldPositionIndex);
        var rand = new System.Random();
        var index = rand.Next(0, shieldSides.Count - 1);
        return range.ElementAt(index);
    }

    private IEnumerator RotateShields()
    {
        var newIndex = PickNewShieldPosition();
        
        SetNewShieldPositions(newIndex);
        currentShieldPositionIndex = newIndex;
        yield return new WaitForSeconds(rotationDelay);
        StartCoroutine(RotateShields());
    }

    protected override void BossDeath()
    {
        base.BossDeath();
        
        Destroy(ropesParent);
        
        var listCount = shieldPoints.Count;
        for (int i = 0; i < listCount; i++)
        {
            Destroy(shieldPoints[listCount - i - 1]);
        }
    }
}
