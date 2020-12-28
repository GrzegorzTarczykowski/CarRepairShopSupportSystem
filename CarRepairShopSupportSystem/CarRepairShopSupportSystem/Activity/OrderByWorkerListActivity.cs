using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Widget;
using Autofac;
using CarRepairShopSupportSystem.Adapter;
using CarRepairShopSupportSystem.BLL.IService;
using CarRepairShopSupportSystem.BLL.Models;
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
            orderService = MainApplication.Container.Resolve<IOrderService>();
            applicationSessionService = MainApplication.Container.Resolve<IApplicationSessionService>();
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_orderByWorkerList);
            ListView lvOrderByWorkerList = FindViewById<ListView>(Resource.Id.lvOrderByWorkerList);
            RefreshGvOrderByWorkerList(lvOrderByWorkerList);
            lvOrderByWorkerList.ItemClick += LvOrderByWorkerList_ItemClick;
        }

        private void RefreshGvOrderByWorkerList(ListView lvOrderByWorkerList)
        {
            orderByWorkerList = orderService.GetOrderListByWorker(applicationSessionService.GetUserFromApplicationSession().UserId)
                                            .ToList();
            lvOrderByWorkerList.Adapter = new OrderAdapter(this, orderByWorkerList.ToList(), true);
        }

        private void LvOrderByWorkerList_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            Intent nextActivity = new Intent(this, typeof(OrderDetailsActivity));
            nextActivity.PutExtra("OrderDetails", JsonConvert.SerializeObject(orderByWorkerList[e.Position]));
            StartActivityForResult(nextActivity, orderDetailsRequestCode);
        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            RefreshGvOrderByWorkerList(FindViewById<ListView>(Resource.Id.lvOrderByWorkerList));
        }
    }
}