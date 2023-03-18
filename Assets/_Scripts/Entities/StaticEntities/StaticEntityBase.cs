using Game.Tiles;
using UnityEngine;
using Game.Utils;

namespace Game.Entites
{
    public class StaticEntityBase : MonoBehaviour
    {
        [SerializeField] protected string _identifier;
        [SerializeField] protected TileBase _occupiedTile;
        [SerializeField] protected SpriteRenderer _spriteRenderer;
        
        protected virtual void SetSprite(Sprite sprite)
        {
            _spriteRenderer.sprite = sprite;
        }
        
        public Vector2Int GetEntityPos()
        {
            return _occupiedTile.GetTilePosId();
        }
        
        public void SetEntityPos(TileBase targetTile)
        {
            transform.localPosition = targetTile.GetTilePosId().ConvertVectorToVector3(0);
            SetOccupiedTile(targetTile);
        }
        
        public TileBase GetOccupiedTile()
        {
            return _occupiedTile;
        }
        
        public void SetOccupiedTile(TileBase targetTile)
        {
            _occupiedTile = targetTile;
        }
    }
}
