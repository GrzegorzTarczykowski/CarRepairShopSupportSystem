using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using CarRepairShopSupportSystem.Adapter;
using CarRepairShopSupportSystem.BLL.Enums;
using CarRepairShopSupportSystem.BLL.IService;
using CarRepairShopSupportSystem.BLL.Models;
using CarRepairShopSupportSystem.BLL.Service;
using Newtonsoft.Json;

namespace CarRepairShopSupportSystem.Activity
{
    [Activity(Label = "Lista zadań")]
    public class OrderByWorkerListActivity : AppCompatActivity
    {
        private const int orderDetailsRequestCode = 1;
        private readonly IOrderService orderService;
        private readonly IApplicationSessionService applicationSessionService;
        private List<Order> orderByWorkerList;

        public OrderByWorkerListActivity()
        {
            this.orderService = new OrderService(new HttpClientService(new AccessTokenService(new ApplicationSessionService(), new TokenService())));
            applicationSessionService = new ApplicationSessionService();
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_orderByWorkerList);
            GridView gvOrderByWorkerList = FindViewById<GridView>(Resource.Id.gvOrderByWorkerList);
            RefreshGvOrderByWorkerList(gvOrderByWorkerList);
            gvOrderByWorkerList.ItemClick += GvOrderByWorkerList_ItemClick;
        }

        private void RefreshGvOrderByWorkerList(GridView gvOrderByWorkerList)
        {
            orderByWorkerList = orderService.GetOrderListByWorker(applicationSessionService.GetUserFromApplicationSession().UserId)
                                                .ToList();
            gvOrderByWorkerList.Adapter = new OrderAdapter(this, orderByWorkerList.ToArray());
        }

        private void GvOrderByWorkerList_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            Intent nextActivity = new Intent(this, typeof(OrderDetailsActivity));
            nextActivity.PutExtra("OrderDetails", JsonConvert.SerializeObject(orderByWorkerList[e.Position]));
            nextActivity.PutExtra(nameof(PermissionId), PermissionId.Admin.ToString());
            StartActivityForResult(nextActivity, orderDetailsRequestCode);
        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            RefreshGvOrderByWorkerList(FindViewById<GridView>(Resource.Id.gvOrderByWorkerList));
        }
    }
}