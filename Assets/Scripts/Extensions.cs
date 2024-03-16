using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace NCU
{
    public static class Extensions
    {
        public static Bounds GetObjectBounds(this GameObject go)
        {

            var allRenderers = go.GetObjectRenderers();

            Bounds boundToReturn = new Bounds(go.transform.position, Vector3.zero);

            if (allRenderers.Count == 0)
                return boundToReturn;
            

            foreach (var meshRenderer in allRenderers)
            {
                if (meshRenderer.bounds.size.magnitude > boundToReturn.size.magnitude)
                    boundToReturn = meshRenderer.bounds;
            }

            return boundToReturn;

        }
        
        public static List<Collider> GetObjectCollider(this GameObject go)
        {
            return go.GetComponents<Collider>().ToList();
        }
        
        public static List<MeshRenderer> GetObjectRenderers(this GameObject go)
        {
            return go.GetComponents<MeshRenderer>().ToList();
        }
        
    }
}