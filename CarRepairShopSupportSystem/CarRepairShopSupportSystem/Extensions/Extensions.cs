using Android.Widget;
using System.Collections.Generic;

namespace CarRepairShopSupportSystem.Extensions
{
    internal static class Extensions
    {
        internal static T Cast<T>(this Java.Lang.Object obj) where T : class
        {
            var propertyInfo = obj.GetType().GetProperty("Instance");
            return propertyInfo == null ? null : propertyInfo.GetValue(obj, null) as T;
        }

        internal static List<T> GetSelectedItems<T>(this ListView listView) where T : class
        {
            var sparseArray = listView.CheckedItemPositions;
            List<T> selectedItems = new List<T>();
            for (var i = 0; i < sparseArray.Size(); i++)
            {
                if (sparseArray.ValueAt(i))
                {
                    selectedItems.Add(listView.Adapter.GetItem(sparseArray.KeyAt(i)).Cast<T>());
                }
            }
            return selectedItems;
        }
    }
}