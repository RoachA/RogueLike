using UnityEngine;

public class EntityDynamic : EntityBase
{
    [SerializeField] protected bool _isAlive;

    public virtual void MoveEntity(Vector2Int direction)
    {
        Debug.LogError("move move bitch");
        //todo target pos must be validated beforehand.
        transform.localPosition += new Vector3(direction.x, direction.y, transform.localPosition.z);
    }
}
