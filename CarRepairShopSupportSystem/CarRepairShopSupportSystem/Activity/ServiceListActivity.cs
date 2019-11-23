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
using CarRepairShopSupportSystem.BLL.IService;
using CarRepairShopSupportSystem.BLL.Service;

namespace CarRepairShopSupportSystem.Activity
{
    [Activity(Label = "Lista usług")]
    public class ServiceListActivity : AppCompatActivity
    {
        private readonly IServiceService serviceService;
        private IList<BLL.Models.Service> services;

        public ServiceListActivity()
        {
            serviceService = new ServiceService(new HttpClientService(new AccessTokenService(new ApplicationSessionService(), new TokenService())));

        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_serviceList);
            GridView gvServiceList = FindViewById<GridView>(Resource.Id.gvServiceList);
            RefreshGvServiceList(gvServiceList);
            gvServiceList.ItemClick += GvServiceList_ItemClick;
            FindViewById<Button>(Resource.Id.btnAddService).Click += BtnAddService_Click;
            FindViewById<Button>(Resource.Id.btnSubmitSelectedServices).Click += BtnSubmitSelectedServices_Click;
        }

        private void RefreshGvServiceList(GridView gvServiceList)
        {
            services = serviceService.GetAllServiceList().ToList();
            gvServiceList.Adapter = new ServiceAdapter(this, services.ToArray());
        }

        private void GvServiceList_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void BtnSubmitSelectedServices_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void BtnAddService_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
        }
    }
}