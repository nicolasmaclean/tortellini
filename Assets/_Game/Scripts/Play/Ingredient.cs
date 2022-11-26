using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Play
{
    public class Ingredient : MonoBehaviour
    {
        public enum IngredientType
        {
            Lime_Juice, 
            Lemon_Juice,
            Ginger_Beer,
            Tonic_Water,
            Simple_Syrup,
            Egg_White,
            Bitters,
            Tomato_Juice,
            Orange_Juice,
            Coke
        }

        public IngredientType _type;
        public bool _full;

        [SerializeField] Material _fullMaterial;
        [SerializeField] Material _emptyMaterial;

        #region Monobehavior
        private void Start()
        {
            FillIngredient();
        }
        #endregion
        public void FillIngredient()
        {
            _full = true;
            ChangeMaterial(_full);
        }
        public void EmptyIngredient()
        {
            _full = false;
            ChangeMaterial(_full);
        }
        private void ChangeMaterial(bool full)
        {
            MeshRenderer[] meshes = GetComponentsInChildren<MeshRenderer>();
            foreach(var mesh in meshes)
            {
                if (full)
                    mesh.material = _fullMaterial;
                else
                    mesh.material = _emptyMaterial;
            }
        }
    }
}
