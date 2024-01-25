using UnityEngine;

namespace TbsFramework.Units.Highlighters
{
    public class RendererHighlighter : UnitHighlighter
    {
        public Renderer Renderer;
        public Color Color;
        public override void Apply(Unit unit, Unit otherUnit)
        {
            Renderer.material.color = Color;
        }
    }
}