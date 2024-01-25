using System.Collections;
using UnityEngine;

namespace TbsFramework.Units.Highlighters
{
    public class GlowHighlighter : UnitHighlighter
    {
        public Color Color;
        public float CooloutTime;
        public Renderer Renderer;

        public override void Apply(Unit unit, Unit otherUnit)
        {
            StartCoroutine(Glow());
        }

        private IEnumerator Glow()
        {
            float startTime = Time.time;
            var baseColor = Renderer.material.color;

            while (startTime + CooloutTime > Time.time)
            {
                Renderer.material.color = Color.Lerp(baseColor, Color, (startTime + CooloutTime) - Time.time);
                yield return 0;
            }

            Renderer.material.color = baseColor;
        }
    }
}