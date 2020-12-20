using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Widget;
using CarRepairShopSupportSystem.Adapter;
using CarRepairShopSupportSystem.BLL.IService;
using CarRepairShopSupportSystem.BLL.Models;
using CarRepairShopSupportSystem.BLL.Service;
using Newtonsoft.Json;

namespace CarRepairShopSupportSystem.Activity
{
    [Activity(Label = "Zarządzanie listą zleceń")]
    public class OrderManagerListActivity : AppCompatActivity
    {
        private const int orderDetailsRequestCode = 1;
        private readonly IOrderService orderService;
        private List<Order> orderList;

        public OrderManagerListActivity()
        {
            orderService = new OrderService(new HttpClientService(new AccessTokenService(new ApplicationSessionService(), new TokenService())));
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_orderManagerList);
            ListView lvOrderManagerList = FindViewById<ListView>(Resource.Id.lvOrderManagerList);
            RefreshGvOrderManagerList(lvOrderManagerList);
            lvOrderManagerList.ItemClick += LvOrderManagerList_ItemClick;
        }

        private void LvOrderManagerList_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            Intent nextActivity = new Intent(this, typeof(OrderDetailsActivity));
            nextActivity.PutExtra("OrderDetails", JsonConvert.SerializeObject(orderList[e.Position]));
            StartActivityForResult(nextActivity, orderDetailsRequestCode);
        }

        private void RefreshGvOrderManagerList(ListView lvOrderManagerList)
        {
            orderList = orderService.GetAllOrderList().ToList();
            lvOrderManagerList.Adapter = new OrderAdapter(this, orderList.ToList(), true);
        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            RefreshGvOrderManagerList(FindViewById<ListView>(Resource.Id.lvOrderManagerList));
        }
    }
}