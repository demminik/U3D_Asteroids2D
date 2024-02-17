using UnityEngine;

namespace Utils {

    public static class MathUtils {

        public static float MathematicalMod(float n, float d) {
            var result = n % d;
            if (Mathf.Sign(result) * Mathf.Sign(d) < 0) {
                result += d;
            }
            return result;
        }
    }
}