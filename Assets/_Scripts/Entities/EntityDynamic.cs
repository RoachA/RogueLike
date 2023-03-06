using UnityEngine;

public class EntityDynamic : EntityBase
{
    [SerializeField] protected bool _isAlive;

    public virtual void MoveEntity(Vector2Int direction)
    {
        var targetVector = new Vector3(direction.x, direction.y, transform.localPosition.z);
        var newPos = transform.localPosition + targetVector;
        transform.localPosition = newPos;
        _entityPos = new Vector2Int((int) newPos.x, (int) newPos.y); //todo check how to do this better.
    }
}
