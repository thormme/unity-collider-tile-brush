using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
using static UnityEngine.Tilemaps.Tile;

namespace UnityEditor.Tilemaps
{
    /// <summary>
    /// This Brush changes the ColliderType of Tiles placed on a Tilemap.
    /// </summary>
    [CustomGridBrush(true, false, false, "Collider Brush")]
    public class ColliderBrush : GridBrushBase
    {
        /// <summary>
        /// The collider type to set the tile to.
        /// </summary>
        public ColliderType m_colliderType;
        /// <summary>
        /// When set to true the collider type will be saved to the base tile.
        /// This will effect every tile painted using the tile.
        /// </summary>
        public bool m_modifyBaseTile = false;

        /// <summary>
        /// Sets the ColliderType of the painted tile to the selected type
        /// </summary>
        /// <param name="grid">Grid used for layout.</param>
        /// <param name="brushTarget">Target of the paint operation. By default the currently selected GameObject.</param>
        /// <param name="position">The coordinates of the cell to paint data to.</param>
        public override void Paint(GridLayout grid, GameObject brushTarget, Vector3Int position)
        {
            Tilemap tilemap = brushTarget.GetComponent<Tilemap>();
            if (tilemap != null)
            {
                SetCollider(tilemap, position, m_colliderType, m_modifyBaseTile);
            }
        }

        /// <summary>
        /// Sets the tile collider type to None
        /// </summary>
        /// <param name="grid">Grid used for layout.</param>
        /// <param name="brushTarget">Target of the erase operation. By default the currently selected GameObject.</param>
        /// <param name="position">The coordinates of the cell to erase data from.</param>
        public override void Erase(GridLayout grid, GameObject brushTarget, Vector3Int position)
        {
            Tilemap tilemap = brushTarget.GetComponent<Tilemap>();
            if (tilemap != null)
            {
                SetCollider(tilemap, position, ColliderType.None, m_modifyBaseTile);
            }
        }

        private static void SetCollider(Tilemap tilemap, Vector3Int position, ColliderType colliderType, bool modifyBaseTile)
        {
            TileBase tile = tilemap.GetTile(position);
            if (tile != null)
            {
                var tileCast = tile as Tile;
                if (modifyBaseTile == true && tileCast != null)
                {
                    tileCast.colliderType = colliderType;
                    EditorUtility.SetDirty(tileCast);
                }
                
                tilemap.SetColliderType(position, colliderType);
            }
        }
    }

    /// <summary>
    /// The Brush Editor for the Collider Brush.
    /// </summary>
    [CustomEditor(typeof(ColliderBrush))]
    public class ColliderBrushEditor : GridBrushEditorBase
    {
        /// <summary>Returns all valid targets that the brush can edit.</summary>
        /// <remarks>Valid targets for the ColliderBrush are any GameObjects with a Tilemap component.</remarks>
        public override GameObject[] validTargets
        {
            get
            {
                return GameObject.FindObjectsOfType<Tilemap>().Select(x => x.gameObject).ToArray();
            }
        }


        /// <summary>
        /// Callback for painting the GUI for the GridBrush in the Scene View.
        /// The ColliderBrush Editor overrides this to draw the ColliderType of the hovered Tile.
        /// </summary>
        /// <param name="grid">Grid that the brush is being used on.</param>
        /// <param name="brushTarget">Target of the GridBrushBase::ref::Tool operation. By default the currently selected GameObject.</param>
        /// <param name="position">Current selected location of the brush.</param>
        /// <param name="tool">Current GridBrushBase::ref::Tool selected.</param>
        /// <param name="executing">Whether brush is being used.</param>
        public override void OnPaintSceneGUI(GridLayout grid, GameObject brushTarget, BoundsInt position, GridBrushBase.Tool tool, bool executing)
        {
            base.OnPaintSceneGUI(grid, brushTarget, position, tool, executing);

            Tilemap tilemap = brushTarget.GetComponent<Tilemap>();
            var colliderType = tilemap.GetColliderType(position.position);
            var labelText = "";
            switch (colliderType) {
                case ColliderType.Grid:
                    labelText += "Grid";
                    break;
                case ColliderType.None:
                    labelText += "None";
                    break;
                case ColliderType.Sprite:
                    labelText += "Sprite";
                    break;

            }

            Handles.Label(grid.CellToWorld(position.position), labelText);
        }
    }
}
