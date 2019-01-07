using UnityEngine;
using System.Collections.Generic;

namespace Pearl
{
    /// <summary>
    /// This class takes care of creating useful functions for manage the layer masks
    /// </summary>
    public static class LayerExtend
    {
        private static List<string> auxListString;
        private static readonly byte numberCells = 32;
		
		static LayerExtend()
        {
            auxListString = new List<string>();
        }

        #region Public Methods
        /// <summary>
        /// Creates the new layer mask through layer-strings
        /// </summary>
        /// <param name = "layers"> The layers (in string) that will be added</param>
        public static LayerMask CreateLayerMask(params string[] layers)
        {
            LayerMask layerMask = (LayerMask)0;
            for (int i = 0; i < layers.Length; i++)
                layerMask |= (1 << LayerMask.NameToLayer(layers[i]));
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
            return ~(~layerMask | CreateLayerMask(layers));
        }

        /// <summary>
        /// Return layerString from layer mask
        /// </summary>
        /// <param name = "layerMask"> The layerMask that contains the layers</param>
        public static string[] LayersInMask(this LayerMask layerMask)
        {
            auxListString.Clear();

            for (int i = 0; i < numberCells; ++i)
            {
                int shifted = 1 << i;
                if ((layerMask & shifted) == shifted)
                {
                    if (!string.IsNullOrEmpty(LayerMask.LayerToName(i)))
                        auxListString.Add(LayerMask.LayerToName(i));
                }
            }
            return auxListString.ToArray();
        }
        #endregion
    }
}
