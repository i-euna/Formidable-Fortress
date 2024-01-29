using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class CannonController : MonoBehaviour
{
    [Tooltip("Cannon pool")]
    [SerializeField]
    private ObjectPoolVariable CannonPool;

    [Tooltip("Original Cannon")]
    [SerializeField]
    private GameObject CannonPrefab;
    //List of cannons
    private Stack FiredCannons;
    [Tooltip("Max no of cannon in the pool")]
    [SerializeField]
    private IntVariable MaxNoOfCannon;

    private Vector3 InitialPos;


    void Start()
    {
        FiredCannons = new Stack();
        //Initialize the pool
        CannonPool.ObjectPool = new ObjectPool<GameObject>(() =>
        { return Instantiate(CannonPrefab); },
        dice => { dice.SetActive(true); },
        dice => { dice.SetActive(false); },
        dice => { Destroy(dice); },
        false,
        MaxNoOfCannon.Value,
        MaxNoOfCannon.Value
        );
        InitialPos = CannonPrefab.transform.position;
        PrepareNextCannon();
    }

    public void PrepareNextCannon() {
        GameObject newCannon = CannonPool.ObjectPool.Get();
        
        newCannon.transform.position = InitialPos;

        newCannon.SetActive(true);
    }
}
