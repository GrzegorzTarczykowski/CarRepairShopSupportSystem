﻿using System;
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
using CarRepairShopSupportSystem.BLL.IService;
using CarRepairShopSupportSystem.BLL.Models;
using CarRepairShopSupportSystem.BLL.Service;

namespace CarRepairShopSupportSystem.Activity
{
    [Activity(Label = "OrderManagerListActivity")]
    public class OrderManagerListActivity : AppCompatActivity
    {
        private readonly IOrderService orderService;
        private List<Order> orderList;

        public OrderManagerListActivity()
        {
            this.orderService = new OrderService(new HttpClientService(new AccessTokenService(new ApplicationSessionService(), new TokenService())));
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_orderManagerList);
            GridView gvOrderManagerList = FindViewById<GridView>(Resource.Id.gvOrderManagerList);
            RefreshGvOrderManagerList(gvOrderManagerList);
            gvOrderManagerList.ItemClick += GvOrderManagerList_ItemClick;
        }

        private void GvOrderManagerList_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {

        }

        private void RefreshGvOrderManagerList(GridView gvOrderManagerList)
        {
            orderList = orderService.GetAllOrderList().ToList();
            gvOrderManagerList.Adapter = new OrderAdapter(this, orderList.ToArray());
        }
    }
}