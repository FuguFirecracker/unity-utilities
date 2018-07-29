using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FuguFirecracker.Utilities
{
    public static class Sorting
    {
/// <summary>
/// Returns selection ordered by Hierarchy
/// </summary>
/// <param name="selection">Typically, Selection.gameObjects</param>
/// <returns></returns>
        public static List<GameObject> SaneSort(IEnumerable<GameObject> selection)
        {
            var orderedList = selection.OrderBy(g => g.transform.GetSiblingIndex()).ToList();

            var toplevel = new List<GameObject>();
            var sorted = new List<GameObject>();

            foreach (var go in orderedList)
            {
                if (go.transform.parent == null) { toplevel.Add(go); }
                else { ParentTrap(go, orderedList, toplevel); }
            }

            foreach (var go in toplevel)
            {
                ChildHunt(go, orderedList, sorted);
            }

            return sorted;
        }

        private static void ChildHunt(GameObject parent, ICollection<GameObject> orderedList, ICollection<GameObject> sorted)
        {
            sorted.Add(parent);

            foreach (Transform t in parent.transform)
            {
                var child = t.gameObject;
                if (orderedList.Contains(child))
                {
                    ChildHunt(child, orderedList, sorted);
                }
            }
        }

        private static void ParentTrap(GameObject go, ICollection<GameObject> orderedList, ICollection<GameObject> topLevel)
        {
            var parent = go.transform.parent.gameObject;
            if (!orderedList.Contains(parent)) { topLevel.Add(go); }
        }
    }
}
