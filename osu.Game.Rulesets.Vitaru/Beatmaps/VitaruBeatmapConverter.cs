using OpenTK;
using osu.Game.Beatmaps;
using osu.Game.Rulesets.Vitaru.Objects;
using osu.Game.Rulesets.Vitaru.Objects.Drawables;
using System.Collections.Generic;
using System.Linq;
using osu.Game.Rulesets.Vitaru.Objects.Characters;
using osu.Game.Rulesets.Beatmaps;
using osu.Game.Rulesets.Objects;
using System;
using osu.Game.Rulesets.Objects.Types;

namespace osu.Game.Rulesets.Vitaru.Beatmaps
{
    internal class VitaruBeatmapConverter : BeatmapConverter<VitaruHitObject>
    {
        public bool playerLoaded = false;

        protected override IEnumerable<Type> ValidConversionTypes
        {
            get
            {
                throw new NotImplementedException();
            }
        }

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
            IHasEndTime endTimeData = original as IHasEndTime;
            IHasPosition positionData = original as IHasPosition;
            IHasCombo comboData = original as IHasCombo;

            if (playerLoaded == false)
            {
                playerLoaded = true;
                DrawableVitaruPlayer.PlayerPosition = new Vector2(256, 612);
                return new VitaruPlayer
                {
                    StartTime = 0f,

                    Position = positionData?.Position ?? Vector2.Zero,

                    NewCombo = comboData?.NewCombo ?? false,
                };
            }
            return new Enemy
            {
                StartTime = original.StartTime,

                Position = positionData?.Position ?? Vector2.Zero,

                NewCombo = comboData?.NewCombo ?? false
            };
        }

        private void updateStacking(List<VitaruHitObject> hitObjects, float stackLeniency, int startIndex = 0, int endIndex = -1)
        {
            if (endIndex == -1)
                endIndex = hitObjects.Count - 1;

            int stackDistance = 3;
            float stackThreshold = DrawableVitaruHitObject.TIME_PREEMPT * stackLeniency;

            // Reset stacking inside the update range
            for (int i = startIndex; i <= endIndex; i++)
                hitObjects[i].StackHeight = 0;

            // Extend the end index to include objects they are stacked on
            int extendedEndIndex = endIndex;
            for (int i = endIndex; i >= startIndex; i--)
            {
                int stackBaseIndex = i;
                for (int n = stackBaseIndex + 1; n < hitObjects.Count; n++)
                {
                    VitaruHitObject stackBaseObject = hitObjects[stackBaseIndex];

                    VitaruHitObject objectN = hitObjects[n];

                    double endTime = (stackBaseObject as IHasEndTime)?.EndTime ?? stackBaseObject.StartTime;

                    if (objectN.StartTime - endTime > stackThreshold)
                        //We are no longer within stacking range of the next object.
                        break;
                }

                if (stackBaseIndex > extendedEndIndex)
                {
                    extendedEndIndex = stackBaseIndex;
                    if (extendedEndIndex == hitObjects.Count - 1)
                        break;
                }
            }

            //Reverse pass for stack calculation.
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

                        if (objectI.StartTime - endTime > stackThreshold)
                            //We are no longer within stacking range of the previous object.
                            break;

                        // HitObjects before the specified update range haven't been reset yet
                        if (n < extendedStartIndex)
                        {
                            objectN.StackHeight = 0;
                            extendedStartIndex = n;
                        }

                        if (Vector2.Distance(objectN.Position, objectI.Position) < stackDistance)
                        {
                            objectN.StackHeight = objectI.StackHeight + 1;
                            objectI = objectN;
                        }
                    }
                }
            }
        }

        protected override IEnumerable<VitaruHitObject> ConvertHitObject(HitObject original, Beatmap beatmap)
        {
            throw new NotImplementedException();
        }
    }
}