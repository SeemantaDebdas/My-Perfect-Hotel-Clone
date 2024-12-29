using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace MPH.Saving
{
    public interface ISaveable
    {
        /// <summary>
        /// Returns a JToken representing the state of the module/ISaveable
        /// </summary>
        /// <returns>A JToken equivalent to object in C#</returns>
        JToken CaptureAsJToken();

        /// <summary>
        /// Restores state of the module/ISaveable from the JToken
        /// </summary>
        /// <param name="state">The JToken contating data to be restored.</param>
        void RestoreFromJToken(JToken state);
    }

    public static class JsonStatics
    {

        public static JToken ToToken(this Vector3 vector)
        {
            JObject state = new JObject();
            IDictionary<string, JToken> stateDict = state;
            stateDict["x"] = vector.x;
            stateDict["y"] = vector.y;
            stateDict["z"] = vector.z;
            return state;
        }

        public static Vector3 ToVector3(this JToken state)
        {
            Vector3 vector = new Vector3();
            if (state is JObject jObject)
            {
                IDictionary<string, JToken> stateDict = jObject;

                if (stateDict.TryGetValue("x", out JToken x))
                {
                    vector.x = x.ToObject<float>();
                }

                if (stateDict.TryGetValue("y", out JToken y))
                {
                    vector.y = y.ToObject<float>();
                }

                if (stateDict.TryGetValue("z", out JToken z))
                {
                    vector.z = z.ToObject<float>();
                }
            }
            return vector;
        }
    }

}
