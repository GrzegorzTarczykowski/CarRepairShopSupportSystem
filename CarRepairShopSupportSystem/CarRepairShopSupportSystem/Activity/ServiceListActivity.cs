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
using Newtonsoft.Json;

namespace CarRepairShopSupportSystem.Activity
{
    [Activity(Label = "Lista usług")]
    public class ServiceListActivity : AppCompatActivity
    {
        private const int serviceRequestCode = 1;
        private readonly IServiceService serviceService;
        private IList<BLL.Models.Service> serviceList;
        private IList<BLL.Models.Service> selectedServiceList;

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

        private void RefreshGvServiceList(GridView gvServiceList)
        {
            serviceList = serviceService.GetAllServiceList().ToList();
            selectedServiceList = JsonConvert.DeserializeObject<IList<BLL.Models.Service>>(Intent.GetStringExtra("SelectedServiceList")) ?? new List<BLL.Models.Service>();
            gvServiceList.Adapter = new ServiceAdapter(this, serviceList.ToArray(), selectedServiceList.ToArray());
        }

        private void GvServiceList_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            if (Intent.GetStringExtra(nameof(OperationType)) == OperationType.Edit.GetDescription())
            {
                Intent nextActivity = new Intent(this, typeof(ServiceActivity));
                nextActivity.PutExtra("SelectedService", JsonConvert.SerializeObject(serviceList[e.Position]));
                StartActivityForResult(nextActivity, serviceRequestCode);
            }
            else
            {
                BLL.Models.Service service = selectedServiceList.FirstOrDefault(ss => ss.ServiceId == serviceList[e.Position].ServiceId);
                if (service != null)
                {
                    ((LinearLayout)e.View).SetBackgroundColor(Android.Graphics.Color.Transparent);
                    selectedServiceList.Remove(service);
                }
                else
                {
                    ((LinearLayout)e.View).SetBackgroundColor(Android.Graphics.Color.GreenYellow);
                    selectedServiceList.Add(serviceList[e.Position]);
                }
            } 
        }

        private void BtnSubmitSelectedServices_Click(object sender, EventArgs e)
        {
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
    }
}