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
using CarRepairShopSupportSystem.BLL.Models;
using CarRepairShopSupportSystem.BLL.Service;
using Newtonsoft.Json;

namespace CarRepairShopSupportSystem.Activity
{
    [Activity(Label = "Lista części")]
    public class VehiclePartListActivity : AppCompatActivity
    {
        private const int vehiclePartRequestCode = 1;
        private readonly IVehiclePartService vehiclePartService;
        private IList<VehiclePart> vehiclePartList;
        private IList<VehiclePart> selectedVehiclePartList;

        public VehiclePartListActivity()
        {
            vehiclePartService = new VehiclePartService(new HttpClientService(new AccessTokenService(new ApplicationSessionService(), new TokenService())));
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_vehiclePartList);
            GridView gvVehiclePartList = FindViewById<GridView>(Resource.Id.gvVehiclePartList);
            RefreshGvVehiclePartList(gvVehiclePartList);
            gvVehiclePartList.ItemClick += GvVehiclePartList_ItemClick;
            if (Intent.GetStringExtra(nameof(OperationType)) == OperationType.Edit.GetDescription())
            {
                Button btnAddVehiclePart = FindViewById<Button>(Resource.Id.btnAddVehiclePart);
                btnAddVehiclePart.Click += BtnAddVehiclePart_Click;
                btnAddVehiclePart.Visibility = ViewStates.Visible;
            }
            else
            {
                Button btnSubmitSelectedVehicleParts = FindViewById<Button>(Resource.Id.btnSubmitSelectedVehicleParts);
                btnSubmitSelectedVehicleParts.Click += BtnSubmitSelectedVehicleParts_Click;
                btnSubmitSelectedVehicleParts.Visibility = ViewStates.Visible;
            }
        }

        private void RefreshGvVehiclePartList(GridView gvVehiclePartList)
        {
            vehiclePartList = vehiclePartService.GetAllVehiclePartList().ToList();
            selectedVehiclePartList = JsonConvert.DeserializeObject<IList<VehiclePart>>(Intent.GetStringExtra("SelectedVehiclePartList") ?? string.Empty) ?? new List<VehiclePart>();
            gvVehiclePartList.Adapter = new VehiclePartAdapter(this, vehiclePartList.ToArray(), selectedVehiclePartList.ToArray());
        }

        private void GvVehiclePartList_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            if (Intent.GetStringExtra(nameof(OperationType)) == OperationType.Edit.GetDescription())
            {
                Intent nextActivity = new Intent(this, typeof(VehiclePartActivity));
                nextActivity.PutExtra("SelectedVehiclePart", JsonConvert.SerializeObject(vehiclePartList[e.Position]));
                StartActivityForResult(nextActivity, vehiclePartRequestCode);
            }
            else
            {
                VehiclePart vehiclePart = selectedVehiclePartList.FirstOrDefault(svp => svp.VehiclePartId == vehiclePartList[e.Position].VehiclePartId);
                if (vehiclePart != null)
                {
                    ((LinearLayout)e.View).SetBackgroundColor(Android.Graphics.Color.Transparent);
                    selectedVehiclePartList.Remove(vehiclePart);
                }
                else
                {
                    ((LinearLayout)e.View).SetBackgroundColor(Android.Graphics.Color.GreenYellow);
                    selectedVehiclePartList.Add(vehiclePartList[e.Position]);
                }
            }
        }

        private void BtnSubmitSelectedVehicleParts_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(OrderEditorActivity));
            intent.PutExtra("SelectedVehiclePartList", JsonConvert.SerializeObject(selectedVehiclePartList));
            SetResult(Result.Ok, intent);
            Finish();
        }

        private void BtnAddVehiclePart_Click(object sender, EventArgs e)
        {
            Intent nextActivity = new Intent(this, typeof(VehiclePartActivity));
            StartActivityForResult(nextActivity, vehiclePartRequestCode);
        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if (requestCode == vehiclePartRequestCode)
            {
                GridView gvVehiclePartList = FindViewById<GridView>(Resource.Id.gvVehiclePartList);
                RefreshGvVehiclePartList(gvVehiclePartList);
            }
        }
    }
}