using System;
using UnityEngine;
using DecorationSystem.CommonUtilities;

namespace DecorationSystem.MaterialUtilities {

    public static class MaterialUtilities {

        /// <summary>
        /// Sets the scale of material.
        /// </summary>
        /// <param name="targetMaterial">The target material.</param>
        /// <param name="scaleVector">The scale vector.</param>
        public static void SetScaleOfMaterial(Material targetMaterial, Vector2 scaleVector) {
            targetMaterial.mainTextureScale = scaleVector;
        }

        public static Material SetMainScale(this Material material, Vector2 scaleVector) {
            SetScaleOfMaterial(material, scaleVector);
            return material;
        }

        /// <summary>
        /// Sets the texture of material.
        /// </summary>
        /// <param name="targetMaterial">The target material.</param>
        /// <param name="newTexture">The new texture.</param>
        public static void SetTextureOfMaterial(Material targetMaterial, Texture newTexture) {
            targetMaterial.mainTexture = newTexture;
        }

        /// <summary>
        /// Sets the main texture.
        /// </summary>
        /// <param name="material">The material.</param>
        /// <param name="texture">The texture.</param>
        /// <returns></returns>
        public static Material SetMainTexture(this Material material, Texture texture) {
            SetTextureOfMaterial(material, texture);
            return material;
        }

        /// <summary>
        /// Sets the offset of material.
        /// </summary>
        /// <param name="targetMaterial">The target material.</param>
        /// <param name="offsetVector">The offset vector.</param>
        public static void SetOffsetOfMaterial(Material targetMaterial, Vector2 offsetVector) {
            targetMaterial.mainTextureOffset = offsetVector;
        }

        /// <summary>
        /// Sets the main offset.
        /// </summary>
        /// <param name="material">The material.</param>
        /// <param name="offsetVector">The offset vector.</param>
        /// <returns></returns>
        public static Material SetMainOffset(this Material material, Vector2 offsetVector) {
            SetOffsetOfMaterial(material, offsetVector);
            return material;
        }

        /// <summary>
        /// Sets the color of material.
        /// </summary>
        /// <param name="targetMaterial">The target material.</param>
        /// <param name="color">The color.</param>
        public static void SetColorOfMaterial(Material targetMaterial, Color color) {
            targetMaterial.color = color;
        }

        /// <summary>
        /// Sets the color of the main.
        /// </summary>
        /// <param name="material">The material.</param>
        /// <param name="newColor">The new color.</param>
        /// <returns></returns>
        public static Material SetMainColor(this Material material, Color newColor) {
            SetColorOfMaterial(material, newColor);
            return material;
        }

        /// <summary>
        /// Determines whether [is mesh collider] [the specified collider].
        /// </summary>
        /// <param name="collider">The collider.</param>
        /// <returns>
        ///   <c>true</c> if [is mesh collider] [the specified collider]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsMeshCollider(Collider collider) {
            return collider.GetType() == typeof(MeshCollider);
        }

        /// <summary>
        /// Gets the index of material.
        /// </summary>
        /// <param name="sharedMesh">The shared mesh.</param>
        /// <param name="triangleIndex">Index of the triangle.</param>
        /// <returns></returns>
        static int GetIndexOfMaterial(Mesh sharedMesh, int triangleIndex) {
            int[] triangles = sharedMesh.triangles;
            for (int i = 0; i < sharedMesh.subMeshCount; i++) {
                if (triangles[triangleIndex * 3].In(sharedMesh.GetTriangles(i))) {
                    return i;
                }
            }
            return -1;
        }

        /// <summary>
        /// Selects the material.
        /// </summary>
        /// <param name="go">The go.</param>
        /// <param name="triangleIndex">Index of the triangle.</param>
        /// <returns></returns>
        static Material SelectMaterial(GameObject go, int triangleIndex) {
            if (IsMeshCollider(go.collider)) {
                MeshCollider meshCollider = (MeshCollider)go.collider;
                int index = GetIndexOfMaterial(meshCollider.sharedMesh, triangleIndex);
                return go.renderer.materials[index];
            }
            return null;
        }


        /// <summary>
        /// Gets the material using triangle index of mesh collider.
        /// </summary>
        /// <param name="go">The go.</param>
        /// <param name="triangleIndex">Index of the triangle.</param>
        /// <returns></returns>
        static public Material GetMaterialUsingTriangleIndexOfMeshCollider(this GameObject go, int triangleIndex) {
            return SelectMaterial(go, triangleIndex);
        }
    }
}