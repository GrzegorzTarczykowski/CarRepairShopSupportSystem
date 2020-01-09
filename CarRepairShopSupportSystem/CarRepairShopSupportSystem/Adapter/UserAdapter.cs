using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using CarRepairShopSupportSystem.BLL.Models;
using Newtonsoft.Json;

namespace CarRepairShopSupportSystem.Adapter
{
    internal class UserAdapter : BaseAdapter
    {
        private readonly Context context;
        private readonly User[] users;
        private readonly User[] selectedUsers;

        public UserAdapter(Context context, User[] users, User[] selectedUsers)
        {
            this.context = context;
            this.users = users;
            this.selectedUsers = selectedUsers;
        }

        public override int Count => users.Length;

        public override Java.Lang.Object GetItem(int position)
        {
            return JsonConvert.SerializeObject(users[position]);
        }

        public override long GetItemId(int position)
        {
            return users[position].UserId;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            LinearLayout linearLayout = new LinearLayout(context);
            linearLayout.Orientation = Orientation.Vertical;
            TextView tvBrandNameAndModelName = new TextView(context)
            {
                TextSize = 40,
                Text = $"{users[position].FirstName} {users[position].LastName}"
            };
            linearLayout.AddView(tvBrandNameAndModelName);

            if (selectedUsers.Any(su => su.UserId == users[position].UserId))
            {
                linearLayout.SetBackgroundColor(Android.Graphics.Color.GreenYellow);
            }
            else
            {
                linearLayout.SetBackgroundColor(Android.Graphics.Color.Transparent);
            }

            return linearLayout;
        }
    }
}