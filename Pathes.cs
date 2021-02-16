using System.Collections.Generic;
using System.IO;
using LeagueSharp;
using SharpDX;

namespace PROdiction
{
    public class OnNewPathEvent
    {
        public double GameTime;
        public Vector3[] Path;
        public bool IsDash;
        public Vector3 Velocity;
    }
    public class Pathes
    {
        public static Dictionary<int, List<OnNewPathEvent>> LastNewPathes =
            new Dictionary<int, List<OnNewPathEvent>>();

        static Pathes()
        {
            Obj_AI_Base.OnNewPath += Obj_AI_BaseOnOnNewPath;
        }
        
        private static void Obj_AI_BaseOnOnNewPath(Obj_AI_Base sender, GameObjectNewPathEventArgs args)
        {
            if (!LastNewPathes.ContainsKey(sender.NetworkId))
            {
                LastNewPathes[sender.NetworkId] = new List<OnNewPathEvent>();
            }
            
            var champArray = LastNewPathes[sender.NetworkId];

            champArray.Add(new OnNewPathEvent
            {
                Path = args.Path,
                GameTime = Game.Time,
                IsDash = args.IsDash,
                Velocity = sender.Velocity
            });

            while (champArray.Count > 50)
            {
                champArray.RemoveAt(0);
            }
        }
    }
}