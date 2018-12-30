using UnityEngine;
using System.Collections.Generic;

namespace it.amalfi.Pearl
{
    /// <summary>
    /// This class takes care of creating useful functions for manage layer masks
    /// </summary>
    public static class LayerExtend
    {
        private static readonly byte numberCells = 32;

        #region Public Methods
        /// <summary>
        /// Creates the new layer mask through layer-strings
        /// </summary>
        /// <param name = "layers"> The layers (in string) that will be added</param>
        public static LayerMask CreateLayerMask(params string[] layers)
        {
            LayerMask layerMask = (LayerMask)0;
            foreach (string name in layers)
                layerMask |= (1 << LayerMask.NameToLayer(name));
            return layerMask;
        }
        #endregion

        #region Extend Methods
        /// <summary>
        /// Creates the inverse layer mask
        /// </summary>
        /// <param name = "layerMask"> The layerMask that will be reversed</param>
        public static LayerMask Inverse(this LayerMask layerMask)
        {
            return ~layerMask;
        }

        /// <summary>
        /// Add new layers (string-layers) in the layer mask.
        /// </summary>
        /// <param name = "layerMask"> The layerMask in which the layers will be added</param>
        /// <param name = "layers"> The layers (in string) that will be added</param>
        public static LayerMask AddToMask(this LayerMask layerMask, params string[] layers)
        {
            return layerMask | CreateLayerMask(layers);
        }

        /// <summary>
        /// Remove layers (string-layers) in the layer mask.
        /// </summary>
        /// <param name = "layerMask"> The layerMask in which the layers will be removed</param>
        /// <param name = "layers"> The layers (in string) that will be removed</param>
        public static LayerMask RemoveFromMask(this LayerMask layerMask, params string[] layers)
        {
            LayerMask invertedOriginal = ~layerMask;
            return ~(invertedOriginal | CreateLayerMask(layers));
        }

        /// <summary>
        /// Return layerString from layer mask
        /// </summary>
        /// <param name = "layerMask"> The layerMask that contains the layers</param>
        public static string[] LayersInMask(this LayerMask layerMask)
        {
            var output = new List<string>();

            for (int i = 0; i < numberCells; ++i)
            {
                int shifted = 1 << i;
                if ((layerMask & shifted) == shifted)
                {
                    string layerName = LayerMask.LayerToName(i);
                    if (!string.IsNullOrEmpty(layerName))
                        output.Add(layerName);
                }
            }
            return output.ToArray();
        }
        #endregion
    }
}
