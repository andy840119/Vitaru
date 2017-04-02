using OpenTK;
using osu.Game.Beatmaps;
using osu.Game.Modes.Objects;
using osu.Game.Modes.Vitaru.Objects;
using osu.Game.Modes.Vitaru.Objects.Drawables;
using System.Collections.Generic;
using osu.Game.Modes.Objects.Types;
using System.Linq;
using osu.Game.Modes.Vitaru.Objects.Characters;

namespace osu.Game.Modes.Vitaru.Beatmaps
{
    internal class VitaruBeatmapConverter : IBeatmapConverter<VitaruHitObject>
    {
        public Beatmap<VitaruHitObject> Convert(Beatmap original)
        {
            return new Beatmap<VitaruHitObject>(original)
            {
                HitObjects = convertHitObjects(original.HitObjects, original.BeatmapInfo?.StackLeniency ?? 0.7f)
            };
        }

        private List<VitaruHitObject> convertHitObjects(List<HitObject> hitObjects, float stackLeniency)
        {
            List<VitaruHitObject> converted = hitObjects.Select(convertHitObject).ToList();

            return converted;
        }

        private VitaruHitObject convertHitObject(HitObject original)
        {
            IHasCurve curveData = original as IHasCurve;
            IHasEndTime endTimeData = original as IHasEndTime;
            IHasPosition positionData = original as IHasPosition;
            IHasCombo comboData = original as IHasCombo;

            if (VitaruHitRenderer.playerLoaded == false)
            {
                VitaruHitRenderer.playerLoaded = true;
                return new VitaruPlayer
                {
                    StartTime = original.StartTime,
                    Sample = original.Sample,

                    Position = positionData?.Position ?? Vector2.Zero,

                    NewCombo = comboData?.NewCombo ?? false,
                };
            }
            return new Enemy
            {
                StartTime = original.StartTime,
                Sample = original.Sample,

                Position = positionData?.Position ?? Vector2.Zero,

                NewCombo = comboData?.NewCombo ?? false
            };
        }

        private void updateStacking(List<VitaruHitObject> hitObjects, float stackLeniency, int startIndex = 0, int endIndex = -1)
        {
            if (endIndex == -1)
                endIndex = hitObjects.Count - 1;

            int extendedEndIndex = endIndex;

            int extendedStartIndex = startIndex;
            for (int i = extendedEndIndex; i > startIndex; i--)
            {
                int n = i;

                VitaruHitObject objectI = hitObjects[i];

                if (objectI is Enemy)
                {
                    while (--n >= 0)
                    {
                        VitaruHitObject objectN = hitObjects[n];

                        double endTime = (objectN as IHasEndTime)?.EndTime ?? objectN.StartTime;

                        // HitObjects before the specified update range haven't been reset yet
                        if (n < extendedStartIndex)
                        {
                            extendedStartIndex = n;
                        }
                    }
                }
            }
        }
    }
}
