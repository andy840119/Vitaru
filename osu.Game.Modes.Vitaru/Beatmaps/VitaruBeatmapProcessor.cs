using osu.Game.Beatmaps;
using osu.Game.Modes.Vitaru.Objects;

namespace osu.Game.Modes.Vitaru.Beatmaps
{
    internal class VitaruBeatmapProcessor : IBeatmapProcessor<VitaruHitObject>
    {
        public void PostProcess(Beatmap<VitaruHitObject> beatmap)
        {
            if (beatmap.ComboColors.Count == 0)
                return;

            int comboIndex = 0;
            int colourIndex = 0;

            foreach (var obj in beatmap.HitObjects)
            {
                if (obj.NewCombo)
                {
                    comboIndex = 0;
                    colourIndex = (colourIndex + 1) % beatmap.ComboColors.Count;
                }

                obj.ComboIndex = comboIndex++;
                obj.ComboColour = beatmap.ComboColors[colourIndex];
            }
        }
    }
}
