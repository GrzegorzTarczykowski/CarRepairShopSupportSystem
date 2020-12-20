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
using CarRepairShopSupportSystem.BLL.Extensions;
using CarRepairShopSupportSystem.BLL.IService;
using CarRepairShopSupportSystem.BLL.Service;
using CarRepairShopSupportSystem.Extensions;
using Newtonsoft.Json;

namespace CarRepairShopSupportSystem.Activity
{
    [Activity(Label = "Lista usług")]
    public class ServiceListActivity : AppCompatActivity
    {
        private const int serviceRequestCode = 1;
        private readonly IServiceService serviceService;
        private List<BLL.Models.Service> serviceList;
        private List<BLL.Models.Service> selectedServiceList;

        public ServiceListActivity()
        {
            serviceService = new ServiceService(new HttpClientService(new AccessTokenService(new ApplicationSessionService(), new TokenService())));
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_serviceList);
            ListView lvServiceList = FindViewById<ListView>(Resource.Id.lvServiceList);
            lvServiceList.ChoiceMode = ChoiceMode.Multiple;
            lvServiceList.ItemClick += GvServiceList_ItemClick;
            RefreshLvServiceList(lvServiceList);
            if (Intent.GetStringExtra(nameof(OperationType)) == OperationType.Edit.GetDescription())
            {
                Button btnAddService = FindViewById<Button>(Resource.Id.btnAddService);
                btnAddService.Click += BtnAddService_Click;
                btnAddService.Visibility = ViewStates.Visible;
            }
            else
            {
                Button btnSubmitSelectedServices = FindViewById<Button>(Resource.Id.btnSubmitSelectedServices);
                btnSubmitSelectedServices.Click += BtnSubmitSelectedServices_Click;
                btnSubmitSelectedServices.Visibility = ViewStates.Visible;
            }
        }

        private void RefreshLvServiceList(ListView lvServiceList)
        {
            serviceList = serviceService.GetAllServiceList().ToList();
            selectedServiceList = JsonConvert.DeserializeObject<List<BLL.Models.Service>>(Intent.GetStringExtra("SelectedServiceList") ?? string.Empty) ?? new List<BLL.Models.Service>();
            lvServiceList.Adapter = new ServiceAdapter(this, serviceList);
            selectedServiceList.ForEach(selectedService =>
            {
                lvServiceList.SetItemChecked(serviceList.FindIndex(s => s.ServiceId == selectedService.ServiceId), true);
            });
        }

        private void GvServiceList_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            if (Intent.GetStringExtra(nameof(OperationType)) == OperationType.Edit.GetDescription())
            {
                Intent nextActivity = new Intent(this, typeof(ServiceActivity));
                nextActivity.PutExtra("SelectedService", JsonConvert.SerializeObject(serviceList[e.Position]));
                StartActivityForResult(nextActivity, serviceRequestCode);
            }
        }

        private void BtnSubmitSelectedServices_Click(object sender, EventArgs e)
        {
            selectedServiceList 
                = FindViewById<ListView>(Resource.Id.lvServiceList).GetSelectedItems<BLL.Models.Service>();
            Intent intent = new Intent(this, typeof(OrderEditorActivity));
            intent.PutExtra("SelectedServiceList", JsonConvert.SerializeObject(selectedServiceList));
            SetResult(Result.Ok, intent);
            Finish();
        }

        private void BtnAddService_Click(object sender, EventArgs e)
        {
            Intent nextActivity = new Intent(this, typeof(ServiceActivity));
            StartActivityForResult(nextActivity, serviceRequestCode);
        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if (requestCode == serviceRequestCode)
            {
                ListView lvServiceList = FindViewById<ListView>(Resource.Id.lvServiceList);
                RefreshLvServiceList(lvServiceList);
            }
        }
    }
}